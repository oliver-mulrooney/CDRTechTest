using CDR.Model.Models.ValidationResults;

namespace CDR.Service.Validators.Interfaces;
public interface ICDRReportQueryValidator
{
    CDRReportValidationResult ValidateQuery(DateTime startDate, DateTime endDate);
}
