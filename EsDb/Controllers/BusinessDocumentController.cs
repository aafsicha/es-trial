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
}