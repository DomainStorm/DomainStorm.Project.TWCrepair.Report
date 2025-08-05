using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA027.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA041.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 流量分析-檢前總表/檢後總表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra041")]
public class RA041Controller : ControllerBase
{
    private readonly IGetService<RA041, string> _ra041Service;
    private readonly IGetService<RA041MeasureDate, Guid> _ra041DateService;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA041Controller(
        IGetService<RA041, string> ra041Service,
        IGetService<RA041MeasureDate, Guid> ra041DateService,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra041Service = ra041Service;
        _ra041DateService = ra041DateService;
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA041 request)
    {
        var ra041Model = await _ra041Service.GetAsync<QueryRA041>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA041.cshtml",
            Model = ra041Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }

    [HttpPost("queryDate")]
    public async Task<ActionResult<RA041MeasureDate[]>> QueryDate([FromBody] QueryRA041Date request)
    {
        return await _ra041DateService.GetListAsync<QueryRA041Date>(request);
    }
}
