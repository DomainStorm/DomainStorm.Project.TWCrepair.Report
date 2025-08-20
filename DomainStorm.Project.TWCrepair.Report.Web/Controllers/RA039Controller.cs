using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA039.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;


namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 年度計畫報告-附表十、儀器需求統計
/// </summary>
[ApiController]
[Route("api/ra039")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
public class RA039Controller : ControllerBase
{
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;
    private readonly IGetService<RA039, string> _ra039Service;

    public RA039Controller(
        IGetService<Stream, ReportConvertRequest> reportService,
        IGetService<RA039, string> ra039Service
        )
    {
        _reportService = reportService;
        _ra039Service = ra039Service;
    }



    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA039 request)
    {
        var RA039Model = await _ra039Service.GetAsync<QueryRA039>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA039.cshtml",
            Model = RA039Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);
    }
}
