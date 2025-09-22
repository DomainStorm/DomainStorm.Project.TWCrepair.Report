using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA045.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 工作日報表-天數檢核
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra045")]
public class RA045Controller : ControllerBase
{
    private readonly IGetService<RA045, string> _RA045Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA045Controller(
        IGetService<RA045, string> RA045Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA045Service = RA045Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA045 request)
    {
        var RA045Model = await _RA045Service.GetAsync<QueryRA045>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA045.cshtml",
            Model = RA045Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
