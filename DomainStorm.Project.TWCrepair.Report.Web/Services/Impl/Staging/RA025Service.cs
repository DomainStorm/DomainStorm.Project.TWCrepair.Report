using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA025.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 個案支援（31表）件數統計
/// </summary>
public class RA025Service : IGetService<RA025, string>
{
    private readonly GetRepository<IRepository<Check31Form>> _getRepository;
    private readonly IGetService<Department, string> _departmentService;
    
    public RA025Service(
        GetRepository<IRepository<Check31Form>> getRepository,
        IGetService<Department, string> departmentService
        )
    {
        _getRepository = getRepository;
        _departmentService = departmentService;
    }

    public Task<RA025> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA025> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA025 e => QueryRA025(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA025> QueryRA025(QueryRA025 condition) 
    {
        var result = new RA025();
        condition.GetRange();
        result.BeginDate = condition.FinalBeginDate;
        result.EndDate = condition.FinalEndDate.AddDays(-1);

        var allForms = await _getRepository().GetListAsync< FormData>(x => x.ApplyTime >= condition.FinalBeginDate && x.ApplyTime < condition.FinalEndDate, 
            x => new FormData
            {
                DepartmentId = x.DepartmentId,
                CaseEstablished = x.Check3XFormFootnote != null ? x.Check3XFormFootnote.OnSiteWork.CaseEstablished : null,
                CloseTime = x.CloseTime
            }
        );

        //取得所有區處
        var departments = await _departmentService.GetListAsync<SearchDepartmentCommand>(new SearchDepartmentCommand
        {
            Level = 1,
        });
        //比對名稱以取得單位代碼
        foreach(var item in result.Items)
        {
            var match = departments.FirstOrDefault(x => x.Name == item.DepartmentName);
            if(match != null)
            {
                item.DepartmentId = match.DepartmentId;
                var forms = allForms.Where(x => x.DepartmentId ==  item.DepartmentId);
                item.AcceptAmount = forms.Count();
                item.EstablishAmount = forms.Where(x => x.CaseEstablished.HasValue && x.CaseEstablished.Value).Count();
                item.NotEstablishAmount = forms.Where(x => x.CaseEstablished.HasValue && !x.CaseEstablished.Value).Count();
                item.CloseAmount = forms.Where(x => x.CloseTime.HasValue).Count();
            }
        }
        result.Sum();
        return result;
    }

    public class FormData
    {
        public Guid DepartmentId { get; set; }
        public bool? CaseEstablished { get; set; }
        public DateTime? CloseTime { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA025[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA025[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA025[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
