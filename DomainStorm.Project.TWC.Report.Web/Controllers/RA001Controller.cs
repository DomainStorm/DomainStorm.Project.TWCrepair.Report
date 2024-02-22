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
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA001.V1;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/ra001")]
    public class RA001Controller : ControllerBase
    {
        private readonly IGetService<RA001, string> _ra001Service;
        private readonly IGetService<Stream, ReportConvertRequest> _reportService;


        public RA001Controller(
            IGetService<RA001, string> ra001Service,
            IGetService<Stream, ReportConvertRequest> reportService
            ) 
        {
            _ra001Service = ra001Service;
            _reportService = reportService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]QueryRA001 request)
        {
            var ra001Model = await _ra001Service.GetAsync<QueryRA001>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/RA001.cshtml",
                Model = ra001Model,
                Extension = request.Extension
            };
            var outStream = await _reportService.GetAsync(convertRequest);
            var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName) }.{ convertRequest.Extension.ToString().ToLower()}";
            return File(outStream, MediaTypeNames.Application.Octet, outFileName);

        }
    }
}
