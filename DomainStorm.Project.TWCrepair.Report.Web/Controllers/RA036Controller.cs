using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA036.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表六、各系統管徑、管長暨附屬設備統計表
/// </summary>
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra036")]
public class RA036Controller : ControllerBase
{
    private readonly IGetService<RA036, string> _RA036Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA036Controller(
        IGetService<RA036, string> RA036Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA036Service = RA036Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA036 request)
    {
        var RA036Model = await _RA036Service.GetAsync<QueryRA036>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA036.cshtml",
            Model = RA036Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
