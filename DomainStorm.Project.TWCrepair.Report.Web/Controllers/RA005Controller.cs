using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA005.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Route("api/ra005")]
public class RA005Controller : ControllerBase
{
    private readonly IGetService<RA005, string> _ra005Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA005Controller(
        IGetService<RA005, string> ra005Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra005Service = ra005Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA005 request)
    {
        var ra005Model = await _ra005Service.GetAsync<QueryRA005>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA005.cshtml",
            Model = ra005Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
