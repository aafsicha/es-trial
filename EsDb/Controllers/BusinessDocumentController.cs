using EsDb.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EsDb.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessDocumentController : ControllerBase
{
    private readonly IQueryBusinessDocuments _queryBusinessDocuments;
    private readonly ICommandBusinessDocuments _commandBusinessDocuments;

    public BusinessDocumentController(IQueryBusinessDocuments queryBusinessDocuments, ICommandBusinessDocuments commandBusinessDocuments)
    {
        _queryBusinessDocuments = queryBusinessDocuments;
        _commandBusinessDocuments = commandBusinessDocuments;
    }

    [HttpGet(Name = "All")]
    public IEnumerable<BusinessDocument> Get()
    {
        return _queryBusinessDocuments.GetWeathers();
    }

    [HttpPost(Name = "{name}")]
    public async Task Post([FromRoute] string name)
    {
        await _commandBusinessDocuments.Create(name);
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