using System.ComponentModel.DataAnnotations;

namespace AutoEcommerce.Dtos;

public class CreateProduct
{   [Required]
    public string Name { get; set; }=string.Empty;
    
    public  string Description { get; set; }=string.Empty;
    
    [Range(0.01, double.MaxValue,ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }
    [Required] 
    public string Category { get; set; }= string.Empty;
    [Required] 
    public string Brand { get; set; }= string.Empty;
    [Required] 
    public string ImgUrl { get; set; } = string.Empty;
    [Required] 
    public  string PartNumber { get; set; }= string.Empty;
    [Required] 
    public  string VehicleMake { get; set; }= string.Empty;
    [Required] 
    public  string VehicleModel { get; set; }= string.Empty;
    [Required] 
    public  string YearCompatible { get; set; }= string.Empty;
    public DateTime CreatedAt { get; set; }=DateTime.Now;
    
    public DateTime? UpdatedAt { get; set; }
}