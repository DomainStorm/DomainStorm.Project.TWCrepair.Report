using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA021.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 修漏紀錄簿
/// </summary>
public class RA021Service : IGetService<RA021, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA021Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
        _departmentService = departmentService;
    }

    public Task<RA021> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA021 e => QueryRA021(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA021> QueryRA021(QueryRA021 condition) 
    {
        
        var result = new RA021();

       
        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        result.DepartmentName = department.Name;

        if (condition.SiteId.HasValue)
        {
            var site = await _departmentService.GetAsync(condition.SiteId.Value.ToString());
            result.SiteName = site.Name;
        }
       

        var pb = PredicateBuilder.New<FixForm>();
        var exp = pb.Start(x =>  !x.Deleted && x.ResponsibleReginId == condition.DepartmentId);  
        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.ResponsibleDepartmentId == condition.SiteId);
        }

        condition.GetRange();
        result.DateRange = condition.DateRange;
        result.BeginDate = condition.FinalBeginDate;
        result.EndDate = condition.FinalEndDate.AddDays(-1);   //輸出要顯示最後一天的 23:59

        exp = pb.And(x => x.AcceptanceTime >= condition.FinalBeginDate && x.AcceptanceTime < condition.FinalEndDate);

        result.Items = await _getRepository().GetListAsync<RA021Item>(exp, x => new RA021Item
        {
            FixCaseNo = x.FixCaseNo,
            AcceptanceTime = x.AcceptanceTime,
            Location = x.Location,
            Source = x.Source != null ? x.Source.Name : "",
            ReporterMobile = x.ReporterMobile,
            FixDescription = x.FixDescription,
            PipeDiameter = x.FixFormDispatch != null && x.FixFormDispatch.PipeDiameter != null ? x.FixFormDispatch.PipeDiameter.Name : "",
            FixTime = x.FixFormProperty != null ? x.FixFormProperty.FixTime : null,
            FixDeadline = x.FixFormDispatch != null ? x.FixFormDispatch.FixDeadline : null,
            FixUnit = x.FixFormDispatch != null && x.FixFormDispatch.FixUnit != null ? x.FixFormDispatch.FixUnit.Name : "",
            CaseEmergency = x.FixFormDispatch != null && x.FixFormDispatch.CaseEmergency != null ? x.FixFormDispatch.CaseEmergency.Name : "",
            WorkTime = x.FixFormDispatch != null && x.FixFormDispatch.WorkTime != null ? x.FixFormDispatch.WorkTime.Name : "",
            ChargeAmount= x.ChargeAmount,
            DispatchNotes = x.FixFormDispatch != null ? x.FixFormDispatch.Notes : null,
            TransferNotes = x.TransferTargetFixForm != null ?  $"移轉廠所: {x.TransferTargetFixForm.ResponsibleDepartmentName}" : null,
            IsRetrieved = x.IsRetrieved
        });
        return result;
    }

    
   
    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA021[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
