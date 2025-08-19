namespace Core.Dto;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public  string PartNumber { get; set; }
    public  string VehicleMake { get; set; }
    public  string VehicleModel { get; set; }
    public  string YearCompatible { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public List<string> PhotoUrls { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    

}