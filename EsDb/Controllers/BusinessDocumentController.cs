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



// app.MapGet("/", async () =>
// {
//     var docId = Guid.NewGuid();
//     var evt = new EventData(
//         Uuid.NewUuid(),
//         "BusinessDocumentOpened",
//         JsonSerializer.SerializeToUtf8Bytes(new BusinessDocumentOpened(docId, "Invoice 11221", "F011221",
//             250)));
//
//     await client.AppendToStreamAsync(
//         "BusinessDocs-"+docId,
//         StreamState.Any,
//         new[] { evt });
//     
//     return "Hello World!";
// });
//
// app.MapGet("/{id}", async (string id) => 
// {
//     var events = client.ReadStreamAsync(
//         Direction.Forwards,
//         "BusinessDocs-"+id,
//         StreamPosition.Start);
//     await foreach (var @event in events) {
//         Console.WriteLine(Encoding.UTF8.GetString(@event.Event.Data.ToArray()));
//     }
//
// });