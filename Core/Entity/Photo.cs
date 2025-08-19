using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;

public class Photo: BaseEntity
{
    public int PhotoId { get; set; }
    public int ProductId { get; set; }
    public required string Url { get; set; }  // URL of the photo

    // Navigation property
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }

}