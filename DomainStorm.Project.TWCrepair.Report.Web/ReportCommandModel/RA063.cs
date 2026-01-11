using DomainStorm.Framework;
using DomainStorm.Framework.Services;

namespace DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel;

public static class RA063
{
    public static class V1
    {
        /// <summary>
        /// 預算書-XML
        /// </summary>
        public class QueryRA063 : IQuery
        {
            public Guid Id { get; set; }


            /// <summary>
            ///  xml 的 documentType ,  1: budget  2:request 3:submit
            /// </summary>
            public string XmlKind { get; set; } = "1";
            public FileExtension Extension { get; set; } = FileExtension.XML;
        }
    }
}
