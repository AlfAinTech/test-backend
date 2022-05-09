using Microsoft.AspNetCore.Mvc;
using TestApp1.Entities;

namespace TestApp1.Controllers
{

    [Route("api/[action]")]
    [ApiController]
    public class FormHelperController : Controller
    {
        [HttpGet]
        public IActionResult GetAddressTypes()
        {
            var addresstypes = Enum.GetNames((typeof(AddressType)));
            return Ok(addresstypes);

        }

        [HttpGet]
        public IActionResult GetBillingFrequency()
        {
            var billingfreq = Enum.GetNames((typeof(BillingFrequency)));
            return Ok(billingfreq);

        }

        [HttpGet]
        public IActionResult GetContactType()
        {
            var contacttypes = Enum.GetNames((typeof(ContactType)));
            return Ok(contacttypes);

        }

        [HttpGet]
        public IActionResult GetMembershipType()
        {
            var membershiptypes = Enum.GetNames((typeof(MembershipType)));
            return Ok(membershiptypes);

        }
    }
}
