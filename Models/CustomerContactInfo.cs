using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp1.Entities;

namespace TestApp1.Models
{
    public class CustomerContactInfo
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey]
        public int CustomersId { get; set; }
        public Customers Customers { get; set; }

        [Required]
        [EnumDataType(typeof(ContactType))]
        public string ContactType { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string? ContactEmail { get; set; }

        [Phone]
        public string? ContactNumber { get; set; }

    }
}
