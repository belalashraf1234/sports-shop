using Core.Dto;
using Core.Entity;


namespace Core.Specifications;

public class ProductSpecifications:BaseSpecification<DemoProduct>
{
    public ProductSpecifications(ProductSpecsParams specsParams): 
        base(x =>
            (string.IsNullOrEmpty(specsParams.Search) || x.Name.ToLower().Contains(specsParams.Search)) &&
            (specsParams.Brands.Count==0|| specsParams.Brands.Contains(x.Brand)) &&
            (specsParams.Categories.Count==0 || specsParams.Categories.Contains(x.Category))
        )
    
    {
        ApplyPagination(specsParams.PageSize *(specsParams.PageIndex-1),specsParams.PageSize);
        switch (specsParams.Sort)
        {
           case "PriceAsc":
               AddOrderBy(x=>x.Price);
               break;
           case "PriceDesc":
               AddOrderByDesc(x=>x.Price);
               break;
           default:
               AddOrderBy(x=>x.Name);
               break;
        }
        
        
    }
}