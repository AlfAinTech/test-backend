namespace TestApp1.Services;
using Microsoft.AspNetCore.Mvc;
using TestApp1.Models;
using TestApp1.Data;
using TestApp1.Controllers;
using TestApp1.Entities;

public class CustomersListItems
{
    public string CustomerId;
    public string MembershipType;
    public string BillingFrequency;
    public bool nextInvoiceFlag;
    public DateTime nextInvoice;
    public string FirstName;
    public string LastName;
}
public interface ICustomerService
{
    object GetAll();
    Customers GetById(int id);
    void Create(NewCustomerRequest model);
    void Update(int id);
    void Delete(int id);
}

public class CustomerService : ICustomerService
{
    private DataContext _context;

    public CustomerService(
        DataContext context)
    {
        _context = context;
    }

    public object GetAll()
    {
        // return _context.Customers.ToList();
       var result = (from c in _context.Customers
                      join mem in _context.Memberships on c.CustomerId equals mem.CustomersId
                     orderby c.CustomerId descending
                     select new  
                      {
                          c.CustomerId,
                          mem.MembershipType,
                          mem.BillingFrequency,
                          nextInvoiceFlag = mem.MembershipType == MembershipType.Prepay.ToString() ? mem.BillingFrequency == BillingFrequency.Weekly.ToString() ? true: mem.BillingFrequency == BillingFrequency.Monthly.ToString() ?true : false : false,
                          nextInvoice =  mem.MembershipType == MembershipType.Prepay.ToString() ? mem.BillingFrequency == BillingFrequency.Weekly.ToString() ? mem.StartDate.AddDays(7) : mem.BillingFrequency == BillingFrequency.Monthly.ToString() ? mem.StartDate.AddMonths(1) : new DateTime() : new DateTime(),                          
                          c.FirstName,
                          c.LastName,
                          c.Address,
                          c.ContactInfo
                          
                      }).ToList();
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
        return result;
    }

    public Customers GetById(int id)
    {
        return getCustomer(id);
    }

    public void Create(NewCustomerRequest model)
    {
        Customers newCustomer = new(model.firstName, model.lastName, model.Email, model.DateOfBirth);
        //model.ContactInfo?.ToList().ForEach(b => return { b.CustomersId = newCustomer.CustomerId; b.Customers = newCustomer; })  ;
        if (model.ContactInfo is not null) {
            foreach (NewContactInfoRequest contact in model.ContactInfo)
            {
                CustomerContactInfo contactInfo = new CustomerContactInfo();
                contactInfo.ContactType = contact.ContactType;
                contactInfo.Customers = newCustomer;
                contactInfo.ContactEmail = contact.ContactEmail;
                contactInfo.ContactNumber = contact.ContactNumber;
                _context.ContactInfo.Add(contactInfo);

            } }
        if(model.Address is not null) {
            foreach (NewAddressInfoRequest address in model.Address)
            {
                CustomerAddress addressInfo = new CustomerAddress();
                addressInfo.AddressType = address.AddressType;
                addressInfo.Customers = newCustomer;
                addressInfo.AddressLine1 = address.AddressLine1;
                addressInfo.AddressLine2 = address.AddressLine2;
                addressInfo.City = address.City;
                addressInfo.CountryId = address.CountryId;
                addressInfo.StateId = address.StateId;
                addressInfo.ZipCode = address.ZipCode;
                _context.Address.Add(addressInfo);

            }
        }
        if (model.MembershipInfo is not null)
        {
        
            Membership memberShip = new Membership();
            memberShip.MembershipPrice = model.MembershipInfo.MembershipPrice;
            memberShip.MembershipType = model.MembershipInfo.MembershipType;
            memberShip.StartDate = model.MembershipInfo.StartDate;
            memberShip.BillingFrequency = model.MembershipInfo.BillingFrequency;
            memberShip.EndDate = model.MembershipInfo.EndDate;
            memberShip.Customers = newCustomer;
            _context.Memberships.Add(memberShip);
        }
        _context.Customers.Add(newCustomer);
        _context.SaveChanges();
    }

    public void Update(int id)
    {
        
    }

    public void Delete(int id)
    {
        var country = getCustomer(id);
        _context.Customers.Remove(country);
        _context.SaveChanges();
    }

    // helper methods

    private Customers getCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null) throw new KeyNotFoundException("User not found");
        return customer;
    }
}