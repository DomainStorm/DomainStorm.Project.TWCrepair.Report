using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA025.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 個案支援（31表）件數統計
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra025")]
public class RA025Controller : ControllerBase
{
    private readonly IGetService<RA025, string> _RA025Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA025Controller(
        IGetService<RA025, string> RA025Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA025Service = RA025Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA025 request)
    {
        var RA025Model = await _RA025Service.GetAsync<QueryRA025>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA025.cshtml",
            Model = RA025Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
