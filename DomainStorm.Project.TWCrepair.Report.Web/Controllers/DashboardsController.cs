using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA001.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA002.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA003.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA004.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;



namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/dashboards")]
    public class DashboardsController : ControllerBase
    {
        private readonly IGetService<PlotlyJson, ReportConvertRequest> _reportService;

        public DashboardsController(
            IGetService<PlotlyJson, ReportConvertRequest> reportService
            )
        {
            _reportService = reportService;
        }

        /// <summary>
        /// 工作日報表未輸入一覽表
        /// </summary>
        [HttpPost("da001")]
        public async Task<PlotlyJson> DA001([FromBody] QueryDA001 request, [FromServices] IGetService<Views.Dashboards.DA001, string> _da001Service)
        {
            var da001 = await _da001Service.GetAsync<QueryDA001>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA001.cshtml",
                Model = da001,
                Extension = IConvert.Extension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

        /// <summary>
        /// 修漏資料變更一覽表
        /// </summary>
        [HttpPost("da002")]
        public async Task<PlotlyJson> DA002([FromBody] QueryDA002 request, [FromServices] IGetService<Views.Dashboards.DA002, string> _da002Service)
        {
            var da002 = await _da002Service.GetAsync<QueryDA002>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA002.cshtml",
                Model = da002,
                Extension = IConvert.Extension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

        /// <summary>
        /// 當日水壓曲線圖
        /// </summary>
        [HttpPost("da003")]
        public async Task<PlotlyJson> DA003([FromBody] QueryDA003 request, [FromServices] IGetService<Views.Dashboards.DA003, string> _da003Service)
        {
            var da003 = await _da003Service.GetAsync<QueryDA003>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA003.cshtml",
                Model = da003,
                Extension = IConvert.Extension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

        /// <summary>
        /// 檢修漏作業水壓比較圖
        /// </summary>
        [HttpPost("da004")]
        public async Task<PlotlyJson> DA004([FromBody] QueryDA004 request, [FromServices] IGetService<Views.Dashboards.DA004, string> _da004Service)
        {
            var da004 = await _da004Service.GetAsync<QueryDA004>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA004.cshtml",
                Model = da004,
                Extension = IConvert.Extension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }
    }
}
