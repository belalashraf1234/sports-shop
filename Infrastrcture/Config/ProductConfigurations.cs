using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastrcture.Config;

public class ProductConfigurations:IEntityTypeConfiguration<DemoProduct>
{
    public void Configure(EntityTypeBuilder<DemoProduct> builder)
    {
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x=>x.CreatedAt).HasDefaultValueSql("GETDATE()");
        
        
        
    }
}