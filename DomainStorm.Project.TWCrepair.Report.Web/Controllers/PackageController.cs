using DomainStorm.Framework;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;
using System.Net.Mime;
using System.Xml;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA004.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Package.V1;
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
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA026.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA027.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA028.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA029.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA030.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA031.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA032.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA033.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA034.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA035.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA036.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA037.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA038.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA039.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA040.V1;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/package")]
    public class PackageController(
            IGetService<RA001, string> ra001Service,
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
            IGetService<RA026, string> ra026Service,
            IGetService<RA027, string> ra027Service,
            IGetService<RA028, string> ra028Service,
            IGetService<RA029, string> ra029Service,
            IGetService<RA030, string> ra030Service,
            IGetService<RA031, string> ra031Service,
            IGetService<RA032, string> ra032Service,
            IGetService<RA033, string> ra033Service,
            IGetService<RA034, string> ra034Service,
            IGetService<RA036, string> ra036Service,
            IGetService<RA037, string> ra037Service,
            IGetService<RA039, string> ra039Service,
            IGetService<RA040, string> ra040Service,
            IGetService<DA004, string> da004Service,
            IGetService<DA005, string> da005Service,
            IGetService<Stream, ReportConvertRequest> reportService,
            GetMerge getMerge,
            GetConvert getConvert,
            ICache cache)
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
                { "paperHeight", "11.7" },
                { "waitDelay", "2s"}
            };

            var toMergeStreamList = new List<Stream>();

            foreach (var id in request.IdList)
            {
                toMergeStreamList.Add(await OutStream<IGetService<RA002, string>, RA002, QueryRA002>(
                    ra002Service, new QueryRA002
                    {
                        Id = id,
                        Extension = extension
                    }, reportService, "/Views/RA002.cshtml", extension, options));

                toMergeStreamList.Add(await OutStream<IGetService<RA003, string>, RA003, QueryRA003>(
                    ra003Service, new QueryRA003
                    {
                        Id = id,
                        Extension = extension
                    }, reportService, "/Views/RA003.cshtml", extension, options));

                toMergeStreamList.Add(await OutStream<IGetService<RA004, string>, RA004, QueryRA004>(
                    ra004Service, new QueryRA004
                    {
                        Id = id,
                        Extension = extension
                    }, reportService, "/Views/RA004.cshtml", extension, options));

                toMergeStreamList.Add(await OutStream<IGetService<RA005, string>, RA005, QueryRA005>(
                    ra005Service, new QueryRA005
                    {
                        Id = id,
                        Extension = extension
                    }, reportService, "/Views/RA005.cshtml", extension, options));
            }

            var stream = getMerge(extension).Merge(toMergeStreamList, extension);
            var outFileName = $"{Guid.NewGuid()}.{request.Extension.ToString().ToLower()}";

            return File(stream, MediaTypeNames.Application.Pdf, outFileName);

        }

        [HttpPost("yearPlan")]
        public async Task<ActionResult> YearPlan([FromBody] QueryPackage request)
        {
            const FileExtension extension = FileExtension.FODS;
            var toMergeXmlDocumentList = new List<XmlDocument>
            {
                await OutXmlDocument<IGetService<RA026, string>, RA026, QueryRA026>(
                    ra026Service, new QueryRA026
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA026.cshtml", extension),
                await OutXmlDocument<IGetService<RA027, string>, RA027, QueryRA027>(
                    ra027Service, new QueryRA027
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA027.cshtml", extension),
                await OutXmlDocument<IGetService<RA028, string>, RA028, QueryRA028>(
                    ra028Service, new QueryRA028
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA028.cshtml", extension),
                await OutXmlDocument<IGetService<RA029, string>, RA029, QueryRA029>(
                    ra029Service, new QueryRA029
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA029.cshtml", extension),
                await OutXmlDocument<IGetService<RA030, string>, RA030, QueryRA030>(
                    ra030Service, new QueryRA030
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA030.cshtml", extension),
                await OutXmlDocument<IGetService<RA031, string>, RA031, QueryRA031>(
                    ra031Service, new QueryRA031
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA031.cshtml", extension),
                await OutXmlDocument<IGetService<RA032, string>, RA032, QueryRA032>(
                    ra032Service, new QueryRA032
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA032.cshtml", extension),
                await OutXmlDocument<IGetService<RA033, string>, RA033, QueryRA033>(
                    ra033Service, new QueryRA033
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA033.cshtml", extension),
                await OutXmlDocument<IGetService<RA034, string>, RA034, QueryRA034>(
                    ra034Service, new QueryRA034
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA034.cshtml", extension),
                await OutXmlDocument(
                    new RA035(), reportService,"/Views/RA035.cshtml", extension),
                await OutXmlDocument<IGetService<RA036, string>, RA036, QueryRA036>(
                    ra036Service, new QueryRA036
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA036.cshtml", extension),
                await OutXmlDocument<IGetService<RA037, string>, RA037, QueryRA037>(
                    ra037Service, new QueryRA037
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA037.cshtml", extension),
                await OutXmlDocument(
                    new RA038
                    {
                        SheetName = "附表八、20%小區(分區主辦提供)"
                    }, reportService,"/Views/RA038.cshtml", extension),
                await OutXmlDocument(
                    new RA038
                    {
                        SheetName = "附表九、20%小區明細(分區主辦提供)"
                    }, reportService,"/Views/RA038.cshtml", extension),
                await OutXmlDocument<IGetService<RA039, string>, RA039, QueryRA039>(
                    ra039Service, new QueryRA039
                    {
                        Id = request.Id,
                        Extension = extension
                    }, reportService, "/Views/RA039.cshtml", extension),
                await OutXmlDocument<IGetService<RA040, string>, RA040, QueryRA040>(
                    ra040Service, new QueryRA040
                    {
                        DepartmentId = request.DepartmentId!.Value,
                        Year = request.Year!.Value,
                        Extension = extension
                    }, reportService, "/Views/RA040.cshtml", extension)
            };

            await using var stream = getMerge(extension).Merge(toMergeXmlDocumentList, extension);
            var outFileName = $"{request.Id}.{request.Extension.ToString().ToLower()}";
            var odsStream = await getConvert(FileExtension.ODS).Convert(stream, extension, FileExtension.ODS);
            return File(odsStream, MediaTypeNames.Application.Octet, outFileName);

        }

        [HttpPost("waterPressure")]
        public async Task<ActionResult> WaterPressure([FromBody] ReportCommandModel.RA001.V1.QueryRA001 request, CancellationToken cancellationToken)
        {
            const FileExtension extension = FileExtension.FODS;

            var toMergeXmlDocumentList = new List<XmlDocument>
            {
                await OutXmlDocument<IGetService<RA001, string>, RA001, ReportCommandModel.RA001.V1.QueryRA001>(
                    ra001Service, request, reportService, "/Views/RA001.cshtml", extension),
            };
          
            var DA004_Img_base64 = await cache.GetAsync<string?>($"{request.CachePrefix}-DA004");
            if (DA004_Img_base64 != null)
            {
                var DA004_Fods = await OutXmlDocument(
                    new RA038
                    {
                        SheetName = "水壓比較圖"
                    }, reportService, "/Views/RA038.cshtml", extension);

                InsertBase64IntoOffice(DA004_Fods, DA004_Img_base64);
                toMergeXmlDocumentList.Add(DA004_Fods);
            }

            var DA005_Img_base64 = await cache.GetAsync<string?>($"{request.CachePrefix}-DA005");
            if (DA005_Img_base64 != null)
            {
                var DA005_Fods = await OutXmlDocument(
                    new RA038
                    {
                        SheetName = "總水頭分布圖"
                    }, reportService, "/Views/RA038.cshtml", extension);

                InsertBase64IntoOffice(DA005_Fods, DA005_Img_base64);
                toMergeXmlDocumentList.Add(DA005_Fods);
            }

            await using var stream = getMerge(extension).Merge(toMergeXmlDocumentList, extension);
            var outFileName = $"{Guid.NewGuid()}.{request.Extension.ToString().ToLower()}";
            var odsStream = await getConvert(FileExtension.ODS).Convert(stream, extension, FileExtension.ODS);
            return File(odsStream, MediaTypeNames.Application.Octet, outFileName);

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

        private static async Task<XmlDocument> OutXmlDocument<TModel>(
            TModel model,
            IGetService<Stream, ReportConvertRequest> reportService,
            string viewName,
            FileExtension extension)
            where TModel : ReportDataModel
        {

            var convertRequest = new ReportConvertRequest
            {
                ViewName = viewName,
                Model = model,
                Extension = extension
            };

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

        private void InsertBase64IntoOffice(XmlDocument doc, string base64)
        {
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0");
            nsmgr.AddNamespace("draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0");
            nsmgr.AddNamespace("text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0");
            nsmgr.AddNamespace("svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0");
            nsmgr.AddNamespace("table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0");

            const string frameName = "檢修漏作業水壓比較圖";

            // 1) 如果檔案已有 draw:image/office:binary-data，直接寫入
            var existingNodes = doc.SelectNodes("//draw:image/office:binary-data", nsmgr);
            if (existingNodes != null && existingNodes.Count > 0)
            {
                foreach (XmlNode node in existingNodes)
                {
                    node.InnerText = base64;
                }
                return;
            }

            // 建立 draw:frame 元素（共用）
            var drawNs = nsmgr.LookupNamespace("draw");
            var svgNs = nsmgr.LookupNamespace("svg");
            var textNs = nsmgr.LookupNamespace("text");
            var officeNs = nsmgr.LookupNamespace("office");

            XmlElement frame = doc.CreateElement("draw", "frame", drawNs);
            // attributes
            var a1 = doc.CreateAttribute("draw", "z-index", drawNs); a1.Value = "0"; frame.Attributes.Append(a1);
            var a2 = doc.CreateAttribute("draw", "name", drawNs); a2.Value = frameName; frame.Attributes.Append(a2);
            var a3 = doc.CreateAttribute("draw", "style-name", drawNs); a3.Value = "gr1"; frame.Attributes.Append(a3);
            var a4 = doc.CreateAttribute("draw", "text-style-name", drawNs); a4.Value = "P1"; frame.Attributes.Append(a4);
            var s1 = doc.CreateAttribute("svg", "width", svgNs); s1.Value = "14.444cm"; frame.Attributes.Append(s1);
            var s2 = doc.CreateAttribute("svg", "height", svgNs); s2.Value = "14.461cm"; frame.Attributes.Append(s2);
            var s3 = doc.CreateAttribute("svg", "x", svgNs); s3.Value = "0cm"; frame.Attributes.Append(s3);
            var s4 = doc.CreateAttribute("svg", "y", svgNs); s4.Value = "0cm"; frame.Attributes.Append(s4);

            // 建立 draw:image
            var image = doc.CreateElement("draw", "image", drawNs);
            var m1 = doc.CreateAttribute("draw", "mime-type", drawNs); m1.Value = "image/png"; image.Attributes.Append(m1);

            // office:binary-data
            var binary = doc.CreateElement("office", "binary-data", officeNs);
            binary.InnerText = base64;
            image.AppendChild(binary);

            // text:p
            var tp = doc.CreateElement("text", "p", textNs);
            image.AppendChild(tp);

            frame.AppendChild(image);

            // 2) 嘗試找尋特定的 table:table-cell（先找已有同名 draw:frame 的 cell）
            XmlNode? targetCell = null;
            try
            {
                targetCell = doc.SelectSingleNode($"//table:table-cell[.//draw:frame[@draw:name='{frameName}']]", nsmgr);
            }
            catch { targetCell = null; }

            // 3) 若找不到，以含有相同文字的 cell 為目標（例如 cell 裡有該標題文字）
            if (targetCell == null)
            {
                try
                {
                    targetCell = doc.SelectSingleNode($"//table:table-cell[.//text:p[contains(., '{frameName}')]]", nsmgr);
                }
                catch { targetCell = null; }
            }

            // 4) 若仍找不到，找第一個沒有 draw:image 的 table:table-cell
            if (targetCell == null)
            {
                var cells = doc.SelectNodes("//table:table-cell", nsmgr);
                if (cells != null)
                {
                    foreach (XmlNode c in cells)
                    {
                        var hasImage = c.SelectSingleNode(".//draw:image", nsmgr) != null;
                        if (!hasImage)
                        {
                            targetCell = c;
                            break;
                        }
                    }
                }
            }

            // 5) 若找到 cell，就把 frame 插入該 cell 底下；否則退回到在 office:drawing 下新增
            if (targetCell != null)
            {
                // 若 cell 裡已有 text:p，則把 frame 插入到該 cell（在 cell 裡直接 append 是合適的）
                targetCell.AppendChild(frame);
                return;
            }

            // fallback: 在 office:drawing 底下新增
            var drawingNode = doc.SelectSingleNode("//office:body//office:drawing", nsmgr) ?? doc.DocumentElement;
            if (drawingNode != null)
            {
                drawingNode.AppendChild(frame);
            }
        }
    }
}
