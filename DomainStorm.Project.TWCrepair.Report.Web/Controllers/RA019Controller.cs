using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Shared.CommandModel.RA019.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

/// <summary>
/// 漏水情形管制月報表
/// </summary>
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra019")]
public class RA019Controller : ControllerBase
{
    private readonly IGetService<RA019, string> _ra019Service;
    private readonly IGetService<RA019FixForm, string> _ra019FixFormService;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA019Controller(
        IGetService<RA019, string> ra019Service,
        IGetService<Stream, ReportConvertRequest> reportService,
        IGetService<RA019FixForm, string> ra019FixFormService)
    {
        _ra019Service = ra019Service;
        _reportService = reportService;
        _ra019FixFormService = ra019FixFormService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA019 request)
    {
        var ra019Model = await _ra019Service.GetAsync<QueryRA019>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA019.cshtml",
            Model = ra019Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }

    /// <summary>
    /// 取得清單,不轉檔
    [HttpPost("list")]
    public async Task<ActionResult<RA019>> List([FromBody] QueryRA019 request)
    {
        var ra019Model = await _ra019Service.GetAsync<QueryRA019>(request);
        return ra019Model;
    }

    
    /// <summary>
    /// 取得案件詳細資料
    [HttpPost("detail")]
    public async Task<ActionResult<RA019FixForm[]>> Detail([FromBody] QueryRA019Detail request)
    {
        var result = await _ra019FixFormService.GetListAsync(request.CacheId.ToString());
        return result;
    }
}
