namespace Core.Entity;

public class Brand: BaseEntity
{
    
    public int BrandId { get; set; }
    public required string BrandName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation property
    public ICollection<Product>? Products { get; set; }
}