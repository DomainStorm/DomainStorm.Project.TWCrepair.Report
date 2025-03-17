using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA017.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

// <summary>
/// 合約-單價分析表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra017")]
public class RA017Controller : ControllerBase
{
    private readonly IGetService<RA017, string> _ra017Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA017Controller(
        IGetService<RA017, string> ra017Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra017Service = ra017Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA017 request)
    {
        var ra017Model = await _ra017Service.GetAsync<QueryRA017>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA017.cshtml",
            Model = ra017Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
