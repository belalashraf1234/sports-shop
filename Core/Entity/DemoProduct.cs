using System.ComponentModel.DataAnnotations;

namespace Core.Entity;

public class DemoProduct:BaseEntity
{
    [Required]
    public  string Name { get; set; }
    [Required]
    public  string Description { get; set; }
    [Range(0,5000)]
    public decimal Price { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string ImgUrl { get; set; }

    public  string? PartNumber { get; set; }
    public  string? VehicleMake { get; set; }
    public  string? VehicleModel { get; set; }
    public  string? YearCompatible { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
}