using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA063.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 預算書-XML
/// </summary>
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra063")]
public class RA063Controller : ControllerBase
{
    private readonly IGetService<RA063, string> _RA063Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA063Controller(
        IGetService<RA063, string> RA063Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA063Service = RA063Service;
        _reportService = reportService;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA063 request)
    {
        var RA063Model = await _RA063Service.GetAsync<QueryRA063>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA063.cshtml",
            Model = RA063Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
