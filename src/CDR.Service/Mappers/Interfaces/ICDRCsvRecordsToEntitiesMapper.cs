using CDR.Model.Models.CSV;

namespace CDR.Service.Mappers.Interfaces;
public interface ICDRCsvRecordsToEntitiesMapper
{
    public List<Data.Entities.CDR> Map(List<CDRCsvRecord> cdrCsvRecords);
}
