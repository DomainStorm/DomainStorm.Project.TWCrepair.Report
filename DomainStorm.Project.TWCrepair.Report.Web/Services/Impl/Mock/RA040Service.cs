using DomainStorm.Framework.Services;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA040.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Mock;

/// <summary>
/// 年度計畫報告-附表十一、隊員目標
/// </summary>
public class RA040Service : IGetService<RA040, string>
{
  

    public Task<RA040> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA040> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA040 e => QueryRA040(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA040> QueryRA040(QueryRA040 condition)
    {
        var result = new RA040
        {
            DepartmentName = "四區處",
            Items = new List<RA040_Item>()
            {
                new RA040_Item
                {
                    Name = "張三",
                    TargetAmount = 100,
                    TargetPipeLength = 1000.2,
                },
                new RA040_Item
                {
                    Name = "李四",
                    TargetAmount = 200,
                    TargetPipeLength = 2000.2,
                }
            }
        };
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA040[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA040[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA040[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
