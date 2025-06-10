using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA024.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 管線漏水密度及修理費用
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra024")]
public class RA024Controller : ControllerBase
{
    private readonly IGetService<RA024, string> _RA024Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA024Controller(
        IGetService<RA024, string> RA024Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA024Service = RA024Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA024 request)
    {
        var RA024Model = await _RA024Service.GetAsync<QueryRA024>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA024.cshtml",
            Model = RA024Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }

    
}
