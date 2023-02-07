using System.Collections.Generic;
using AMP.Domain.ValueObjects.Base;

namespace AMP.Domain.ValueObjects;

public class MomoTransfer : ValueObject
{
    private MomoTransfer(string number, string networkProvider)
    {
        Number = number;
        NetworkProvider = networkProvider;
    }
    public string Number { get; private set; }
    public string NetworkProvider { get; private set; }

    public static MomoTransfer Create(string number, string networkProvider = "MTN")
        => new(number, networkProvider);
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
        yield return NetworkProvider;
    }
}