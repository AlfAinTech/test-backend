using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models
{
    public class State
    {
        public int Id { get; set; } 

        [Required]
        public string StateName { get; set; }

        public int CountryId { get; set; }
        public Country? Country { get; set; }

        // This constructor is to be used when adding state for existing Country
        public State(string stateName, int countryId)
        {
            StateName = stateName;
            CountryId = countryId;
        }
        // This constructor is for state while simultaneously creating country
        public State(string stateName)
        {
            StateName = stateName;
        }
    }
}
