using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
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
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA005.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA006.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA007.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA008.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA009.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA010.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA011.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;



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
                Extension = FileExtension.JSON
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
                Extension = FileExtension.JSON
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
                Extension = FileExtension.JSON
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
                Extension = FileExtension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

        /// <summary>
        /// 總水頭分布圖
        /// </summary>
        [HttpPost("da005")]
        public async Task<PlotlyJson> DA005([FromBody] QueryDA005 request, [FromServices] IGetService<Views.Dashboards.DA005, string> _da005Service)
        {
            var da005 = await _da005Service.GetAsync<QueryDA005>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA005.cshtml",
                Model = da005,
                Extension = FileExtension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }


        /// <summary>
        /// 當日水壓曲線圖
        /// </summary>
        [HttpPost("da006")]
        public async Task<PlotlyJson> DA006([FromBody] QueryDA006 request, [FromServices] IGetService<Views.Dashboards.DA006, string> _da006Service)
        {
            var da006 = await _da006Service.GetAsync<QueryDA006>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA006.cshtml",
                Model = da006,
                Extension = FileExtension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }


        /// <summary>
        /// 當日流量曲線圖
        /// </summary>
        [HttpPost("da007")]
        public async Task<PlotlyJson> DA007([FromBody] QueryDA007 request, [FromServices] IGetService<Views.Dashboards.DA007, string> _da007Service)
        {
            var da007 = await _da007Service.GetAsync<QueryDA007>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA007.cshtml",
                Model = da007,
                Extension = FileExtension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

        /// <summary>
        /// 流量分析-(檢前總表/檢後總表)的流量曲線圖(結合在 RA041裡的圖)
        /// </summary>
        [HttpPost("da008")]
        public async Task<PlotlyJson> DA008([FromBody] QueryDA008 request, [FromServices] IGetService<Views.Dashboards.DA008, string> _da008Service)
        {
            var da008 = await _da008Service.GetAsync<QueryDA008>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/Dashboards/DA008.cshtml",
                Model = da008,
                Extension = FileExtension.JSON
            };

            var plotlyJson = await _reportService.GetAsync(convertRequest);
            return plotlyJson;
        }

		/// <summary>
		/// 流量分析-(檢前總表/檢後總表)的流量曲線圖(結合在 RA041裡的圖)
		/// </summary>
		[HttpPost("da009")]
		public async Task<PlotlyJson> DA009([FromBody] QueryDA009 request, [FromServices] IGetService<Views.Dashboards.DA009, string> _da009Service)
		{
			var da009 = await _da009Service.GetAsync<QueryDA009>(request);
			var convertRequest = new ReportConvertRequest
			{
				ViewName = "/Views/Dashboards/DA009.cshtml",
				Model = da009,
				Extension = FileExtension.JSON
			};

			var plotlyJson = await _reportService.GetAsync(convertRequest);
			return plotlyJson;
		}

		/// <summary>
		/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表-水壓比較圖(結合在 RA053裡的圖)
		/// </summary>
		[HttpPost("da010")]
		public async Task<PlotlyJson> DA010([FromBody] QueryDA010 request, [FromServices] IGetService<Views.Dashboards.DA010, string> _da010Service)
		{
			var da010 = await _da010Service.GetAsync<QueryDA010>(request);
			var convertRequest = new ReportConvertRequest
			{
				ViewName = "/Views/Dashboards/DA010.cshtml",
				Model = da010,
				Extension = FileExtension.JSON
			};

			var plotlyJson = await _reportService.GetAsync(convertRequest);
			return plotlyJson;
		}

		/// <summary>
		/// 檢漏系統-年度計畫-系統成果報告書-五.作業前後水壓比較表-總水頭分布圖(結合在 RA053裡的圖)
		/// </summary>
		[HttpPost("da011")]
		public async Task<PlotlyJson> DA011([FromBody] QueryDA011 request, [FromServices] IGetService<Views.Dashboards.DA011, string> _da011Service)
		{
			var da011 = await _da011Service.GetAsync<QueryDA011>(request);
			var convertRequest = new ReportConvertRequest
			{
				ViewName = "/Views/Dashboards/DA011.cshtml",
				Model = da011,
				Extension = FileExtension.JSON
			};

			var plotlyJson = await _reportService.GetAsync(convertRequest);
			return plotlyJson;
		}

	}
}
