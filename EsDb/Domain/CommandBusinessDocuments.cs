namespace EsDb.Domain;

public interface ICommandBusinessDocuments
{ 
    Task Create(BizDoc doc);
}

public record BizDoc(int Version, Guid Id, string Number, decimal Amount, string Currency, DateOnly OccuringDate);

public class CommandBusinessDocuments : ICommandBusinessDocuments
{
    private readonly IBusinessDocumentRepository _repository;

    public CommandBusinessDocuments(IBusinessDocumentRepository repository)
    {
        _repository = repository;
    }
    public async Task Create(BizDoc doc)
    {
        await _repository.Save(doc);
    }
}