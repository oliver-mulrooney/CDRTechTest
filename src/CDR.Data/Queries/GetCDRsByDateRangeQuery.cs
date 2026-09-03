using CDR.Data.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CDR.Data.Queries;
public class GetCDRsByDateRangeQuery : IGetCDRsByDateRangeQuery
{
    private readonly CDRContext _cdrContext;

    public GetCDRsByDateRangeQuery(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public Task<List<Entities.CDR>> Execute(DateTime startDate, DateTime endDate)
    {
        return _cdrContext.CDRs.Where(x => x.CallDate >= startDate && x.CallDate <= endDate).ToListAsync();
    }
}
