namespace CDR.Data.Queries.Interfaces;
public interface IGetCDRsByDateRangeQuery
{
    public Task<List<Entities.CDR>> Execute(DateTime startDate, DateTime endDate);
}
