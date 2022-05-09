using Microsoft.AspNetCore.Mvc;
using TestApp1.Services;
using TestApp1.Models;

namespace TestApp1.Controllers
{

    public class NewCountryRequest
    {
        public string Cname { get; set; }
        public string[]? Statenames { get; set; }
    }


    [Route("api/[action]")]
    [ApiController]
    public class CountryController : Controller
    {
        private ICountriesService _countriesService;
        public CountryController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        //[HttpGet("{id}")]
        public IActionResult GetCountriesList()
        {
            var countries = _countriesService.GetAll();
            return Ok(countries);

        }

        [HttpPost]
        public IActionResult AddCountry(NewCountryRequest country )
        {
            _countriesService.Create(country);
            return Ok(new { message = $"{country.Cname} with states {country.Statenames[0]} added to list" });
        }
    }


}
