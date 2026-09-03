using CDR.Model.Responses;
using CDR.Service.Mappers.Interfaces;

namespace CDR.Service.Mappers;
public class CDRUploadSummaryMapper : ICDRUploadSummaryMapper
{
    public CDRUploadSummaryResponse Map(List<Data.Entities.CDR>? CDRs, bool IsSuccessful, string? ErrorMessage)
    {
        return new CDRUploadSummaryResponse
        {
            TotalRecordsUploaded = CDRs.Count,
            IsSuccessful = IsSuccessful,
            ErrorMessage = ErrorMessage
        };
    }
}
