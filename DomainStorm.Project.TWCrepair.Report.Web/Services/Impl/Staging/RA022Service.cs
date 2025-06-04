using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA022.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 修漏紀錄簿二
/// </summary>
public class RA022Service : IGetService<RA022, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA022Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
        _departmentService = departmentService;
    }

    public Task<RA022> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA022 e => QueryRA022(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA022> QueryRA022(QueryRA022 condition) 
    {
        
        var result = new RA022();


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
        result.BeginDate = condition.FinalBeginDate;
        result.EndDate = condition.FinalEndDate.AddDays(-1);   //輸出要顯示最後一天的 23:59

        exp = pb.And(x => x.AcceptanceTime >= condition.FinalBeginDate && x.AcceptanceTime < condition.FinalEndDate);

        result.Items = await _getRepository().GetListAsync<RA022Item>(exp, x => new RA022Item
        {
            FixCaseNo = x.FixCaseNo,
            CaseAttribute = x.FixFormProperty.CaseAttribute != null ? x.FixFormProperty.CaseAttribute.Name : "",
            CaseAttributeNotLeackageOther = x.FixFormProperty.CaseAttributeNotLeackageOther != null ? x.FixFormProperty.CaseAttributeNotLeackageOther.Name : "",
            EquipmentAttribute = x.FixFormProperty.EquipmentAttribute != null ? x.FixFormProperty.EquipmentAttribute.Name : "",
            EquipmentAttributeOther = x.FixFormProperty.EquipmentAttributeOther != null ? x.FixFormProperty.EquipmentAttributeOther.Name : "",
            PipeKind = x.FixFormProperty.PipeKind != null ? x.FixFormProperty.PipeKind.Name : "",
            PipeDiameter = x.FixFormProperty.PipeDiameter != null ? x.FixFormProperty.PipeDiameter.Name : "",
            AccessoryEquipment = x.FixFormProperty.AccessoryEquipment != null ? x.FixFormProperty.AccessoryEquipment.Name : "",
            BoxAnnex = x.FixFormProperty.BoxAnnex != null ? x.FixFormProperty.BoxAnnex.Name : "",
            FinalCost_Outsourcing = x.FinalCost.FinalCost_Outsourcing,
            FinalCost_Material = x.FinalCost.FinalCost_Material,
            FinalCost_RoadRightProxy = x.FinalCost.FinalCost_RoadRightProxy,
            FinalCost_EmployeeSalary = x.FinalCost.FinalCost_EmployeeSalary,
            FinalCost_Other = x.FinalCost.FinalCost_Other,
            FinalCost_Total = x.FinalCost.FinalCost_Total,
            Reason = x.FixFormLeakage != null && x.FixFormLeakage.Reason != null ? x.FixFormLeakage.Reason.Name : "",
            FixSituation = x.FixFormLeakage != null && x.FixFormLeakage.FixSituation != null ? x.FixFormLeakage.FixSituation.Name : "",
            Situation = x.FixFormLeakage != null && x.FixFormLeakage.Situation != null ? x.FixFormLeakage.Situation.Name : "",
            DailyAmount = x.FixFormLeakage != null ? x.FixFormLeakage.DailyAmount : null,
            TotalAmount = x.FixFormLeakage != null ? x.FixFormLeakage.TotalAmount : null,
            LeakageEquipmentAttribute = x.FixFormLeakage != null && x.FixFormLeakage.EquipmentAttribute != null ? x.FixFormLeakage.EquipmentAttribute.Name : "",
            Photos = x.FixFormAudit != null ? x.FixFormAudit.FixFormAuditAttachments.Count : 0,
            SuperVisorHour = x.FixFormAudit != null ? x.FixFormAudit.FixFormAuditSupervisors.Select(v => $"{v.SupervisorUserName} {v.Hour}時").ToList() : null,
        });
        return result;
    }

    
   
    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA022[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
