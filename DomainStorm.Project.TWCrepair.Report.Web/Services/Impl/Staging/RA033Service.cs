using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA033.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-三、檢漏作業各系統費用分析表
/// </summary>
public class RA033Service : IGetService<RA033, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> _getZoneRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> _getPlanBaseRepository;
    private IMapper _mapper;

    public RA033Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> getPlanBaseRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getZoneRepository = getZoneRepository;
        _getPlanBaseRepository = getPlanBaseRepository;
        _mapper = mapper;
    }

    public Task<RA033> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA033 e => QueryRA033(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA033> QueryRA033(QueryRA033 condition)
    {
        var result = new RA033();
        var planReport = await condition.GetModel(_getRepository(), _getPlanBaseRepository());

        if (planReport != null)
        {
            result.DepartmentName = planReport.DepartmentName;
            result.Year = planReport.Year - 1911;

            if (planReport.YearPlanBase != null)
            {
                planReport.YearPlanBase.CalculateTravelExpense();

                //多加一筆合計列
                planReport.YearPlanBase.AppendSumItem();
                result.YearPlanExpenseAllocate = _mapper.Map<YearPlanExpenseAllocate>(planReport.YearPlanBase);
                result.YearPlanExpenseAllocate.AddPercentageRow();


                //原本 vm 的清單裡, 配合 UI 的結構, 最後一列是 "合計的百分比" , 倒數第二列是合計
                //抽出百分比那一列
                result.SumPercentageItem = result.YearPlanExpenseAllocate.YearPlanWorkSpaces.Last();
                result.YearPlanExpenseAllocate.YearPlanWorkSpaces.Remove(result.SumPercentageItem);

                //再抽出合計那一列
                result.SumItem = result.YearPlanExpenseAllocate.YearPlanWorkSpaces.Last();
                result.YearPlanExpenseAllocate.YearPlanWorkSpaces.Remove(result.SumItem);
            }
        }
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA033[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
