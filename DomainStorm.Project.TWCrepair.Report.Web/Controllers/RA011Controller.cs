using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA011.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra011")]
public class RA011Controller : ControllerBase
{
    private readonly IGetService<RA011, string> _ra011Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA011Controller(
        IGetService<RA011, string> ra011Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra011Service = ra011Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA011 request)
    {
        var ra011Model = await _ra011Service.GetAsync<QueryRA011>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA011.cshtml",
            Model = ra011Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
