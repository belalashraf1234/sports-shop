using Core.Entity;
using Infrastrcture.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastrcture.Data;

public class StoreContext(DbContextOptions options): IdentityDbContext<AppUser>(options)
{
     
   
    public DbSet<DemoProduct> DemoProducts { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       
           
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfigurations).Assembly);
       
       
    }
}