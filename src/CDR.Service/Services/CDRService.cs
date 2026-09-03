using CDR.Data.Commands.Interfaces;
using CDR.Data.Queries;
using CDR.Data.Queries.Interfaces;
using CDR.Model.Models.CSV;
using CDR.Model.Responses;
using CDR.Service.Mappers;
using CDR.Service.Mappers.Interfaces;
using CDR.Service.Services.Interfaces;
using CDR.Service.Validators.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CDR.Service.Services;
public class CDRService : ICDRService
{
    private readonly IAddCDRCommand _addCDRCommand;
    private readonly IGetCDRByReferenceQuery _getCDRByReferenceQuery;
    private readonly ICDRUploadSummaryMapper _cdrUploadSummaryMapper;
    private readonly ICDRCsvRecordsToEntitiesMapper _cdrCsvRecordsToEntitiesMapper;
    private readonly ICDRReportQueryValidator _cdrReportQueryValidator;
    private readonly IGetCDRsByDateRangeQuery _getCDRsByDateRangeQuery;
    private readonly ICDRReportResponseMapper _cdrReportResponseMapper;
    private readonly IGetCDRsByDateRangeAndCallerIdQuery _getCDRsByDateRangeAndCallerIdQuery;


    public CDRService(IAddCDRCommand addCDRCommand,
        IGetCDRByReferenceQuery getCDRByReferenceQuery,
        ICDRUploadSummaryMapper cdrUploadSummaryMapper,
        ICDRCsvRecordsToEntitiesMapper cdrCsvRecordsToEntitiesMapper,
        ICDRReportQueryValidator cdrReportQueryValidator,
        IGetCDRsByDateRangeQuery getCDRsByDateRangeQuery,
        ICDRReportResponseMapper cdrReportResponseMapper,
        IGetCDRsByDateRangeAndCallerIdQuery getCDRsByDateRangeAndCallerIdQuery)
    {
        _addCDRCommand = addCDRCommand;
        _cdrUploadSummaryMapper = cdrUploadSummaryMapper;
        _cdrCsvRecordsToEntitiesMapper = cdrCsvRecordsToEntitiesMapper;
        _getCDRByReferenceQuery = getCDRByReferenceQuery;
        _cdrReportQueryValidator = cdrReportQueryValidator;
        _getCDRsByDateRangeQuery = getCDRsByDateRangeQuery;
        _cdrReportResponseMapper = cdrReportResponseMapper;
        _getCDRsByDateRangeAndCallerIdQuery = getCDRsByDateRangeAndCallerIdQuery;
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

    public async Task<Data.Entities.CDR?> GetCdrByReference(string cdrReference)
    {
        return await _getCDRByReferenceQuery.Execute(cdrReference);
    }

    public async Task<CDRReportResponse> GetCdrReport(DateTime startDate, DateTime endDate)
    {
        ValidateStartAndEndDates(startDate, endDate);

        var matchingCdrs = await _getCDRsByDateRangeQuery.Execute(startDate, endDate);

        return _cdrReportResponseMapper.Map(matchingCdrs);
    }

    public async Task<List<Data.Entities.CDR>> GetCdrsByCallerIdAndDate(string callerId, DateTime startDate, DateTime endDate)
    {
        ValidateStartAndEndDates(startDate, endDate);

        var result = await _getCDRsByDateRangeAndCallerIdQuery.Execute(callerId, startDate, endDate);

        return result;
    }

    private void ValidateStartAndEndDates(DateTime startDate, DateTime endDate)
    {
        var validationResult = _cdrReportQueryValidator.ValidateQuery(startDate, endDate);

        if (!validationResult.IsValid)
        {
            throw new ArgumentException(validationResult.Message);
        }
    }
}
