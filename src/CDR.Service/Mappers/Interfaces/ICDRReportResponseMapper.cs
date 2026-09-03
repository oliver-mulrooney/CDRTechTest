using CDR.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDR.Service.Mappers.Interfaces;
public interface ICDRReportResponseMapper
{
    CDRReportResponse Map(List<Data.Entities.CDR> cdrs);
}
