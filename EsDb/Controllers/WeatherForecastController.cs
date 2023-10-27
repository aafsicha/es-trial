using EsDb.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EsDb.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IQueryWeathers _queryWeathers;
    private readonly ICommandWeathers _commandWeathers;

    public WeatherForecastController(IQueryWeathers queryWeathers, ICommandWeathers commandWeathers)
    {
        _queryWeathers = queryWeathers;
        _commandWeathers = commandWeathers;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _queryWeathers.GetWeathers();
    }

    [HttpPost(Name = "PostWeatherForecast/{name}")]
    public async Task Post([FromRoute] string name)
    {
        await _commandWeathers.Create(name);
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