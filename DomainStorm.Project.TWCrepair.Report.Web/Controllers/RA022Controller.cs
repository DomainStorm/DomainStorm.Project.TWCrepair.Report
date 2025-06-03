using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA022.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 修漏紀錄簿二
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra022")]
public class RA022Controller : ControllerBase
{
    private readonly IGetService<RA022, string> _RA022Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA022Controller(
        IGetService<RA022, string> RA022Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA022Service = RA022Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA022 request)
    {
        var RA022Model = await _RA022Service.GetAsync<QueryRA022>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA022.cshtml",
            Model = RA022Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }

    
}
