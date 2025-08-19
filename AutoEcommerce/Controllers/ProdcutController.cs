

using Core.Entity;
using Core.Interfaces;
using Core.Specifications;

using Microsoft.AspNetCore.Mvc;


namespace AutoEcommerce.Controllers;

public class ProdcutController(IGenericRepository<DemoProduct> repo) : BaseApiController
{
    
    [Route("GetAllProducts")]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DemoProduct>>> GetAllProducts([FromQuery]ProductSpecsParams specsParams)
    {
        try
        { 
            var spec = new ProductSpecifications(specsParams);
           
            return await CreatePageResult(repo, spec, specsParams.PageIndex, specsParams.PageSize);
        }
        catch (Exception e)
        {
            return  BadRequest(e.Message);
        }
        
         
        
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DemoProduct>> GetProduct(int id)
    {
     var product = await repo.GetByIdAsync(id);
        return Ok(product);
       
    }

    [HttpPost]
    [Route("CreateProduct")]
    public async Task<ActionResult<DemoProduct>> CreateProduct([FromBody]DemoProduct product)
    {
        try
        {
            repo.Add(product);
            if (await repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
           
        }



        return BadRequest();
}
    public async Task<ActionResult<DemoProduct>> UpdateProduct(DemoProduct product)
    {
        repo.Update(product);
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest();
     
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecifivations();
        return Ok(await repo.ListAsync(spec));
    }
    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetCategories()
    {
        var spec = new CategoryListSpecifications();
        return Ok(await repo.ListAsync(spec));
    }
    public async Task<ActionResult<DemoProduct>> DeleteProduct(DemoProduct product)
    {
        repo.Delete(product);
        if (await repo.SaveChangesAsync())
        {
            return NoContent();
        }
        return BadRequest();
        
    }
}

