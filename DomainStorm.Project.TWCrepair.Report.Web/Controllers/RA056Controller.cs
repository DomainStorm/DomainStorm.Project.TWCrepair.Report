using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA056.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-七.系統暨成本費用工作報表
/// </summary>
[ApiController]
[Route("api/ra056")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
public class RA056Controller : ControllerBase
{
    private readonly IGetService<RA056, string> _RA056Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA056Controller(
        IGetService<RA056, string> RA056Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA056Service = RA056Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA056 request)
    {
        var RA056Model = await _RA056Service.GetAsync<QueryRA056>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA056.cshtml",
            Model = RA056Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
