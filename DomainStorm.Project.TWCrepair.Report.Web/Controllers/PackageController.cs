using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA006.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA007.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA008.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA009.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA010.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA011.V1;

using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;
using System.Net.Mime;
using DomainStorm.Framework;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Package.V1;
using System.Xml;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/package")]
    public class PackageController(
            IGetService<RA006, string> ra006Service, 
            IGetService<RA007, string> ra007Service,
            IGetService<RA008, string> ra008Service,
            IGetService<RA009, string> ra009Service,
            IGetService<RA010, string> ra010Service,
            IGetService<RA011, string> ra011Service,
            IGetService<Stream, ReportConvertRequest> reportService,
            IMerge merge)
        : ControllerBase
    {
        [HttpPost("budgetDoc")]
        public async Task<ActionResult> BudgetDoc([FromBody] QueryPackage request)
        {
            var toMergeXmlDocumentList = new List<XmlDocument>
            {
                await OutXmlDocument<IGetService<RA006, string>, RA006, QueryRA006>(
                    ra006Service, new QueryRA006
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA006.cshtml", IConvert.Extension.XML),
                await OutXmlDocument<IGetService<RA007, string>, RA007, QueryRA007>(
                    ra007Service, new QueryRA007
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA007.cshtml", IConvert.Extension.XML),
                await OutXmlDocument<IGetService<RA008, string>, RA008, QueryRA008>(
                    ra008Service, new QueryRA008
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA008.cshtml", IConvert.Extension.XML),
                await OutXmlDocument<IGetService<RA009, string>, RA009, QueryRA009>(
                    ra009Service, new QueryRA009
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA009.cshtml", IConvert.Extension.XML),
                await OutXmlDocument<IGetService<RA010, string>, RA010, QueryRA010>(
                    ra010Service, new QueryRA010
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA010.cshtml", IConvert.Extension.XML),
                await OutXmlDocument<IGetService<RA011, string>, RA011, QueryRA011>(
                    ra011Service, new QueryRA011
                    {
                        Id = request.Id,
                        Extension = IConvert.Extension.XML
                    }, reportService, "/Views/RA011.cshtml", IConvert.Extension.XML)
            };

            var stream = merge.Merge(toMergeXmlDocumentList, IMerge.Extension.ODS);
            var outFileName = $"{request.Id}.{request.Extension.ToString().ToLower()}";

            return File(stream, MediaTypeNames.Application.Octet, outFileName);

        }

        private static async Task<XmlDocument> OutXmlDocument<TService, TModel, TQuery>(
            TService service,
            TQuery request,
            IGetService<Stream, ReportConvertRequest> reportService,
            string viewName,
            IConvert.Extension extension)
            where TService : IGetService<TModel, string>
            where TQuery : IQuery
            where TModel : ReportDataModel
        {
            var convertRequest = await ReportConvertRequest<TService, TModel, TQuery>(
                service, request, viewName, extension);

            await using var outStream = await reportService.GetAsync(convertRequest);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(outStream);
            return xmlDoc;
        }

        private static async Task<ReportConvertRequest> ReportConvertRequest<TService, TModel, TQuery>(
            TService service,
            TQuery request,
            string viewName,
            IConvert.Extension extension)
            where TService : IGetService<TModel, string>
            where TQuery : IQuery
            where TModel : ReportDataModel
        {
            var model = await service.GetAsync<TQuery>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = viewName,
                Model = model,
                Extension = extension
            };
            return convertRequest;
        }
    }
}
