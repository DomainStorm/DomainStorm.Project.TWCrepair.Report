using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA021.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 修漏紀錄簿
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra021")]
public class RA021Controller : ControllerBase
{
    private readonly IGetService<RA021, string> _RA021Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA021Controller(
        IGetService<RA021, string> RA021Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA021Service = RA021Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA021 request)
    {
        var RA021Model = await _RA021Service.GetAsync<QueryRA021>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA021.cshtml",
            Model = RA021Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }

    
}
