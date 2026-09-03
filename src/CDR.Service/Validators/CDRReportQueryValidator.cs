using CDR.Model.Models.ValidationResults;
using CDR.Service.Validators.Interfaces;

namespace CDR.Service.Validators;
internal class CDRReportQueryValidator : ICDRReportQueryValidator
{
    public CDRReportValidationResult ValidateQuery(DateTime startDate, DateTime endDate)
    {
        var validationResult = new CDRReportValidationResult()
        {
            IsValid = true
        };

        if (startDate > endDate)
        {
            validationResult.IsValid = false;
            validationResult.Message = "Start date must be less than or equal to the end date.";
            return validationResult;
        }

        if ((endDate - startDate).TotalDays > 30)
        {
            validationResult.IsValid = false;
            validationResult.Message = "Date Range must be no longer than 1 month (30 days).";
            return validationResult;
        }

        return validationResult;
    }
}
