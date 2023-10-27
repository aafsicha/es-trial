namespace EsDb.Domain;

public interface IQueryWeathers
{
    IEnumerable<WeatherForecast> GetWeathers();
}

public class QueryWeathers : IQueryWeathers
{
    private readonly IWeatherRepository _repository;

    public QueryWeathers(IWeatherRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<WeatherForecast> GetWeathers()
    {
        return _repository.All();
    }
}