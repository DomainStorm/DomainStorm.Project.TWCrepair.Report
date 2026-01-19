using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA053.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra053")]
public class RA053Controller : ControllerBase
{
    private readonly IGetService<RA053, string> _RA053Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA053Controller(
        IGetService<RA053, string> RA053Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA053Service = RA053Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA053 request)
    {
        var RA053Model = await _RA053Service.GetAsync<QueryRA053>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA053.cshtml",
            Model = RA053Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
