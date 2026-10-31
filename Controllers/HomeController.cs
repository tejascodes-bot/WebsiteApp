using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;

namespace WebsiteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            DateTime? checkIn,
            DateTime? checkOut,
            int guests = 2)
        {
            DateTime today = DateTime.Today;

            DateTime selectedCheckIn =
                checkIn ?? today.AddDays(1);

            DateTime selectedCheckOut =
                checkOut ?? selectedCheckIn.AddDays(1);

            if (selectedCheckIn.Date < today ||
                selectedCheckOut.Date <= selectedCheckIn.Date)
            {
                selectedCheckIn = today.AddDays(1);
                selectedCheckOut = today.AddDays(2);
            }

            if (guests < 1 || guests > 5)
            {
                guests = 2;
            }

            ViewBag.CheckIn = selectedCheckIn.ToString("yyyy-MM-dd");
            ViewBag.CheckOut = selectedCheckOut.ToString("yyyy-MM-dd");
            ViewBag.Guests = guests;

            var roomTypes = await _context.RoomTypes
                .Where(r => r.IsActive && r.MaxCapacity >= guests)
                .ToListAsync();

            return View(roomTypes);
        }
    }
}