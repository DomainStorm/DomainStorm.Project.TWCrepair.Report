using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA006.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Project.TWCrepair.Report.Web.Views;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
[Route("api/ra006")]
public class RA006Controller : ControllerBase
{
    private readonly IGetService<RA006, string> _ra006Service;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;

    public RA006Controller(
        IGetService<RA006, string> ra006Service,
        IGetService<Stream, ReportConvertRequest> reportService)
    {
        _ra006Service = ra006Service;
        _reportService = reportService;
    }

   

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] QueryRA006 request)
    {
        var ra006Model = await _ra006Service.GetAsync<QueryRA006>(request);
        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA006.cshtml",
            Model = ra006Model,
            Extension = request.Extension
        };
        var outStream = await _reportService.GetAsync(convertRequest);
        var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName)}.{convertRequest.Extension.ToString().ToLower()}";
        return File(outStream, MediaTypeNames.Application.Octet, outFileName);

    }

    [HttpPost("editor")]
    public async Task<ActionResult> PostForEditor([FromBody] QueryRA006 request)
    {
        var ra006Model = await _ra006Service.GetAsync<QueryRA006>(request);
        ra006Model.EngineeringName = GetInputString("EngineeringName", ra006Model.EngineeringName, "text", "width: 350px");
        ra006Model.EngineeringNumber = GetInputString("EngineeringNumber", ra006Model.EngineeringNumber, "text", "width: 350px");
        ra006Model.AccountingAccount = GetInputString("AccountingAccount", ra006Model.AccountingAccount, "text", "width: 350px");
        ra006Model.EngineeringLocation = GetInputString("EngineeringLocation", ra006Model.EngineeringLocation, "text", "width: 350px");
        ra006Model.EngineeringMethod = GetInputString("EngineeringMethod", ra006Model.EngineeringMethod, "text", "width: 350px");
        ra006Model.EngineeringSummary = $"${{<textarea name=\"EngineeringSummary\" style=\"width: 337px; height: 267px;\">{ra006Model.EngineeringSummary}</textarea>}}";
        ra006Model.PlanStartDate = GetInputString("PlanStartDate", ra006Model.PlanStartDate, "date");
        ra006Model.PlanEndDate = GetInputString("PlanEndDate", ra006Model.PlanEndDate, "date");
        ra006Model.DesignDrawingAmount = GetInputString("DesignDrawingAmount", ra006Model.DesignDrawingAmount, "text", "width: 60px");
        ra006Model.ManualAmount = GetInputString("ManualAmount", ra006Model.ManualAmount, "text", "width: 60px");
        ra006Model.CalculationBookAmount = GetInputString("CalculationBookAmount", ra006Model.CalculationBookAmount, "text", "width: 60px");
        ra006Model.DetailTableAmount = GetInputString("DetailTableAmount", ra006Model.DetailTableAmount, "text", "width: 60px");
        ra006Model.UnitPriceAmount = GetInputString("UnitPriceAmount", ra006Model.UnitPriceAmount, "text", "width: 60px");
        ra006Model.Notes = GetInputString("Notes", ra006Model.Notes, "text", "width: 700px");

        ra006Model.Notes += GetInputString("_IsTemplate", ra006Model.IsTemplate, "hidden");

        var convertRequest = new ReportConvertRequest
        {
            ViewName = "/Views/RA006.cshtml",
            Model = ra006Model,
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
}
