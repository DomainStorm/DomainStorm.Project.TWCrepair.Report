using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using System.Web;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.Report.V1;
using System.Text.Json;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock
{
    public class ReportService : IGetService<Stream, ReportConvertRequest>, IGetService<PlotlyJson, ReportConvertRequest>
    {
        private readonly IConvert _convert;
        private readonly IRazorViewToStringRenderer _razorRenderer;
        private readonly ILogger _log;
        private readonly IInvokeMethod _invokeMethod;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportService(
            IRazorViewToStringRenderer razorRenderer,
            IConvert convert,
            IInvokeMethod invokeMethod,
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory
            )
        {
            _razorRenderer = razorRenderer;
            _convert = convert;
            _invokeMethod = invokeMethod;
            _httpContextAccessor = httpContextAccessor;
            _log = loggerFactory.CreateLogger(GetType());
        }

        public async Task<Stream> GetAsync(ReportConvertRequest request)
        {
            try
            {
                Stream outStream = null!;

                if (request.Extension == IConvert.Extension.PDF)
                {
                    string pdfFile = "wwwroot/assets/fake.pdf";
                    outStream = File.OpenRead(pdfFile);
                }
                else if (request.Extension == IConvert.Extension.ODS)
                {
                    string odsFile = "wwwroot/assets/fake.ods";
                    outStream = File.OpenRead(odsFile);
                }
                else if (request.Extension == IConvert.Extension.XLSX)
                {
                    string xlsxFile = "wwwroot/assets/fake.xlsx";
                    outStream = File.OpenRead(xlsxFile);
                }
                else
                {
                    throw new Exception("不支援該格式");
                }

                return outStream;
            }
            catch
            {
                throw;
            }
        }


        //private async Task<MemoryStream> ConvertPDF(string fileName)
        //{
        //    using var formData = new MultipartFormDataContent();
        //    var fileBytes = System.IO.File.ReadAllBytes(fileName);
        //    var fileContent = new ByteArrayContent(fileBytes);
        //    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        //    formData.Add(fileContent, "files", fileName);
        //    var ms = await _invokeMethod.OpenStreamAsync("Gotenberg", "forms/libreoffice/convert", formData, _httpContextAccessor.HttpContext);
        //    return ms;
        //}

        public Task<Stream> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }

        public Task<Stream[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Stream[]> GetListAsync(ReportConvertRequest id)
        {
            throw new NotImplementedException();
        }

        public Task<Stream[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }

        
        async Task<PlotlyJson> IGetService<PlotlyJson, ReportConvertRequest>.GetAsync(ReportConvertRequest request)
        {
            var str = await _razorRenderer.RenderToStringAsync(request.ViewName, request.Model);
            return JsonSerializer.Deserialize<PlotlyJson>(HttpUtility.HtmlDecode(str))!;
        }

        Task<PlotlyJson> IGetService<PlotlyJson, ReportConvertRequest>.GetAsync<TQuery>(IQuery condition)
        {
            throw new NotImplementedException();
        }

        Task<PlotlyJson[]> IGetService<PlotlyJson, ReportConvertRequest>.GetListAsync()
        {
            throw new NotImplementedException();
        }

        Task<PlotlyJson[]> IGetService<PlotlyJson, ReportConvertRequest>.GetListAsync(ReportConvertRequest id)
        {
            throw new NotImplementedException();
        }

        Task<PlotlyJson[]> IGetService<PlotlyJson, ReportConvertRequest>.GetListAsync<TQuery>(IQuery condition)
        {
            throw new NotImplementedException();
        }
    }
}
