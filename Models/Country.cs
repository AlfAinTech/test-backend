using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models
{
    public class Country
    {
        public int Id { get; set; } 

        [Required]
        public string CountryName { get; set; }

        public ICollection<State>? States { get; set; }

        public Country(string countryName)
        {
            CountryName = countryName;
        } 

    }
}
