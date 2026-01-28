using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA055.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-六A.各計量點水量比較表
/// </summary>
[ApiController]
[Route("api/ra055")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
public class RA055Controller : ControllerBase
{
    private readonly IGetService<RA055, string> _RA055Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA055Controller(
        IGetService<RA055, string> RA055Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA055Service = RA055Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA055 request)
    {
        var RA055Model = await _RA055Service.GetAsync<QueryRA055>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA055.cshtml",
            Model = RA055Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
