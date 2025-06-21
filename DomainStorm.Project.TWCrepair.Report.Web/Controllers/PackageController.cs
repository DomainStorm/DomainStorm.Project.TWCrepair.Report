using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA002.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA003.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA004.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA005.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA006.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA007.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA008.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA009.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA010.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA011.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA012.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA013.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA014.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA015.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA016.V1;

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
            IGetService<RA002, string> ra002Service,
            IGetService<RA003, string> ra003Service,
            IGetService<RA004, string> ra004Service,
            IGetService<RA005, string> ra005Service,
            IGetService<RA006, string> ra006Service, 
            IGetService<RA007, string> ra007Service,
            IGetService<RA008, string> ra008Service,
            IGetService<RA009, string> ra009Service,
            IGetService<RA010, string> ra010Service,
            IGetService<RA011, string> ra011Service,
            IGetService<RA012, string> ra012Service,
            IGetService<RA013, string> ra013Service,
            IGetService<RA014, string> ra014Service,
            IGetService<RA015, string> ra015Service,
            IGetService<Stream, ReportConvertRequest> reportService,
            GetMerge getMerge,
            GetConvert getConvert)
        : ControllerBase
    {
        [HttpPost("budgetDoc")]
        public async Task<ActionResult> BudgetDoc([FromBody] QueryPackage request)
        {
            const FileExtension extension = FileExtension.FODS;
            var toMergeXmlDocumentList = new List<XmlDocument>
            {
                await OutXmlDocument<IGetService<RA006, string>, RA006, QueryRA006>(
                    ra006Service, new QueryRA006
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA006.cshtml", extension),
                await OutXmlDocument<IGetService<RA007, string>, RA007, QueryRA007>(
                    ra007Service, new QueryRA007
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA007.cshtml", extension),
                await OutXmlDocument<IGetService<RA008, string>, RA008, QueryRA008>(
                    ra008Service, new QueryRA008
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA008.cshtml", extension),
                await OutXmlDocument<IGetService<RA009, string>, RA009, QueryRA009>(
                    ra009Service, new QueryRA009
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA009.cshtml", extension),
                await OutXmlDocument<IGetService<RA010, string>, RA010, QueryRA010>(
                    ra010Service, new QueryRA010
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA010.cshtml", extension),
                await OutXmlDocument<IGetService<RA011, string>, RA011, QueryRA011>(
                    ra011Service, new QueryRA011
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA011.cshtml", extension)
            };

            await using var stream = getMerge(extension).Merge(toMergeXmlDocumentList, extension);
            var outFileName = $"{request.Id}.{request.Extension.ToString().ToLower()}";
            var odsStream = await getConvert(FileExtension.ODS).Convert(stream, extension, FileExtension.ODS);
            return File(odsStream, MediaTypeNames.Application.Octet, outFileName);

        }

        [HttpPost("budgetDocOut")]
        public async Task<ActionResult> BudgetDocOut([FromBody] QueryPackage request)
        {
            const FileExtension extension = FileExtension.FODS;
            var toMergeXmlDocumentList = new List<XmlDocument>
            {
                await OutXmlDocument<IGetService<RA012, string>, RA012, QueryRA012>(
                    ra012Service, new QueryRA012
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA012.cshtml", extension),
                await OutXmlDocument<IGetService<RA013, string>, RA013, QueryRA013>(
                    ra013Service, new QueryRA013
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA013.cshtml", extension),
                await OutXmlDocument<IGetService<RA014, string>, RA014, QueryRA014>(
                    ra014Service, new QueryRA014
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA014.cshtml", extension),
                await OutXmlDocument<IGetService<RA015, string>, RA015, QueryRA015>(
                    ra015Service, new QueryRA015
                    {   
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA015.cshtml", extension)
            };

            await using var stream = getMerge(extension).Merge(toMergeXmlDocumentList, extension);
            var outFileName = $"{request.Id}.{request.Extension.ToString().ToLower()}";
            var odsStream = await getConvert(FileExtension.ODS).Convert(stream, extension, FileExtension.ODS);

            return File(odsStream, MediaTypeNames.Application.Octet, outFileName);

        }


        [HttpPost("dispatch")]
        public async Task<ActionResult> Dispatch([FromBody] QueryPackage request)
        {
            const FileExtension extension = FileExtension.PDF;
            var options = new Dictionary<string, string>
            {
                { "paperWidth", "8.27" },
                { "paperHeight", "11.7" }
            };
            var toMergeStreamList = new List<Stream>
            {
                await OutStream<IGetService<RA002, string>, RA002, QueryRA002>(
                    ra002Service, new QueryRA002
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA002.cshtml", extension, options),
                await OutStream<IGetService<RA003, string>, RA003, QueryRA003>(
                    ra003Service, new QueryRA003
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA003.cshtml", extension, options),
                await OutStream<IGetService<RA004, string>, RA004, QueryRA004>(
                    ra004Service, new QueryRA004
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA004.cshtml", extension, options),
                await OutStream<IGetService<RA005, string>, RA005, QueryRA005>(
                    ra005Service, new QueryRA005
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA005.cshtml", extension, options)
            };

            var stream = getMerge(extension).Merge(toMergeStreamList, extension);
            var outFileName = $"{request.Id}.{request.Extension.ToString().ToLower()}";

            return File(stream, MediaTypeNames.Application.Pdf, outFileName);

        }

        private static async Task<XmlDocument> OutXmlDocument<TService, TModel, TQuery>(
            TService service,
            TQuery request,
            IGetService<Stream, ReportConvertRequest> reportService,
            string viewName,
            FileExtension extension,
            Dictionary<string, string>? options = null)
            where TService : IGetService<TModel, string>
            where TQuery : IQuery
            where TModel : ReportDataModel
        {
            var convertRequest = await ReportConvertRequest<TService, TModel, TQuery>(
                service, request, viewName, extension, options);

            await using var outStream = await reportService.GetAsync(convertRequest);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(outStream);
            return xmlDoc;
        }

        private static async Task<Stream> OutStream<TService, TModel, TQuery>(
            TService service,
            TQuery request,
            IGetService<Stream, ReportConvertRequest> reportService,
            string viewName,
            FileExtension extension,
            Dictionary<string, string>? options = null)
            where TService : IGetService<TModel, string>
            where TQuery : IQuery
            where TModel : ReportDataModel
        {
            var convertRequest = await ReportConvertRequest<TService, TModel, TQuery>(
                service, request, viewName, extension, options);

            return await reportService.GetAsync(convertRequest);
        }

        private static async Task<ReportConvertRequest> ReportConvertRequest<TService, TModel, TQuery>(
            TService service,
            TQuery request,
            string viewName,
            FileExtension extension,
            Dictionary<string, string>? options = null)
            where TService : IGetService<TModel, string>
            where TQuery : IQuery
            where TModel : ReportDataModel
        {
            var model = await service.GetAsync<TQuery>(request);
            var convertRequest = new ReportConvertRequest
            {
                ViewName = viewName,
                Model = model,
                Extension = extension,
                Options = options
            };
            return convertRequest;
        }
    }
}
