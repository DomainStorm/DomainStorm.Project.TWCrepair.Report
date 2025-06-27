using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA025.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 個案支援（31表）件數統計
/// </summary>
public class RA025Service : IGetService<RA025, string>
{
   

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

        result.Items[2].AcceptAmount = 3;
        result.Items[2].EstablishAmount = 1;
        result.Items[2].NotEstablishAmount = 2;
        result.Items[2].CloseAmount = 3;

        result.Items[3].AcceptAmount = 5;
        result.Items[3].EstablishAmount = 0;
        result.Items[3].NotEstablishAmount = 4;
        result.Items[3].CloseAmount = 5;


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
