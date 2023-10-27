namespace EsDb.Domain;

public interface IQueryBusinessDocuments
{
    IEnumerable<BusinessDocument> GetWeathers();
}

public class QueryBusinessDocuments : IQueryBusinessDocuments
{
    private readonly IBusinessDocumentRepository _repository;

    public QueryBusinessDocuments(IBusinessDocumentRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<BusinessDocument> GetWeathers()
    {
        return _repository.All();
    }
}