using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;


public class Category: BaseEntity
{
    public int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public int? ParentId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Self-referencing relationship for subcategories
    [ForeignKey("ParentId")]
    public Category? ParentCategory { get; set; }
    public ICollection<Category>? Subcategories { get; set; }

    // Navigation property
    public ICollection<Product>? Products { get; set; }
}

