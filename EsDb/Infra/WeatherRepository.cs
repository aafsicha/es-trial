using EsDb.Domain;

namespace EsDb.Infra;

public class WeatherRepository : IWeatherRepository
{
    private readonly List<string> Summaries = new List<string>()
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public IEnumerable<WeatherForecast> All()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
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