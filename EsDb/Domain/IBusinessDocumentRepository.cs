namespace EsDb.Domain;

public interface IBusinessDocumentRepository
{
    Task<IEnumerable<BusinessDocument>> All();
    Task Save(BizDoc doc);
}

public record BusinessDocument(string Number, decimal Amount, string Currency);