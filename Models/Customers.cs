using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; } 

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public ICollection<CustomerAddress>? Address { get; set; }
        public ICollection<CustomerContactInfo>? ContactInfo { get; set; }
        public Membership? MembershipInfo { get; set; }
        //public ICollection<Invoices> Invoices { get; set; }

        public Customers(string firstName, string lastName, string email, DateTime? dateOfBirth = null)
        {
            (FirstName, LastName, Email, DateOfBirth) = (firstName, lastName, email, dateOfBirth);

        }
            
    }
}
