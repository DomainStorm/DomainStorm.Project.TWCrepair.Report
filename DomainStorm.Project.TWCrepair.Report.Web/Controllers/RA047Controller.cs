using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA047.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-封面
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra047")]
public class RA047Controller : ControllerBase
{
    private readonly IGetService<RA047, string> _RA047Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA047Controller(
        IGetService<RA047, string> RA047Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA047Service = RA047Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA047 request)
    {
        var RA047Model = await _RA047Service.GetAsync<QueryRA047>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA047.cshtml",
            Model = RA047Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
