namespace EsDb.Domain;

public interface ICommandBusinessDocuments
{ 
    Task Create(BizDoc doc);
    Task Postpone(Guid id, DateOnly newDate);
    Task AdjustAmount(Guid id, decimal newAmount);
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
        var bizDoc = BusinessDocument.Create(doc);
        await _repository.Save(bizDoc);
    }

    public async Task Postpone(Guid id, DateOnly newDate)
    {
        var doc = await _repository.Fetch(id);
        doc.Postpone(newDate);
        await _repository.Save(doc);
    }

    public async Task AdjustAmount(Guid id, decimal newAmount)
    {
        var doc = await _repository.Fetch(id);
        doc.AdjustAmount(newAmount);
        await _repository.Save(doc);
    }
}