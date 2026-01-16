using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA052.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra052")]
public class RA052Controller : ControllerBase
{
    private readonly IGetService<RA052, string> _RA052Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA052Controller(
        IGetService<RA052, string> RA052Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA052Service = RA052Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA052 request)
    {
        var RA052Model = await _RA052Service.GetAsync<QueryRA052>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA052.cshtml",
            Model = RA052Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
