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
         res.Add(new BusinessDocument(evt.DocNumber, evt.DocAmount, evt.DocCurrency));
     }

     return res;
    }
    
    public async Task Save(BizDoc doc)
    {
     var evt = new EventData(
         Uuid.NewUuid(),
         "BusinessDocumentOpened",
         JsonSerializer.SerializeToUtf8Bytes(new BusinessDocumentOpened(doc.Id, doc.Number, doc.Version, doc.OccuringDate, doc.Amount, doc.Currency)));

     await _client.AppendToStreamAsync(
         "BusinessDocs",
         StreamState.Any,
         new[] { evt });
    }
}

public record BusinessDocumentOpened(Guid DocId, string DocNumber, int DocVersion, DateOnly DocOccuringDate, decimal DocAmount, string DocCurrency);