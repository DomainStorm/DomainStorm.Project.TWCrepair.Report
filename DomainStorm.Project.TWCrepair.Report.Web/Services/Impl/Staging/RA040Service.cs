using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using DomainStorm.Project.TWCrepair.Repository.Models;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA040.V1;
using DomainStorm.Framework.BlazorComponent.ViewModel;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表十一、隊員目標
/// </summary>
public class RA040Service : IGetService<RA040, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<CheckerTarget>> _getTargetRepository;
    private readonly IGetService<Department, string> _departmentService;
    private IMapper _mapper;

    public RA040Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<CheckerTarget>> getTargetRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getTargetRepository = getTargetRepository;
        _departmentService = departmentService;
        _mapper = mapper;
    }

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
        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        var result = new RA040
        {
            DepartmentName = department.Name

        };

         var targets = await _getTargetRepository().GetListAsync<RA040_Item>(x => x.Year == condition.Year && x.DepartmentId == condition.DepartmentId,
            x => new RA040_Item
            {
                Name = x.Name,
                TargetAmount = x.TargetAmount,
                TargetPipeLength = x.TargetPipeLength
            });

        result.Items = targets.ToList();
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
