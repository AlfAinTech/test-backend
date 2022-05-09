using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models
{
    public class ListingItem
    {
        public int UserId { get; set; } 

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(300)]
        [EmailAddress]
        public string Email { get; set; }

        public int MembershipType { get; set; }
        public int BillingFrequency { get; set; }
        public DateOnly NextInvoice { get; set; }
    }
}
