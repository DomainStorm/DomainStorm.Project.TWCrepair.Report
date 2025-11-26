using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA049.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-一.作業成果分析表
/// </summary>
public class RA049Service : IGetService<RA049, string>
{
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> _getPlanRepository;
    private readonly GetRepository<IRepository<Repository.Models.CheckAchievement>> _getCheckAchievementRepository;
    private readonly IMapper _mapper;
    

    public RA049Service(
       GetRepository<IRepository<Repository.Models.YearPlan.YearPlanBase>> getPlanRepository,
       GetRepository<IRepository<Repository.Models.CheckAchievement>> getCheckAchievementRepository,
       IMapper mapper

       )
    {
        _getPlanRepository = getPlanRepository;
        _getCheckAchievementRepository = getCheckAchievementRepository;
        _mapper = mapper;   
    }

    public Task<RA049> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA049> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA049 e => QueryRA049(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA049> QueryRA049(QueryRA049 condition)
    {
        var checkAchivement = (await _getCheckAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
        var result = _mapper.Map<RA049>(checkAchivement);
        if(checkAchivement != null) 
        {
            result.WorkSpaceName = checkAchivement.WorkSpace?.WorkSpaceName;

            #region 績效分析  要計算差異及達成率,轉型成新類別報表比較好處理
            result.Performance_UnderGroundLeakageAmount.RealAmount = checkAchivement.UnderGroundLeakageAmount;
            result.Performance_PipeLength.PlanAmount = checkAchivement.PlanPipeLength;
            result.Performance_PipeLength.RealAmount = checkAchivement.RealPipeLength;
            result.Performance_CustomerAmount.PlanAmount = checkAchivement.PlanCustomerAmount;
            result.Performance_CustomerAmount.RealAmount = checkAchivement.RealCustomerAmount;
            var temp = checkAchivement.CheckAchievementAmountVolumes.FirstOrDefault(x => x.Name == "合計");
            if(temp != null)
            {
                result.Performance_Volumn.PlanAmount =  temp.PlanVolumn; ;
                result.Performance_Volumn.RealAmount = temp.RealVolumn;

            }
            result.Performance_BenefitAmount.RealAmount = checkAchivement.BenefitAmount;
            result.Performance_CostPerCmd.RealAmount = checkAchivement.ProductionCostPerCmd;
            result.Performance_CostPerKm.RealAmount = checkAchivement.ProductionCostPerKm;

            // "基本資料" 沒有儲存的相關欄位, 要在報表載入
            var plan = (await _getPlanRepository().GetListAsync(x =>
                x.DepartmentId == checkAchivement.DepartmentId  &&
                x.Year == checkAchivement.Year)).FirstOrDefault();
            var yearPlanWorkSpace = plan?.YearPlanWorkSpaces?.FirstOrDefault(x => x.WorkSpaceId == checkAchivement.WorkSpaceId);

            
            if(yearPlanWorkSpace != null)
            {
                //地下漏水件數=年度規劃分析-五.工作計劃-地下漏水檢漏件數 管線+用戶外線
                result.Performance_UnderGroundLeakageAmount.PlanAmount = (yearPlanWorkSpace.CheckOutAmountDistributionPipe ?? 0)
                                                 + (yearPlanWorkSpace.CheckOutAmountOutdoorPipe ?? 0);
                //檢漏效益(元) = 年度規劃分析 - 五.工作計劃 - 效益額(元)
                result.Performance_BenefitAmount.PlanAmount = yearPlanWorkSpace.BenefitAmount;
                //檢漏成本(元/CMD)=年度規劃分析-五.工作計劃-檢漏成本-元/CMD
                result.Performance_CostPerCmd.PlanAmount = yearPlanWorkSpace.CheckOutCostPerCMD;
                //檢漏成本(元/KM)=年度規劃分析-五.工作計劃-檢漏成本-元/KM
                result.Performance_CostPerKm.PlanAmount = yearPlanWorkSpace.CheckOutCostPerKm;
            }
            #endregion

            #region 費用 要計算差異及達成率,轉型成新類別報表比較好處理
            result.Expense_Personnel = new RA049_DiffAndRate(checkAchivement.PlanPersonnelExpense, checkAchivement.RealPersonnelExpense);
            result.Expense_Travel = new RA049_DiffAndRate(checkAchivement.PlanTravelExpense, checkAchivement.RealTravelExpense);
            result.Expense_Other = new RA049_DiffAndRate(checkAchivement.PlanOtherExpense, checkAchivement.RealOtherExpense);
            result.Expense_CarMaintain = new RA049_DiffAndRate(checkAchivement.PlanCarMaintainExpense, checkAchivement.RealCarMaintainExpense);
            result.Expense_ApplianceMaintain = new RA049_DiffAndRate(checkAchivement.PlanApplianceMaintainExpense, checkAchivement.RealApplianceMaintainExpense);
            result.Expense_Depreciation = new RA049_DiffAndRate(checkAchivement.PlanDepreciationExpense, checkAchivement.RealDepreciationExpense);
            result.Expense_Overtime = new RA049_DiffAndRate(checkAchivement.PlanOvertimeExpense, checkAchivement.RealOvertimeExpense);
            result.Expense_CheckOutTool = new RA049_DiffAndRate(checkAchivement.PlanCheckOutToolExpense, checkAchivement.RealCheckOutToolExpense);
            result.Expense_Fuel = new RA049_DiffAndRate(checkAchivement.PlanFuleExpense, checkAchivement.RealFuleExpense);
            result.Expense_Total = new RA049_DiffAndRate(checkAchivement.PlanTotalExpense, checkAchivement.RealTotalExpense);
            #endregion

            result.MinFlowCompare = new RA049_DiffAndRate(checkAchivement.MinFlowBefore, checkAchivement.MinFlowAfter);
        }
        return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA049[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA049[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA049[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
