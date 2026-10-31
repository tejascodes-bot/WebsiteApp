using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApp.Models
{
    public class Room
    {
        // Primary Key
        [Key]
        public int RoomId { get; set; }

        // Room Number
        [Required]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        // Foreign Key
        public int RoomTypeId { get; set; }

        // Room Category Relationship
        public RoomType? RoomType { get; set; }

        // Housekeeping Status
        public bool IsClean { get; set; } = true;

        // Maintenance Status
        public bool IsOutOfOrder { get; set; } = false;

        // Booking Relationship
        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}