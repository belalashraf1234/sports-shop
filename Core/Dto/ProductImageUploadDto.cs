using Microsoft.AspNetCore.Http;

namespace Core.Dto;

public class ProductImageUploadDto
{   public  string Name { get; set; }
    public  string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public  string PartNumber { get; set; }
    public  string VehicleMake { get; set; }
    public  string VehicleModel { get; set; }
    public  string YearCompatible { get; set; }
    public IFormFileCollection Images { get; set; }
    
}