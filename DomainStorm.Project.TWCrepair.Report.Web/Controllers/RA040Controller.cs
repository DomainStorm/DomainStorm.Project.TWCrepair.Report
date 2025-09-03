using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA040.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表十一、隊員目標
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra040")]
public class Ra040Controller : ControllerBase
{
    private readonly IGetService<RA040, string> _ra040Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public Ra040Controller(
        IGetService<RA040, string> Ra040Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _ra040Service = Ra040Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA040 request)
    {
        var Ra040Model = await _ra040Service.GetAsync<QueryRA040>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/Ra040.cshtml",
            Model = Ra040Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
