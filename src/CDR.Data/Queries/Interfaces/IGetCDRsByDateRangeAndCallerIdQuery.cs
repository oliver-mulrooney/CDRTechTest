using CDR.Data.Enums;

namespace CDR.Data.Queries.Interfaces;
public interface IGetCDRsByDateRangeAndCallerIdQuery
{
    Task<List<Entities.CDR>> Execute(string callerId, DateTime? startDate, DateTime? endDate, CallTypeEnum? callType);
}
