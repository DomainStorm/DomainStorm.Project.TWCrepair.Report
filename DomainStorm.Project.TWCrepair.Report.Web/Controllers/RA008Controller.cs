using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA008.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra008")]
public class RA008Controller : ControllerBase
{
    private readonly IGetService<RA008, string> _ra008Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA008Controller(
        IGetService<RA008, string> ra008Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra008Service = ra008Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA008 request)
    {
        var ra008Model = await _ra008Service.GetAsync<QueryRA008>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA008.cshtml",
            Model = ra008Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }

    [HttpPost("editor")]
    public async Task<ActionResult> PostForEditor([FromBody] QueryRA008 request)
    {
        var ra008Model = await _ra008Service.GetAsync<QueryRA008>(request);
        ra008Model.Schedule = $"${{<textarea name=\"Schedule\" style=\"width: 560px; height: 720px;\">{ra008Model.Schedule}</textarea>}}";
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA008.cshtml",
            Model = ra008Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
}
