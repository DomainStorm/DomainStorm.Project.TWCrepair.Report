using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA002.V1;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/ra002")]
    public class RA002Controller : ControllerBase
    {
        private readonly IGetService<RA002, string> _ra002Service;
        private readonly IGetService<Stream, ReportConvertRequest> _reportService;


        public RA002Controller(
            IGetService<RA002, string> ra002Service,
            IGetService<Stream, ReportConvertRequest> reportService
            ) 
        {
            _ra002Service = ra002Service;
            _reportService = reportService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]QueryRA002 request)
        {
            var ra002Model = await _ra002Service.GetAsync<QueryRA002>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/RA002.cshtml",
                Model = ra002Model,
                Extension = request.Extension
            };
            
            var outStream = await _reportService.GetAsync(convertRequest);
            var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName) }.{ convertRequest.Extension.ToString().ToLower()}";
            return File(outStream, MediaTypeNames.Application.Octet, outFileName);
        }
    }
}
