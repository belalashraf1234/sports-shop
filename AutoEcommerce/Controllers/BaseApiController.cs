using AutoEcommerce.RequestHelpers;
using Core.Entity;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoEcommerce.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : Controller
{
    // GET
    protected async Task<ActionResult> CreatePageResult<T>(IGenericRepository<T> repo,
        ISpcification<T> spec, int pageIndex, int pageSize)where T:BaseEntity
    {
        var items= await repo.ListAsync(spec);
        var count = await repo.CountAsync(spec);
        var pagination = new Pagination<T>(pageIndex, pageSize, count, items);
        return Ok(pagination);
    }
}