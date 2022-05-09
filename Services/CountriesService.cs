namespace TestApp1.Services;
using Microsoft.AspNetCore.Mvc;
using TestApp1.Models;
using TestApp1.Data;
using TestApp1.Controllers;

public interface ICountriesService
{
    IEnumerable<Country> GetAll();
    Country GetById(int id);
    void Create(NewCountryRequest model);
    void Update(int id);
    void Delete(int id);
}

public class CountryService : ICountriesService
{
    private DataContext _context;

    public CountryService(
        DataContext context)
    {
        _context = context;
    }

    public IEnumerable<Country> GetAll()
    {
        return _context.Countries;
    }

    public Country GetById(int id)
    {
        return getCountry(id);
    }

    public void Create(NewCountryRequest model)
    {
        Country newcountry = new(model.Cname);
        _context.Countries.Add(newcountry);
        _context.SaveChanges();
    }

    public void Update(int id)
    {
        
    }

    public void Delete(int id)
    {
        var country = getCountry(id);
        _context.Countries.Remove(country);
        _context.SaveChanges();
    }

    // helper methods

    private Country getCountry(int id)
    {
        var user = _context.Countries.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}