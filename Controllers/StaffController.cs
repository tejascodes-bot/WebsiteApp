using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteApp.Data;

namespace WebsiteApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? confirmationNumber)
        {
            var bookings = _context.Bookings
                .Include(b => b.Guest)
                .Include(b => b.RoomType)
                .Include(b => b.Room)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(confirmationNumber))
            {
                string search = confirmationNumber.Trim();

                bookings = bookings.Where(b =>
                    b.ConfirmationId == search);
            }

            var result = await bookings
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            ViewBag.SearchTerm = confirmationNumber;

            return View(result);
        }
    }
}