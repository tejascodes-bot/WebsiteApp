using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApp.Models
{
    public class Booking
    {
        // Primary Key
        [Key]
        public int BookingId { get; set; }

        // Booking Confirmation Number
        [Required]
        [StringLength(30)]
        public string ConfirmationId { get; set; } = string.Empty;

        // Compatibility property for existing views
        [NotMapped]
        public string ConfirmationCode
        {
            get => ConfirmationId;
            set => ConfirmationId = value;
        }

        // Guest Foreign Key
        [Required]
        public int GuestId { get; set; }

        // Guest Relationship
        public Guest? Guest { get; set; }

        // Room Type Foreign Key
        [Required]
        public int RoomTypeId { get; set; }

        // Room Type Relationship
        public RoomType? RoomType { get; set; }

        // Physical Room Foreign Key
        // Null until room assignment during check-in
        public int? RoomId { get; set; }

        // Physical Room Relationship
        public Room? Room { get; set; }

        // Check-In Date
        [Required]
        public DateTime CheckInDate { get; set; }

        // Check-Out Date
        [Required]
        public DateTime CheckOutDate { get; set; }

        // Total Booking Price
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 99999999)]
        public decimal TotalPrice { get; set; }

        // Booking Status
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Confirmed";

        // Booking Created Date
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}