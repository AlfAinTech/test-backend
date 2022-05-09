using Microsoft.AspNetCore.Mvc;

namespace TestApp1.Controllers
{

    [Route("api/[action]")]
    [ApiController]
    public class ListingController : Controller
    {
        [HttpGet]
        [HttpGet("{id}")]
        public IActionResult GetPaginatedList(int id)
        {
            return Ok(new { message = $"List of page {id}" });

        }
    }
}
