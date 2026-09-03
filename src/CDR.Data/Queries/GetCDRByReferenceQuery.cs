using CDR.Data.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CDR.Data.Queries;
public class GetCDRByReferenceQuery : IGetCDRByReferenceQuery
{
    private readonly CDRContext _cdrContext;

    public GetCDRByReferenceQuery(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public async Task<Entities.CDR?> Execute(string cdrReference)
    {
        return await _cdrContext.CDRs.FirstOrDefaultAsync(x => x.Reference == cdrReference);
    }
}
