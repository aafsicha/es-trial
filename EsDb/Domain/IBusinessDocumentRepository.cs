namespace EsDb.Domain;

public interface IBusinessDocumentRepository
{
    Task<IEnumerable<BusinessDocument>> All();
    Task Save(BusinessDocument doc);
    Task<BusinessDocument> Fetch(Guid id);
}

public enum CreationReason
{
    Creation,
    Hydratation
}