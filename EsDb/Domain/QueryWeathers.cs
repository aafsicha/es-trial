namespace EsDb.Domain;

public interface IQueryBusinessDocuments
{
    Task<IEnumerable<BusinessDocument>> GetAll();
}

public class QueryBusinessDocuments : IQueryBusinessDocuments
{
    private readonly IBusinessDocumentRepository _repository;

    public QueryBusinessDocuments(IBusinessDocumentRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<BusinessDocument>> GetAll()
    {
        return await _repository.All();
    }
}