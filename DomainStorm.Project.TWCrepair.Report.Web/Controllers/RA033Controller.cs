using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA033.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表三、檢漏作業各系統費用分析表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra033")]
public class RA033Controller : ControllerBase
{
    private readonly IGetService<RA033, string> _RA033Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA033Controller(
        IGetService<RA033, string> RA033Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA033Service = RA033Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA033 request)
    {
        var RA033Model = await _RA033Service.GetAsync<QueryRA033>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA033.cshtml",
            Model = RA033Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
