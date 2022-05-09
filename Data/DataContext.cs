
using Microsoft.EntityFrameworkCore;
using TestApp1.Models;

namespace TestApp1.Data;
public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(DbContextOptions<DataContext> options)
            : base(options)
    {
    }

    public DbSet<Customers> Customers { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<CustomerAddress> Address { get; set; }
    public DbSet<CustomerContactInfo> ContactInfo { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Invoices> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customers>()
            .HasOne(b => b.MembershipInfo)
            .WithOne(i => i.Customers)
            .HasForeignKey<Membership>(b => b.CustomersId);
    }

}