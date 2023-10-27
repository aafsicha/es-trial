namespace EsDb.Domain;

public interface ICommandBusinessDocuments
{ 
    Task Create(string name);
}

public class CommandBusinessDocuments : ICommandBusinessDocuments
{
    private readonly IBusinessDocumentRepository _repository;

    public CommandBusinessDocuments(IBusinessDocumentRepository repository)
    {
        _repository = repository;
    }
    public async Task Create(string name)
    {
        await _repository.Save(name);
    }
}