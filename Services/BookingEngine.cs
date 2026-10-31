using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;
using WebsiteApp.Models;

namespace WebsiteApp.Services
{
    public class BookingEngine : IBookingEngine
    {
        private readonly ApplicationDbContext _context;

        public BookingEngine(ApplicationDbContext context)
        {
            _context = context;
        }

        // Friday and Saturday use weekend pricing.
        // The checkout date is not charged.
        public decimal CalculateTotalPrice(
            RoomType roomType,
            DateTime checkIn,
            DateTime checkOut)
        {
            if (checkIn.Date < DateTime.Today)
            {
                throw new ArgumentException(
                    "Check-in cannot be in the past.");
            }

            if (checkOut.Date <= checkIn.Date)
            {
                throw new ArgumentException(
                    "Check-out must be after check-in.");
            }

            decimal total = 0m;

            for (DateTime night = checkIn.Date;
                 night < checkOut.Date;
                 night = night.AddDays(1))
            {
                bool isWeekend =
                    night.DayOfWeek == DayOfWeek.Friday ||
                    night.DayOfWeek == DayOfWeek.Saturday;

                total += isWeekend
                    ? roomType.WeekendPrice
                    : roomType.WeekdayPrice;
            }

            return total;
        }

        public bool ValidateDates(
            DateTime checkIn,
            DateTime checkOut,
            out string errorMessage)
        {
            if (checkIn.Date < DateTime.Today)
            {
                errorMessage = "Check-in cannot be in the past.";
                return false;
            }

            if (checkOut.Date <= checkIn.Date)
            {
                errorMessage =
                    "Check-out must be at least one day after check-in.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        public async Task<bool> IsRoomTypeAvailableAsync(
            int roomTypeId,
            DateTime checkIn,
            DateTime checkOut,
            CancellationToken cancellationToken = default)
        {
            if (!ValidateDates(checkIn, checkOut, out _))
            {
                return false;
            }

            // Count physical rooms that are not under maintenance.
            int physicalRooms = await _context.Rooms
                .CountAsync(
                    r => r.RoomTypeId == roomTypeId
                         && !r.IsOutOfOrder,
                    cancellationToken);

            if (physicalRooms == 0)
            {
                return false;
            }

            // Count active reservations that overlap the requested stay.
            // A checkout on the requested check-in date does not overlap.
            int overlappingBookings = await _context.Bookings
                .CountAsync(
                    b => b.RoomTypeId == roomTypeId
                         && b.CheckInDate < checkOut.Date
                         && b.CheckOutDate > checkIn.Date
                         && b.Status != "Cancelled"
                         && b.Status != "Completed",
                    cancellationToken);

            return overlappingBookings < physicalRooms;
        }
    }
}