namespace EsDb.Domain;

public interface IBusinessDocumentRepository
{
    IEnumerable<BusinessDocument> All();
    Task Save(string temperature);
}