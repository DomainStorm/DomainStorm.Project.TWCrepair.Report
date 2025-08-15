using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using LinqKit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Http.Headers;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA023.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 管線修理統計表
/// </summary>
public class RA023Service : IGetService<RA023, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly GetRepository<IRepository<YearPlanSetAllZoneItem>> _getYearPlanRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA023Service(
        GetRepository<IRepository<FixForm>> getRepository,
        GetRepository<IRepository<YearPlanSetAllZoneItem>> getYearPlanRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _getYearPlanRepository = getYearPlanRepository;
        _mapper = mapper;
        _departmentService = departmentService;
    }

    public Task<RA023> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA023 e => QueryRA023(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA023> QueryRA023(QueryRA023 condition) 
    {
        var result = new RA023();
        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        result.DepartmentName = department.Name;

        if (condition.SiteId.HasValue)
        {
            var site = await _departmentService.GetAsync(condition.SiteId.Value.ToString());
            result.SiteName = site.Name;
        }
       

        var pb = PredicateBuilder.New<FixForm>();
        var exp = pb.Start(x => !x.IsRetrieved && !x.Deleted && x.ResponsibleReginId == condition.DepartmentId && x.FixFormProperty != null);   //排除移辦取回
        
        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.ResponsibleDepartmentId == condition.SiteId);
        }

        condition.GetRange();
        result.DateRange = condition.DateRange;
        exp = pb.And(x => x.AcceptanceTime >= condition.FinalBeginDate && x.AcceptanceTime < condition.FinalEndDate);

        var fixForms = await _getRepository().GetListAsync<RA023FixForm>(exp, x => new RA023FixForm
        {
            FormId =  x.FormId,
            FixUnit = x.FixFormDispatch.FixUnit != null ? x.FixFormDispatch.FixUnit.Name : "",
            CaseAttribute = x.FixFormDispatch.CaseAttribute != null ? x.FixFormDispatch.CaseAttribute.Name : "",
            CaseAttributeNotLeackage = x.FixFormDispatch.CaseAttributeNotLeackage != null ? x.FixFormDispatch.CaseAttributeNotLeackage.Name : "",
            EquipmentAttribute = x.FixFormDispatch.EquipmentAttribute != null ? x.FixFormDispatch.EquipmentAttribute.Name : "",
            EquipmentAttributeOther = x.FixFormDispatch.EquipmentAttributeOther != null ? x.FixFormDispatch.EquipmentAttributeOther.Name : "",
            PipeKind = x.FixFormDispatch.PipeKind != null ? x.FixFormDispatch.PipeKind.Name : "",
            PipeDiameter = x.FixFormDispatch.PipeDiameter != null ? x.FixFormDispatch.PipeDiameter.Name : "",
            AccessoryEquipment = x.FixFormProperty.AccessoryEquipment != null ? x.FixFormProperty.AccessoryEquipment.Name : "",
            AccessoryEquipmentCover = x.FixFormProperty.AccessoryEquipmentCover != null ? x.FixFormProperty.AccessoryEquipmentCover.Name : "",
            BoxAnnex = x.FixFormProperty.BoxAnnex != null ? x.FixFormProperty.BoxAnnex.Name : "",
            FinalCost_Outsourcing = x.FinalCost.FinalCost_Outsourcing,
            FinalCost_Material = x.FinalCost.FinalCost_Material,
            FinalCost_RoadRightProxy = x.FinalCost.FinalCost_RoadRightProxy,
            FinalCost_EmployeeSalary = x.FinalCost.FinalCost_EmployeeSalary,
            FinalCost_Other = x.FinalCost.FinalCost_Other,
            FinalCost_Total = x.FinalCost.FinalCost_Total,
            DailyAmount = x.FixFormLeakage != null ? x.FixFormLeakage.DailyAmount : null,
            TotalAmount = x.FixFormLeakage != null ? x.FixFormLeakage.TotalAmount : null,
            StartTime = x.FixFormDispatch != null ? x.FixFormDispatch.StartTime : null,
            DispatchTime = x.FixFormDispatch != null ? x.FixFormDispatch.DispatchTime : null,
            FixTime = x.FixFormProperty.FixTime
        });

        result.GenerateData(fixForms);

        //取得用戶數資料


         
        var pbYearplan = PredicateBuilder.New<YearPlanSetAllZoneItem>();
        
        var expYearplan = pbYearplan.Start(x => x.YearPlanSetAllZone.Year == condition.FinalBeginDate.Year - 1  //要抓前一個年度的資料 
        && x.YearPlanSetAllZone.DepartmentId == condition.DepartmentId );   
        if (condition.SiteId.HasValue)
        {
            expYearplan = expYearplan.And(x => x.YearPlanSetAllZone.SiteId == condition.SiteId);
        }
        var yearplans = await _getYearPlanRepository().GetListAsync<YearPlanItemData>(expYearplan, x => new YearPlanItemData { 
            DataType = x.DataType,
            Amount = x.Amount
        });
        result.PipeLength = yearplans
            .Where(i => i.DataType == YearPlanSetAllZoneDataType.OutdoorPipe || i.DataType == YearPlanSetAllZoneDataType.DistributionPipe)
            .Sum(x => x.Amount) * 1000;   //管線長度財產系統原始資料為公尺, FpWorker 的整合有預先除以 1000 (轉換為公里 ) =>這裡要還原為公尺
        result.CustomerAmount = yearplans
            .Where(i => i.DataType == YearPlanSetAllZoneDataType.Customer)
            .Sum(x => (int)x.Amount);
        return result;
    }

    public class YearPlanItemData
    {
        public double Amount { get; set; }

        public YearPlanSetAllZoneDataType DataType { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA023[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
