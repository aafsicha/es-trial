namespace EsDb.Domain;

public interface IWeatherRepository
{
    IEnumerable<WeatherForecast> All();
    Task Save(string temperature);
}