using Core.Entity;

namespace Core.Specifications;

public class CategoryListSpecifications : BaseSpecification<DemoProduct, string>
{
    public CategoryListSpecifications()
    {
        AddSelect(x => x.Category);
        AddDistinct();
    }

}