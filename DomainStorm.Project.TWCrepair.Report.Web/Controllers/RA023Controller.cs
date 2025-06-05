using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA023.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 管線修理統計表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra023")]
public class RA023Controller : ControllerBase
{
    private readonly IGetService<RA023, string> _RA023Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA023Controller(
        IGetService<RA023, string> RA023Service,
        IGetService<Stream, ReportConvertRequest> reportService
        )
    {
        _RA023Service = RA023Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA023 request)
    {
        var RA023Model = await _RA023Service.GetAsync<QueryRA023>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA023.cshtml",
            Model = RA023Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }

    
}
