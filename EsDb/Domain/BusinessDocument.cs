using EsDb.Infra;

namespace EsDb.Domain;

public record BusinessDocument
{
    private readonly Guid _id;
    private readonly string _number;
    private readonly decimal _amount;
    private readonly string _currency;
    private readonly DateOnly _occuringDate;
    private readonly List<IBusinessDocumentEvent> _events = new List<IBusinessDocumentEvent>();

    private BusinessDocument(Guid Id, string Number, decimal Amount, string Currency, DateOnly OccuringDate, CreationReason reason)
    {
        _id = Id;
        _number = Number;
        _amount = Amount;
        _currency = Currency;
        _occuringDate = OccuringDate;
        switch (reason)
        {
            case CreationReason.Creation: _events.Add(new BusinessDocumentOpened(Id,Number, 0, OccuringDate, Amount, Currency));
                break;
            case CreationReason.Hydratation:
                break;
        }
    }
    public static BusinessDocument Create(BizDoc doc)
    {
        return new BusinessDocument(doc.Id, doc.Number, doc.Amount, doc.Currency, doc.OccuringDate, CreationReason.Creation);
    }
    
    public static BusinessDocument Hydrate(Guid id, string number, decimal amount, string currency, DateOnly occuringDate)
    {
        return new BusinessDocument(id, number, amount, currency, occuringDate, CreationReason.Hydratation);
    }

    public IReadOnlyCollection<IBusinessDocumentEvent> Events => _events;

    public void Postpone(DateOnly newDate)
    {
        _events.Add(new BusinessDocumentPostponed(_id, newDate));
    }

    public void AdjustAmount(decimal newAmount)
    {
        _events.Add(new BusinessDocumentAmountAdjusted(_id, newAmount));
    }
}

public record BusinessDocumentAmountAdjusted(Guid Id, decimal NewAmount) : IBusinessDocumentEvent;

public record BusinessDocumentPostponed(Guid Id, DateOnly NewDate) : IBusinessDocumentEvent;