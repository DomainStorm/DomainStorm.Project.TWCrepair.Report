using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA018.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 合約-資源統計表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra018")]
public class RA018Controller : ControllerBase
{
    private readonly IGetService<RA018, string> _ra018Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA018Controller(
        IGetService<RA018, string> ra018Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra018Service = ra018Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA018 request)
    {
        var ra018Model = await _ra018Service.GetAsync<QueryRA018>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA018.cshtml",
            Model = ra018Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
