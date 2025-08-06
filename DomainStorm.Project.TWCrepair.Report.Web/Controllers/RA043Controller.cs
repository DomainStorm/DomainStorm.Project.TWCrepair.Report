using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA043.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 毀損計算營業損失
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra043")]
public class RA043Controller : ControllerBase
{
    private readonly IGetService<RA043, string> _RA043Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA043Controller(
        IGetService<RA043, string> RA043Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA043Service = RA043Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA043 request)
    {
        var RA043Model = await _RA043Service.GetAsync<QueryRA043>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA043.cshtml",
            Model = RA043Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
