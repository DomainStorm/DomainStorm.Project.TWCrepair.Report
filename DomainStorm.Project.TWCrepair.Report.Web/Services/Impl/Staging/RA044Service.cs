using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA044.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 區處執行管控表
/// </summary>
public class RA044Service : IGetService<RA044, string>
{
    private readonly GetRepository<IRepository<Repository.Models.ExecuteControl>> _getRepository;
   
    private IMapper _mapper;

    public RA044Service(
       GetRepository<IRepository<Repository.Models.ExecuteControl>> getRepository,
       IMapper mapper
        )
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA044> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA044 e => QueryRA044(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA044> QueryRA044(QueryRA044 condition)
    {
        var result = new RA044();

        var pb = PredicateBuilder.New<Repository.Models.ExecuteControl>();
        var exp = pb.Start(x =>
            x.DepartmentId == condition.DepartmentId
            && x.SuggestionDate >= condition.SuggestionDateBegin
            && x.SuggestionDate <= condition.SuggestionDateEnd);

        if (condition.Delisting.HasValue)
        {
            exp = exp.And(x => x.Delisting == condition.Delisting.Value);
        }
        var models = await _getRepository().GetListAsync(exp);
        result.Items = _mapper.Map<List<RA044_Item>>(models);

        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA044[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
