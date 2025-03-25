using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA007.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static Google.Api.ResourceDescriptor.Types;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra007")]
public class RA007Controller : ControllerBase
{
    private readonly IGetService<RA007, string> _ra007Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA007Controller(
        IGetService<RA007, string> ra007Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra007Service = ra007Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA007 request)
    {
        var ra007Model = await _ra007Service.GetAsync<QueryRA007>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA007.cshtml",
            Model = ra007Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }

    [HttpPost("editor")]
    public async Task<ActionResult> PostForEditor([FromBody] QueryRA007 request)
    {
        var ra007Model = await _ra007Service.GetAsync<QueryRA007>(request);
        ra007Model.MaterialPrice = GetInputString("MaterialPrice", ra007Model.MaterialPrice, "text", "width: 120px");
        ra007Model.MaterialPriceMemo = $"${{<p>{ra007Model.MaterialPriceMemo}</p><button id=\"button_InAllMaterialPrice\">帶入全部材料費</button><p>【{ra007Model.DepartmentName}全部區域材料費<br>※材料費：$<span id=\"span_InAllMaterialPrice\"></span>】</p>}}";
        ra007Model.SubTotalPrice = GetSpanString("SubTotalPrice", ra007Model.SubTotalPrice);
        ra007Model.Tax = GetSpanString("Tax", ra007Model.Tax);
        ra007Model.TotalPrice = GetSpanString("TotalPrice", ra007Model.TotalPrice);


        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA007.cshtml",
            Model = ra007Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }
    private static string GetInputString(string name, object? value, string type = "text", string style = "")
    {
        return $"${{<input name=\"{name}\" type=\"{type}\" value=\"{value}\" style=\"{style}\"></input>}}";
    }

    private static string GetSpanString(string id, string? text)
    {
        return $"${{<span id=\"{id}\">{text}</span>}}";
    }
}
