using DomainStorm.Framework;
using DomainStorm.Framework.RazorEngine;
using DomainStorm.Framework.Services;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
using System.Xml;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.Report.V1;
using static Google.Rpc.Context.AttributeContext.Types;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
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
            var xmlContent = await _razorRenderer.RenderToStringAsync(request.ViewName, request.Model);
            var tempFile = Path.Combine("/input", $"{Guid.NewGuid()}.xml");

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);
            }
            catch(Exception e)
            {
                _log.LogError(e, $"f{request.Extension.ToString().ToLower()} file format error");
                throw;
            }

            try
            {
                await File.WriteAllTextAsync(tempFile, xmlContent);
                Stream outStream;

                if (request.Extension == IConvert.Extension.PDF)
                {
                    outStream = await ConvertPDF(tempFile);
                }
                else
                {
                    await using FileStream fs = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
                    outStream = _convert.Convert(fs, request.Extension);
                }
                return outStream;
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
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

        private async Task<MemoryStream> ConvertPDF(string fileName)
        {
            using var formData = new MultipartFormDataContent();
            var fileBytes = await File.ReadAllBytesAsync(fileName);
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            formData.Add(fileContent, "files", fileName);
            var ms = await _invokeMethod.OpenStreamAsync("Gotenberg", "forms/libreoffice/convert", formData, _httpContextAccessor.HttpContext);
            return ms;
        }

        async Task<PlotlyJson> IGetService<PlotlyJson, ReportConvertRequest>.GetAsync(ReportConvertRequest request)
        {
            var str = await _razorRenderer.RenderToStringAsync(request.ViewName, request.Model);
            return JsonSerializer.Deserialize<PlotlyJson>(HttpUtility.HtmlDecode(str))!;
        }

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
    }
}
