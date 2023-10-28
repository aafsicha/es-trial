using EsDb.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EsDb.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessDocumentController : ControllerBase
{
    [HttpGet(Name = "All")]
    public async Task<IEnumerable<BusinessDocument>> Get(
        [FromServices] IQueryBusinessDocuments queryBusinessDocuments)
    {
        return await queryBusinessDocuments.GetAll();
    }

    [HttpPost]
    public async Task Post(
        [FromBody] BizDoc doc, 
        [FromServices] ICommandBusinessDocuments commandBusinessDocuments)
    {
        await commandBusinessDocuments.Create(doc);
    }
    
    [HttpPost("{id}:postpone")]
    public async Task Postpone(
        [FromRoute] Guid id,
        [FromBody] DateOnly newDate, 
        [FromServices] ICommandBusinessDocuments commandBusinessDocuments)
    {
        await commandBusinessDocuments.Postpone(id, newDate);
    }
    
    [HttpPost("{id}:adjust_amount")]
    public async Task AdjustAmount(
        [FromRoute] Guid id,
        [FromBody] decimal newAmount, 
        [FromServices] ICommandBusinessDocuments commandBusinessDocuments)
    {
        await commandBusinessDocuments.AdjustAmount(id, newAmount);
    }
    
}