using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp1.Entities;

namespace TestApp1.Models
{
    public class Membership
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey]
        public int CustomersId { get; set; }
        public Customers Customers { get; set; }

        [Required]
        [EnumDataType(typeof(MembershipType))]
        public string MembershipType { get; set; }

        [Required]
        [EnumDataType(typeof(BillingFrequency))]
        public string? BillingFrequency { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MembershipPrice { get; set; }

        public ICollection<Invoices> Invoices { get; set; }

    }
}
