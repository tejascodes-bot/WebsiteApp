using WebsiteApp.Models;

namespace WebsiteApp.Models
{
    public class RoomSearchViewModel
    {
        public DateTime CheckIn { get; set; } = DateTime.Today;

        public DateTime CheckOut { get; set; } = DateTime.Today.AddDays(1);

        public int Guests { get; set; } = 1;

        public List<RoomSearchResult> Results { get; set; } = new();

        public string? ErrorMessage { get; set; }
    }

    public class RoomSearchResult
    {
        public RoomType RoomType { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public bool IsAvailable { get; set; }
    }
}