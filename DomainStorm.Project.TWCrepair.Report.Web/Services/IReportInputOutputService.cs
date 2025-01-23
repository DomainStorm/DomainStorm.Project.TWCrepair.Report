using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services;

public interface IReportInputOutputService<TViewModel, in TInputModel, out TQueryModel> : IGetService<TViewModel, string>
{
    TQueryModel InputToQueryMap(TInputModel inputModel);

    Task<Stream> ConvertAsync(ReportConvertRequest reportConvertRequest);
}