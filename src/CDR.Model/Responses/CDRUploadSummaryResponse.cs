namespace CDR.Model.Responses;
public class CDRUploadSummaryResponse
{
    public bool IsSuccessful { get; set; } 

    public string? ErrorMessage { get; set; }

    public int TotalRecordsUploaded { get; set; }
}
