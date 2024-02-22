using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.Views;
using DomainStorm.Project.TWC.Report.Web.Views.Dashboards;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.DA001.V1;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/dashboards")]
    public class DashboardsController : ControllerBase
    {
        private readonly IGetService<PlotlyJson, ReportConvertRequest> _reportService;
        public DashboardsController(IGetService<PlotlyJson, ReportConvertRequest> reportService) 
        {
            _reportService = reportService;
        }
        
        [HttpPost("da001")]
        public async Task<PlotlyJson> DA001([FromBody]QueryDA001 request, [FromServices] IGetService<DA001, string> _da001Service)
        {
            var da001 = await _da001Service.GetAsync<QueryDA001>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA001.cshtml",
                Model = da001,
                Extension = request.Extension
            };
            
            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;


        }
    }
}
