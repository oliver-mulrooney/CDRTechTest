using CDR.Data.Queries.Interfaces;

namespace CDR.Data.Queries;
public class GetCDRsByDateRangeAndCallerIdQuery : IGetCDRsByDateRangeAndCallerIdQuery
{
    private readonly CDRContext _cdrContext;

    public GetCDRsByDateRangeAndCallerIdQuery(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public async Task<List<Entities.CDR>> Execute(string callerId, DateTime? startDate, DateTime? endDate)
    {
        return _cdrContext.CDRs.Where(x => x.CallDate >= startDate
        && x.CallDate <= endDate
        && x.CallerId == callerId).ToList();
    }
}
