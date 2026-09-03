using CDR.Data.Commands.Interfaces;

namespace CDR.Data.Commands;
public class AddCDRCommand : IAddCDRCommand
{
    private readonly CDRContext _cdrContext;

    public AddCDRCommand(CDRContext cdrContext)
    {
        _cdrContext = cdrContext;
    }

    public async Task<List<Entities.CDR>> Execute(List<Entities.CDR> cdrs)
    {
        await _cdrContext.CDRs.AddRangeAsync(cdrs);

        await _cdrContext.SaveChangesAsync();

        return cdrs;
    }
}
