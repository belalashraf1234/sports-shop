using Core.Dto;
using Core.Entity;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<ProductDto>> GetAllProductsAsync(string? BrandName, string? CategoryName, string? sort);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> CreateProductAsync(ProductImageUploadDto productDto);
    void UpdateProductAsync(Product productDto);
    void DeleteProductAsync(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
    Task<List<string>> GetAllBrands();
    Task<List<string>> GetAllCategories();

}