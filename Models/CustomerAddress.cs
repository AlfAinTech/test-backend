using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp1.Entities;

namespace TestApp1.Models
{
    public class CustomerAddress
    {
        [Key]
        public int Id { get; set; } 

        //[ForeignKey]
        public int CustomersId { get; set; }
        public Customers Customers { get; set; }

        [Required]
        [EnumDataType(typeof(AddressType))]
        public string AddressType { get; set; }

        [Required]
        [MaxLength(50)]
        public string AddressLine1 { get; set; }

        [MaxLength(50)]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(20)]
        public string City { get; set; }
        [Required]
        public string CountryId { get; set; }
        [Required]
        public string StateId { get; set; }
        [Required]
        public int ZipCode { get; set; }

        
    }
}
