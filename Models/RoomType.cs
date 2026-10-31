using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApp.Models
{
    public class RoomType
    {
        // Primary Key
        [Key]
        public int RoomTypeId { get; set; }

        // Room Category Name
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        // Room Description
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Maximum Number of Guests
        [Range(1, 10)]
        public int MaxCapacity { get; set; }

        // Weekday Room Price
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999)]
        public decimal WeekdayPrice { get; set; }

        // Weekend Room Price
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 999999)]
        public decimal WeekendPrice { get; set; }

        // Active or Inactive Room Category
        public bool IsActive { get; set; } = true;

        // Relationship with Room
        public ICollection<Room> Rooms { get; set; }
            = new List<Room>();

        // Relationship with Booking
        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}