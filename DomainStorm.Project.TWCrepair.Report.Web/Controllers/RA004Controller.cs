using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA004.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra004")]
public class RA004Controller : ControllerBase
{
    private readonly IGetService<DateTime, Guid> _ra004DateService;
    private readonly IGetService<RA004, string> _ra004Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA004Controller(
        IGetService<DateTime, Guid> ra004DateService,
        IGetService<RA004, string> ra004Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra004DateService = ra004DateService;
        _ra004Service = ra004Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA004 request)
    {
        var ra004Model = await _ra004Service.GetAsync<QueryRA004>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA004.cshtml",
            Model = ra004Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
