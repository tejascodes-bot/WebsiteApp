using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApp.Models
{
    public class BookingCheckoutViewModel
    {
        public int RoomTypeId { get; set; }

        public string RoomTypeName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Range(1, 5)]
        public int Guests { get; set; } = 1;

        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        
        public decimal TotalPrice { get; set; }

        public string? ConfirmationCode { get; set; }
    }
}
