using CDR.Model.Responses;
using CDR.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CDR.API.Controllers;

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
}
