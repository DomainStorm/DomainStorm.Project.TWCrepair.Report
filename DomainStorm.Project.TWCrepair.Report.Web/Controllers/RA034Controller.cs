using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA034.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表四、檢漏作業計劃差旅費分析表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra034")]
public class RA034Controller : ControllerBase
{
    private readonly IGetService<RA034, string> _RA034Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA034Controller(
        IGetService<RA034, string> RA034Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA034Service = RA034Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA034 request)
    {
        var RA034Model = await _RA034Service.GetAsync<QueryRA034>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA034.cshtml",
            Model = RA034Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
