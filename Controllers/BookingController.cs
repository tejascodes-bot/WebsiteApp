using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;
using WebsiteApp.Models.ViewModels;
using System.Data;

namespace WebsiteApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        [HttpGet]
        public async Task<IActionResult> Create(
            int roomTypeId,
            DateTime checkIn,
            DateTime checkOut,
            int guests = 2)
        {
            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(r =>
                    r.RoomTypeId == roomTypeId &&
                    r.IsActive);

            if (roomType == null)
                return NotFound("Room category not found.");

            if (checkIn.Date < DateTime.Today ||
                checkOut.Date <= checkIn.Date ||
                guests < 1 ||
                guests > roomType.MaxCapacity)
            {
                return BadRequest("Invalid booking details.");
            }

            ViewBag.CheckIn = checkIn.Date;
            ViewBag.CheckOut = checkOut.Date;
            ViewBag.Guests = guests;

            return View(roomType);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            int roomTypeId,
            DateTime checkIn,
            DateTime checkOut,
            int guests,
            string firstName,
            string lastName,
            string email,
            string phone)
        {
            checkIn = checkIn.Date;
            checkOut = checkOut.Date;

            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(r =>
                    r.RoomTypeId == roomTypeId &&
                    r.IsActive);

            if (roomType == null)
                return NotFound("Room category not found.");

            if (checkIn < DateTime.Today ||
                checkOut <= checkIn ||
                guests < 1 ||
                guests > roomType.MaxCapacity)
            {
                ModelState.AddModelError("",
                    "Please enter valid booking dates and guest count.");

                return await ReturnCreateView(
                    roomTypeId, checkIn, checkOut, guests);
            }

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone))
            {
                ModelState.AddModelError("",
                    "Please complete all guest details.");

                return await ReturnCreateView(
                    roomTypeId, checkIn, checkOut, guests);
            }

            if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute()
                .IsValid(email))
            {
                ModelState.AddModelError("",
                    "Please enter a valid email address.");

                return await ReturnCreateView(
                    roomTypeId, checkIn, checkOut, guests);
            }

            try
            {
                await using var transaction =
                    await _context.Database.BeginTransactionAsync(
                        IsolationLevel.Serializable);

                // Count physical rooms excluding maintenance rooms
                var totalRooms = await _context.Rooms
                    .CountAsync(r =>
                        r.RoomTypeId == roomTypeId &&
                        !r.IsOutOfOrder);

                // Count all overlapping active category bookings
                var overlappingBookings = await _context.Bookings
                    .CountAsync(b =>
                        b.RoomTypeId == roomTypeId &&
                        b.CheckInDate < checkOut &&
                        b.CheckOutDate > checkIn &&
                        (b.Status == "Confirmed" ||
                         b.Status == "CheckedIn"));

                if (totalRooms <= overlappingBookings)
                {
                    await transaction.RollbackAsync();

                    ModelState.AddModelError("",
                        "Sorry! No rooms are available for these dates.");

                    return await ReturnCreateView(
                        roomTypeId, checkIn, checkOut, guests);
                }

                // Calculate price
                decimal totalPrice = 0;

                for (DateTime date = checkIn;
                     date < checkOut;
                     date = date.AddDays(1))
                {
                    if (date.DayOfWeek == DayOfWeek.Friday ||
                        date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        totalPrice += roomType.WeekendPrice;
                    }
                    else
                    {
                        totalPrice += roomType.WeekdayPrice;
                    }
                }

                // Save guest
                var guest = new Guest
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    Email = email.Trim(),
                    Phone = phone.Trim()
                };

                _context.Guests.Add(guest);

                await _context.SaveChangesAsync();

                // Create booking
                var booking = new Booking
                {
                    ConfirmationId = Guid.NewGuid()
                        .ToString("N")[..10].ToUpper(),

                    GuestId = guest.GuestId,

                    RoomTypeId = roomTypeId,

                    // Physical room assigned during check-in
                    RoomId = null,

                    CheckInDate = checkIn,

                    CheckOutDate = checkOut,

                    TotalPrice = totalPrice,

                    Status = "Confirmed",

                    CreatedAt = DateTime.UtcNow
                };

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return RedirectToAction(
                    nameof(Confirmation),
                    new { id = booking.ConfirmationId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("",
                    "Booking failed. Please check your database connection and try again.");

                return await ReturnCreateView(
                    roomTypeId, checkIn, checkOut, guests);
            }
        }

        // GET: Booking/Confirmation
        [HttpGet]
        public async Task<IActionResult> Confirmation(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.RoomType)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b =>
                    b.ConfirmationId == id);

            if (booking == null)
                return NotFound("Booking not found.");

            var model = new BookingConfirmationViewModel
            {
                Booking = booking
            };

            return View(model);
        }

        // Helper method
        private async Task<IActionResult> ReturnCreateView(
    int roomTypeId,
    DateTime checkIn,
    DateTime checkOut,
    int guests)
        {
            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(r =>
                    r.RoomTypeId == roomTypeId);

            if (roomType == null)
            {
                return NotFound("Room category not found.");
            }

            ViewBag.CheckIn = checkIn;
            ViewBag.CheckOut = checkOut;
            ViewBag.Guests = guests;

            return View("Create", roomType);
        }
    
    }
}