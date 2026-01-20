using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA054.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六.最小流量比較表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra054")]
public class RA054Controller : ControllerBase
{
    private readonly IGetService<RA054, string> _RA054Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA054Controller(
        IGetService<RA054, string> RA054Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA054Service = RA054Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA054 request)
    {
        var RA054Model = await _RA054Service.GetAsync<QueryRA054>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA054.cshtml",
            Model = RA054Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
