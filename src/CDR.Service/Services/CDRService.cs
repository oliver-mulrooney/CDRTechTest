using CDR.Data.Commands.Interfaces;
using CDR.Model.Responses;
using CDR.Service.Mappers.Interfaces;
using CDR.Service.Services.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CDR.Service.Services;
public class CDRService : ICDRService
{
    private readonly IAddCDRCommand _addCDRCommand;
    private readonly ICDRUploadSummaryMapper _cdrUploadSummaryMapper;

    public CDRService(IAddCDRCommand addCDRCommand,
        ICDRUploadSummaryMapper cdrUploadSummaryMapper)
    {
        _addCDRCommand = addCDRCommand;
        _cdrUploadSummaryMapper = cdrUploadSummaryMapper;
    }

    public async Task<CDRUploadSummaryResponse> CreateCdrsFromCsv(IFormFile cdrFile)
    {
        try
        {
            using var stream = cdrFile.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var cdrs = csvReader.GetRecords<Data.Entities.CDR>().ToList();

            var commandResult = await _addCDRCommand.Execute(cdrs);

            var CdrSummary = _cdrUploadSummaryMapper.Map(cdrs, true, null);

            return CdrSummary;
        }
        catch (Exception ex) 
        {
            var CdrSummary = _cdrUploadSummaryMapper.Map(null, false, ex.Message);
            return CdrSummary;
        }
    }
}
