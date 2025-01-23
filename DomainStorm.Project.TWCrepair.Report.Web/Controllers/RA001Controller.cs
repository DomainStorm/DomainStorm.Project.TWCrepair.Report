using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA001.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra001")]
public class RA001Controller : ControllerBase
{
    private readonly IGetService<DateTime, Guid> _ra001DateService;
    private readonly IGetService<RA001, string> _ra001Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA001Controller(
        IGetService<DateTime, Guid> ra001DateService,
        IGetService<RA001, string> ra001Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra001DateService = ra001DateService;
        _ra001Service = ra001Service;
        _reportService = reportService;
    }

    [HttpPost("queryDate")]
    public async Task<ActionResult<DateTime[]>> QueryDate([FromBody] QueryRA001Date request)
    {
        return await _ra001DateService.GetListAsync<QueryRA001Date>(request);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA001 request)
    {
        var ra001Model = await _ra001Service.GetAsync<QueryRA001>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA001.cshtml",
            Model = ra001Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
