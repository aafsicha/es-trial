namespace EsDb.Domain;

public interface ICommandWeathers
{ 
    Task Create(string name);
}

public class CommandWeathers : ICommandWeathers
{
    private readonly IWeatherRepository _repository;

    public CommandWeathers(IWeatherRepository repository)
    {
        _repository = repository;
    }
    public async Task Create(string name)
    {
        await _repository.Save(name);
    }
}