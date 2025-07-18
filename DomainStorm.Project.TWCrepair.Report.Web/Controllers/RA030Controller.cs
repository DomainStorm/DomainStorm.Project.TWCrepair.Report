using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA030.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-計劃經費
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra030")]
public class RA030Controller : ControllerBase
{
    private readonly IGetService<RA030, string> _RA030Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA030Controller(
        IGetService<RA030, string> RA030Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA030Service = RA030Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA030 request)
    {
        var RA030Model = await _RA030Service.GetAsync<QueryRA030>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA030.cshtml",
            Model = RA030Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
