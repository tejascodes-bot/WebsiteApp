using System;
using System.ComponentModel.DataAnnotations;
using WebsiteApp.Models;

namespace WebsiteApp.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public int RoomTypeId { get; set; }

        public RoomType? RoomType { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone]
        public string Phone { get; set; } = "";

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        public int Guests { get; set; } = 1;

        public decimal TotalPrice { get; set; }
    }
}
