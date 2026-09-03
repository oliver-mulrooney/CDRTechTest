using CDR.Model.Responses;
using Microsoft.AspNetCore.Http;

namespace CDR.Service.Services.Interfaces;
public interface ICDRService
{
    Task<CDRUploadSummaryResponse> CreateCdrsFromCsv(IFormFile cdrFile);

    Task<Data.Entities.CDR?> GetCdrByReference(string cdrReference);

    Task<CDRReportResponse> GetCdrReport(DateTime startDate, DateTime endDate);

    Task<List<Data.Entities.CDR>> GetCdrsByCallerIdAndDate(string callerId, DateTime startDate, DateTime endDate);

    Task<List<Data.Entities.CDR>> GetMostExpensiveCallsByDateRangeAndCallerId(string callerId, DateTime startDate, DateTime endDate, int amount);
}
