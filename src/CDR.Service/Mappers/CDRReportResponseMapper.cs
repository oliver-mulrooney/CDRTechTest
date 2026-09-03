using CDR.Model.Responses;
using CDR.Service.Mappers.Interfaces;

namespace CDR.Service.Mappers;
public class CDRReportResponseMapper : ICDRReportResponseMapper
{
    public CDRReportResponse Map(List<Data.Entities.CDR> cdrs)
    {
        var callAmount = cdrs.Count;

        var totalCallDuration = cdrs.Sum(x => x.Duration);

        return new CDRReportResponse()
        {
            TotalCallDuration = totalCallDuration,
            CallAmount = callAmount
        };
    }
}
