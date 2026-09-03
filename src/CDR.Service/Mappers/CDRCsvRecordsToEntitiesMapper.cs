using CDR.Model.Models.CSV;
using CDR.Service.Mappers.Interfaces;

namespace CDR.Service.Mappers;
public class CDRCsvRecordsToEntitiesMapper : ICDRCsvRecordsToEntitiesMapper
{
    public List<Data.Entities.CDR> Map(List<CDRCsvRecord> cdrCsvRecords)
    {
        return cdrCsvRecords.Select(x =>
            new Data.Entities.CDR
            {
                CallerId = x.CallerId,
                CallDate = DateTime.Parse(x.CallDate),
                Recipient = x.Recipient,
                Duration = x.Duration,
                Cost = x.Cost,
                Currency = x.Currency,
                EndTime = x.EndTime,
                Reference = x.Reference,
                CallType = Data.Enums.CallTypeEnum.Domestic
            }
        ).ToList();
    }
}
