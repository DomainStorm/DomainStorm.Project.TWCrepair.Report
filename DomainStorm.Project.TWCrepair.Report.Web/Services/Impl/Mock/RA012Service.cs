using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA012.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 發包-進度表
/// </summary>
public class RA012Service : IGetService<RA012, string>
{

    public Task<RA012> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA012 e => QueryRA012(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA012> QueryRA012(QueryRA012 condition) 
    {
        var result = new RA012
        {
            PrintDate = DateTime.Now,
            DepartmentName = "台中給水廠",
            PlanStartDate = DateTime.Parse("2025/01/01"),
            PlanEndDate = DateTime.Parse("2025/12/31"),
            EngineeringName = "台中給水廠東工區管線設備修理工程",
            Schedule= "１、本工程係屬管線維修工程，施工時間依照管線修漏工程契約之案件處理時限辦理，由甲方依漏水案件之緊急性及發生時間通知乙方進場施工。\n２、本表僅供參考，實際開工及竣工日期，依管線修漏工程契約之履約期限辦理。"
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA012[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
