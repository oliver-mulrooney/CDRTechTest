using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR.Data.Queries.Interfaces;
public interface IGetMostExpensiveCDRsByDateRangeAndCallerIdQuery
{
    Task<List<Data.Entities.CDR>> Execute(DateTime startDate, DateTime endDate, string callerId, int amount);
}
