using CDR.Data.Enums;
using CsvHelper.Configuration.Attributes;

namespace CDR.Model.Models.CSV;
public class CDRCsvRecord
{
    [Name("caller_id")]
    public required string CallerId { get; set; }

    [Name("recipient")]
    public required string Recipient { get; set; }

    [Name("call_date")]
    public required string CallDate { get; set; }

    [Name("end_time")]
    public TimeSpan EndTime { get; set; }

    [Name("duration")]
    public int Duration { get; set; }

    [Name("cost")]
    public decimal Cost { get; set; }

    [Name("reference")]
    public required string Reference { get; set; }

    [Name("currency")]
    public CurrencyEnum Currency { get; set; }
}
