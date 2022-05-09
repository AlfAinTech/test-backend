using Microsoft.AspNetCore.Mvc;
using TestApp1.Services;
using TestApp1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Net;
using System.Configuration;

namespace TestApp1.Controllers
{
    public class NewContactInfoRequest
    {
        public string ContactType { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
    }
    public class NewAddressInfoRequest
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public String? AddressLine2 { get; set; }
        public String City { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public int ZipCode { get; set; }

    }
    public class NewMemberShipInfoRequest
    {
        public string MembershipType { get; set; }
        public string? BillingFrequency { get; set; }
        public decimal MembershipPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


    public class NewCustomerRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public String Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<NewAddressInfoRequest>? Address { get; set; }
        public ICollection<NewContactInfoRequest>? ContactInfo { get; set; }
        public NewMemberShipInfoRequest? MembershipInfo { get; set; }
    }


    [Route("api/[action]")]
   // [EnableCors("Policy1")]
    [ApiController]
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [EnableCors("Policy1")]
        
        //[HttpGet("{id}")]
        public IActionResult GetCustomersList()
        {
            var customers = _customerService.GetAll();
            return Json(customers);
           

        }

        [HttpPost]
        [EnableCors("Policy1")]
        public IActionResult AddCustomer(NewCustomerRequest customer )
        {
            
            _customerService.Create(customer);

              return Json(new { message = $"{customer.firstName} {customer.lastName} added to list" });
          //  return Request.CreateResponse(HttpStatusCode.OK, customer, Configuration.Formatters.JsonFormatter);

        }
    }


}
