using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteApp.Models
{
    public class Guest
    {
        // Primary Key
        [Key]
        public int GuestId { get; set; }

        // First Name
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        // Last Name
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Email Address
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        // Phone Number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;

        // Complete Guest Name
        [NotMapped]
        public string FullName
        {
            get
            {
                return (FirstName + " " + LastName).Trim();
            }
        }

        // One Guest can have multiple Bookings
        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}