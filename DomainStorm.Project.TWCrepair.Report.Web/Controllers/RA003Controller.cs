using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA003.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra003")]
public class RA003Controller : ControllerBase
{
    private readonly IGetService<RA003, string> _ra003Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA003Controller(
        IGetService<DateTime, Guid> ra003DateService,
        IGetService<RA003, string> ra003Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra003Service = ra003Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA003 request)
    {
        var ra003Model = await _ra003Service.GetAsync<QueryRA003>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA003.cshtml",
            Model = ra003Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
