namespace TestApp1.Services;
using Microsoft.AspNetCore.Mvc;
using TestApp1.Models;
using TestApp1.Data;
using TestApp1.Controllers;
using TestApp1.Entities;


public interface ICustomerService
{
    object GetAll();
    Customers GetById(int id);
    void Create(NewCustomerRequest model);
    void Update( NewCustomerRequest model);
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
                          c.MembershipInfo,
                          nextInvoiceFlag = mem.MembershipType == MembershipType.Prepay.ToString() ? mem.BillingFrequency == BillingFrequency.Weekly.ToString() ? true: mem.BillingFrequency == BillingFrequency.Monthly.ToString() ?true : false : false,
                          nextInvoice =  mem.MembershipType == MembershipType.Prepay.ToString() ? mem.BillingFrequency == BillingFrequency.Weekly.ToString() ? mem.StartDate.AddDays(7) : mem.BillingFrequency == BillingFrequency.Monthly.ToString() ? mem.StartDate.AddMonths(1) : new DateTime() : new DateTime(),                          
                          c.FirstName,
                          c.LastName,
                          c.Address,
                          c.Email,
                          c.DateOfBirth,
                          c.ContactInfo
                          
                      }).ToList();
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
        return json;
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

    public void Update( NewCustomerRequest model)
    {
        Customers customer = _context.Customers.FirstOrDefault(x => x.CustomerId == model.id);
        if(customer == null)
        {
            return;
        }
        else
        {
            //update customer object
            customer.FirstName = model.firstName;
            customer.LastName = model.lastName;
            customer.Email = model.Email;
            customer.DateOfBirth = model.DateOfBirth;

            if (model.ContactInfo is not null)
            {
                foreach (NewContactInfoRequest contact in model.ContactInfo)
                { 

                    CustomerContactInfo contactInfo = contact.GetType().GetProperty("Id") == null ? new CustomerContactInfo() : _context.ContactInfo.FirstOrDefault(x => x.Id == contact.Id);


                    contactInfo.ContactType = contact.ContactType;
                    contactInfo.Customers = customer;
                    contactInfo.ContactEmail = contact.ContactEmail;
                    contactInfo.ContactNumber = contact.ContactNumber;
                    if (contact.GetType().GetProperty("Id") == null)
                        _context.Customers.Add(customer);

                }
            }
            if (model.Address is not null)
            {
                foreach (NewAddressInfoRequest address in model.Address)
                {
                    CustomerAddress addressInfo = address.GetType().GetProperty("Id") == null ? new CustomerAddress() : _context.Address.FirstOrDefault(x => x.Id == address.Id);
                    addressInfo.AddressType = address.AddressType;
                    addressInfo.Customers = customer;
                    addressInfo.AddressLine1 = address.AddressLine1;
                    addressInfo.AddressLine2 = address.AddressLine2;
                    addressInfo.City = address.City;
                    addressInfo.CountryId = address.CountryId;
                    addressInfo.StateId = address.StateId;
                    addressInfo.ZipCode = address.ZipCode;
                    if(address.GetType().GetProperty("Id") == null)
                    _context.Address.Add(addressInfo);

                }
            }
            if (model.MembershipInfo is not null)
            {
                Membership memberShip = model.MembershipInfo.GetType().GetProperty("Id") == null ? new Membership() : _context.Memberships.FirstOrDefault(x => x.Id == model.MembershipInfo.Id);
                memberShip.MembershipPrice = model.MembershipInfo.MembershipPrice;
                memberShip.MembershipType = model.MembershipInfo.MembershipType;
                memberShip.StartDate = model.MembershipInfo.StartDate;
                memberShip.BillingFrequency = model.MembershipInfo.BillingFrequency;
                memberShip.EndDate = model.MembershipInfo.EndDate;
                memberShip.Customers = customer;
                if(model.MembershipInfo.GetType().GetProperty("Id") == null)
                _context.Memberships.Add(memberShip);
            }
            //_context.Customers.Add(newCustomer);
            _context.SaveChanges();


        }

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