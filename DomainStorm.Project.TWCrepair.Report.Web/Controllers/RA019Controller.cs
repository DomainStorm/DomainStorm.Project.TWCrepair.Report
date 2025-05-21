using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA019.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 漏水情形管制月報表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra019")]
public class RA019Controller : ControllerBase
{
    private readonly IGetService<RA019, string> _ra019Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA019Controller(
        IGetService<RA019, string> ra019Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra019Service = ra019Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA019 request)
    {
        var ra019Model = await _ra019Service.GetAsync<QueryRA019>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA019.cshtml",
            Model = ra019Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
