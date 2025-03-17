using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA014.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

// <summary>
/// 發包-單價分析表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra014")]
public class RA014Controller : ControllerBase
{
    private readonly IGetService<RA014, string> _ra014Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA014Controller(
        IGetService<RA014, string> ra014Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra014Service = ra014Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA014 request)
    {
        var ra014Model = await _ra014Service.GetAsync<QueryRA014>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA014.cshtml",
            Model = ra014Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
