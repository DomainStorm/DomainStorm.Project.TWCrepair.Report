using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA020.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 漏水原因分析表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra020")]
public class RA020Controller : ControllerBase
{
    private readonly IGetService<RA020, string> _ra020Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA020Controller(
        IGetService<RA020, string> ra020Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _ra020Service = ra020Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA020 request)
    {
        var ra020Model = await _ra020Service.GetAsync<QueryRA020>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA020.cshtml",
            Model = ra020Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }

    
}
