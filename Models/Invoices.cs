using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp1.Entities;

namespace TestApp1.Models
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey]
        //public int CustomersId { get; set; }
        //public Customers Customers { get; set; }

        public int MembershipId { get; set; }
        public Membership Membership { get; set; }

        
        [Required]
        public DateTime InvoiceDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvoiceAmount { get; set; }        

    }
}
