using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA006.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra006")]
public class RA006Controller : ControllerBase
{
    private readonly IGetService<RA006, string> _ra006Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA006Controller(
        IGetService<RA006, string> ra006Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra006Service = ra006Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA006 request)
    {
        var ra006Model = await _ra006Service.GetAsync<QueryRA006>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA006.cshtml",
            Model = ra006Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
