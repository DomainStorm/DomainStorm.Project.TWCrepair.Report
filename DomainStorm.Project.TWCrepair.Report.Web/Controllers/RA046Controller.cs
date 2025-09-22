using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA046.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 工作日報表-請假天數檢核
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra046")]
public class RA046Controller : ControllerBase
{
    private readonly IGetService<RA046, string> _RA046Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA046Controller(
        IGetService<RA046, string> RA046Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA046Service = RA046Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA046 request)
    {
        var RA046Model = await _RA046Service.GetAsync<QueryRA046>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA046.cshtml",
            Model = RA046Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
