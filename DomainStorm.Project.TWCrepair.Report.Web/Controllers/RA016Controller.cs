using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA016.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 合約-詳細表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra016")]
public class RA016Controller : ControllerBase
{
    private readonly IGetService<RA016, string> _ra016Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA016Controller(
        IGetService<RA016, string> ra016Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra016Service = ra016Service;
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA016 request)
    {
        var ra016Model = await _ra016Service.GetAsync<QueryRA016>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA016.cshtml",
            Model = ra016Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
