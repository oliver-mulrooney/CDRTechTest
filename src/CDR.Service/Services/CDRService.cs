using CDR.Data.Commands.Interfaces;
using CDR.Model.Models.CSV;
using CDR.Model.Responses;
using CDR.Service.Mappers;
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
    private readonly ICDRCsvRecordsToEntitiesMapper _cdrCsvRecordsToEntitiesMapper;

    public CDRService(IAddCDRCommand addCDRCommand,
        ICDRUploadSummaryMapper cdrUploadSummaryMapper,
        ICDRCsvRecordsToEntitiesMapper cdrCsvRecordsToEntitiesMapper)
    {
        _addCDRCommand = addCDRCommand;
        _cdrUploadSummaryMapper = cdrUploadSummaryMapper;
        _cdrCsvRecordsToEntitiesMapper = cdrCsvRecordsToEntitiesMapper;
    }

    public async Task<CDRUploadSummaryResponse> CreateCdrsFromCsv(IFormFile cdrFile)
    {
        try
        {
            using var stream = cdrFile.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var cdrs = csvReader.GetRecords<CDRCsvRecord>().ToList();

            var cdrEntities = _cdrCsvRecordsToEntitiesMapper.Map(cdrs);

            var commandResult = await _addCDRCommand.Execute(cdrEntities);

            var CdrSummary = _cdrUploadSummaryMapper.Map(commandResult, true, null);

            return CdrSummary;
        }
        catch (Exception ex) 
        {
            var CdrSummary = _cdrUploadSummaryMapper.Map(null, false, ex.Message);
            return CdrSummary;
        }
    }
}
