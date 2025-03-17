using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA013.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 發包-詳細表(估價單)
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra013")]
public class RA013Controller : ControllerBase
{
    private readonly IGetService<RA013, string> _ra013Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA013Controller(
        IGetService<RA013, string> ra013Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra013Service = ra013Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA013 request)
    {
        var ra013Model = await _ra013Service.GetAsync<QueryRA013>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA013.cshtml",
            Model = ra013Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
