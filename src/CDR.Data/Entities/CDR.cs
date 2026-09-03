using CDR.Data.Enums;

namespace CDR.Data.Entities;
public class CDR
{
    public int Id { get; set; }

    public required string CallerId { get; set; }

    public required string Recipient { get; set; }

    public DateTime CallDate { get; set; }

    public TimeSpan EndTime { get; set; }

    public int Duration { get; set; }

    public double Cost { get; set; }

    public required string Reference { get; set; }

    public CurrencyEnum Currency { get; set; }

    public CallTypeEnum CallType { get; set; }
}
