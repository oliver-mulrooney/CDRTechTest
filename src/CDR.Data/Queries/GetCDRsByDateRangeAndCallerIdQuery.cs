using CDR.Data.Enums;
using CDR.Data.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CDR.Data.Queries;
public class GetCDRsByDateRangeAndCallerIdQuery : IGetCDRsByDateRangeAndCallerIdQuery
{
    private readonly CDRContext _cdrContext;

    public GetCDRsByDateRangeAndCallerIdQuery(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public async Task<List<Entities.CDR>> Execute(string callerId, DateTime? startDate, DateTime? endDate, CallTypeEnum? callType)
    {
        return await _cdrContext.CDRs.Where(x => x.CallDate >= startDate
        && x.CallDate <= endDate
        && x.CallerId == callerId
        && (x.CallType == callType || callType == null)).ToListAsync();
    }
}
