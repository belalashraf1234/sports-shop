using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;


    public class Product : BaseEntity
    {
      

        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
       
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public required string PartNumber { get; set; }
        public required string VehicleMake { get; set; }
        public required string VehicleModel { get; set; }
        public required string YearCompatible { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        
      
        // Navigation properties
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        
        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }
        
        public  ICollection<Photo>? Photos { get; set; }
      
    }
