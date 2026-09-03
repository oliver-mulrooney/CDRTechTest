using CDR.Data.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR.Data.Queries;
public class GetMostExpensiveCDRsByDateRangeAndCallerIdQuery : IGetMostExpensiveCDRsByDateRangeAndCallerIdQuery
{
    private readonly CDRContext _cdrContext;

    public GetMostExpensiveCDRsByDateRangeAndCallerIdQuery(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public async Task<List<Entities.CDR>> Execute(DateTime startDate, DateTime endDate, string callerId, int amount)
    {
        return await _cdrContext.CDRs.Where(x => x.CallDate >= startDate  
        && x.CallDate <= endDate
        && x.CallerId == callerId)
        .OrderByDescending(x => x.Cost)
        .Take(amount)
        .ToListAsync();
    }
}
