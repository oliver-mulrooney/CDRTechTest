using CDR.Model.Responses;
using Microsoft.AspNetCore.Http;

namespace CDR.Service.Services.Interfaces;
public interface ICDRService
{
    Task<CDRUploadSummaryResponse> CreateCdrsFromCsv(IFormFile cdrFile);
}
