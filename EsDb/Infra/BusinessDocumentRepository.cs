using System.Text.Json;
using EsDb.Domain;
using EventStore.Client;

namespace EsDb.Infra;

public class BusinessDocumentRepository : IBusinessDocumentRepository
{
    private readonly EventStoreClient _client;

    public BusinessDocumentRepository(EventStoreClient client)
    {
        _client = client;
    }
    
    public async Task<IEnumerable<BusinessDocument>> All()
    {
        var events = _client.ReadStreamAsync(
         Direction.Forwards,
         "BusinessDocs",
         StreamPosition.Start);
        var res = new List<BusinessDocument>();
     await foreach (var @event in events)
     {
         var evt = JsonSerializer.Deserialize<BusinessDocumentOpened>(@event.Event.Data.Span);
         res.Add(BusinessDocument.Hydrate(evt.DocId, evt.DocNumber, evt.DocAmount, evt.DocCurrency, evt.DocOccuringDate));
     }

     return res;
    }
    
    public async Task Save(BusinessDocument doc)
    {
        var listData = new List<EventData>();
        foreach (var businessDocumentEvent in doc.Events)
        {
            var evt = new EventData(
                Uuid.NewUuid(),
                typeof(IBusinessDocumentEvent).ToString(),
                JsonSerializer.SerializeToUtf8Bytes(businessDocumentEvent)); 
            listData.Add(evt);
        } 
        await _client.AppendToStreamAsync(
         "BusinessDocs",
         StreamState.Any,
         listData);
    }

    public Task<BusinessDocument> Fetch(Guid id)
    {
        throw new NotImplementedException();
    }
}

public interface IBusinessDocumentEvent
{
    
}
public record BusinessDocumentOpened(Guid DocId, string DocNumber, int DocVersion, DateOnly DocOccuringDate, decimal DocAmount, string DocCurrency) : IBusinessDocumentEvent;