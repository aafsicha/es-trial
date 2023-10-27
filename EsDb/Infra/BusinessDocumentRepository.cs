using EsDb.Domain;

namespace EsDb.Infra;

public class BusinessDocumentRepository : IBusinessDocumentRepository
{
    private readonly List<string> Summaries = new List<string>()
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public IEnumerable<BusinessDocument> All()
    {
        return Enumerable.Range(1, 5).Select(index => new BusinessDocument
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Count)]
            })
            .ToArray();
    }
    
    public async Task Save(string temperature)
    {
        Summaries.Add(temperature);
    }
}