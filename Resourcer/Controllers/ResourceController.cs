using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resourcer.Data.DB;
using Resourcer.Data.DTO;
using Resourcer.Helpers;

namespace Resourcer.Controllers;

[Route("api/requests/")]
[Produces("application/json")]
[ApiController]
public class ResourceController : ControllerBase
{
    private DataContext _ctx;

    public ResourceController(DataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    public async Task<IActionResult> GetResource([FromBody] Request item)
    {
        if (!item.IsValid())
            return Ok(new Response { Decision = "Denied", Resource = item.Resource, Reason = "Validation failed"});
        await Task.Delay(10000);
        
        var existResource = await _ctx.Resources.FirstOrDefaultAsync(t => t.Name == item.Resource);
        if (existResource is null)
            return Ok(new Response{ Decision = "Denied", Resource = item.Resource, Reason = "Resource not found"});
        return Ok(existResource.ToResponse());
    }
}

[Route("api/access/")]
[Produces("application/json")]
[ApiController]
public class AccessController : ControllerBase
{
    private DataContext _ctx;
    public AccessController(DataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    public async Task<IActionResult> SetAccess([FromBody] SetAccess item)
    {
        if (!item.IsValid())
            return Ok(new Response{ Decision = "Denied", Resource = item.Resource, Reason = "Validation failed"});
        
        var existResource = await _ctx.Resources.FirstOrDefaultAsync(t => t.Name == item.Resource);
        if (existResource is null)
            return Ok(new Response{ Decision = "Denied", Resource = item.Resource, Reason = "Resource not found"});

        if (existResource.Decision == item.Action) return Ok();
        existResource.Decision = item.Action;
        await _ctx.SaveChangesAsync();
        return Ok();
    }
}