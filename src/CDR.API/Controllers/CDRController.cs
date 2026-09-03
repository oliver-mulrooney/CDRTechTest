using CDR.Model.Requests;
using CDR.Model.Responses;
using CDR.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CDR.API.Controllers;

[ApiController]
[Route("CDR")]
public class CDRController : ControllerBase
{
    private readonly ILogger<CDRController> _logger;
    private readonly ICDRService _cdrService;

    public CDRController(ILogger<CDRController> logger,
        ICDRService cdrService)
    {
        _logger = logger;
        _cdrService = cdrService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateCdrsFromCsv(IFormFile cdrFile)
    {
        var result = await _cdrService.CreateCdrsFromCsv(cdrFile);

        return result.IsSuccessful ? Ok(result) : BadRequest(result);
    }

    [HttpGet("Reference/{cdrReference}")]
    public async Task<IActionResult> GetCdrByReference(string cdrReference)
    {
        var result = await _cdrService.GetCdrByReference(cdrReference);

        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("Report")]
    public async Task<IActionResult> CdrReport([FromBody] CDRDateFilterRequest dateFilterRequest)
    {
        try
        {
            var result = await _cdrService.GetCdrReport(dateFilterRequest.StartDate, dateFilterRequest.EndDate);

            return result.CallAmount > 0 ? Ok(result) : NotFound(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("CallerId/{callerId}")]
    public async Task<IActionResult> GetCdrByReference([FromBody] CDRDateFilterRequest dateFilterRequest, string callerId)
    {
        try
        {
            var result = await _cdrService.GetCdrsByCallerIdAndDate(callerId, dateFilterRequest.StartDate, dateFilterRequest.EndDate);

            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("ExpensiveCallsReport/{callerId}/{amountOfCalls}")]
    public async Task<IActionResult> GetExpensiveCallsReport([FromBody] CDRDateFilterRequest dateFilterRequest, string callerId, int amountOfCalls)
    {
        try
        {
            var result = await _cdrService.GetMostExpensiveCallsByDateRangeAndCallerId(callerId, dateFilterRequest.StartDate, dateFilterRequest.EndDate, amountOfCalls);

            return result != null ? Ok(result) : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
