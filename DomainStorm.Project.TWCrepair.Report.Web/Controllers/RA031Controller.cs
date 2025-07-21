using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA031.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表一 抄見率暨戶日配水量明細表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra031")]
public class RA031Controller : ControllerBase
{
    private readonly IGetService<RA031, string> _RA031Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA031Controller(
        IGetService<RA031, string> RA031Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA031Service = RA031Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA031 request)
    {
        var RA031Model = await _RA031Service.GetAsync<QueryRA031>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA031.cshtml",
            Model = RA031Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
