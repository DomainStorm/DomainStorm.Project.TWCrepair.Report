using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWC.Report.Web.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA999.V1;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;

namespace DomainStorm.Project.TWC.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/ra999")]
    public class RA999Controller : ControllerBase
    {
        private readonly IGetService<RA999, string> _ra999Service;
        private readonly IGetService<Stream, ReportConvertRequest> _reportService;


        public RA999Controller(
            IGetService<RA999, string> ra999Service,
            IGetService<Stream, ReportConvertRequest> reportService
            ) 
        {
            _ra999Service = ra999Service;
            _reportService = reportService;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]QueryRA999 request)
        {
            var ra999Model = await _ra999Service.GetAsync<QueryRA999>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = "/Views/RA999.cshtml",
                Model = ra999Model,
                Extension = request.Extension
            };
            
            var outStream = await _reportService.GetAsync(convertRequest);
            var outFileName = $"{System.IO.Path.GetFileNameWithoutExtension(convertRequest.ViewName) }.{ convertRequest.Extension.ToString().ToLower()}";
            return File(outStream, MediaTypeNames.Application.Octet, outFileName);
        }
    }
}
