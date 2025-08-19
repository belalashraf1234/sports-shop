using Core.Entity;

namespace Core.Specifications;

public class BrandListSpecifivations:BaseSpecification<DemoProduct,string>
{
    public BrandListSpecifivations()
    {
        AddSelect(x=>x.Brand);
        AddDistinct();
        
    }
}