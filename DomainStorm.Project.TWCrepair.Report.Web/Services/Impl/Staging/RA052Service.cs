using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA052.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-四.最小流量率(修正值)比較表
/// </summary>
public class RA052Service : IGetService<RA052, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
    private readonly IMapper _mapper;
    

    public RA052Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
       IMapper mapper

       )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
        _mapper = mapper;   
    }

    public Task<RA052> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA052> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA052 e => QueryRA052(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA052> QueryRA052(QueryRA052 condition)
    {
        RA052 result;
        var checkAchivement = (await _getCheckSysAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
        if(checkAchivement != null) 
        {
            result = _mapper.Map<RA052>(checkAchivement);
            result.WorkSpaceName = checkAchivement.WorkSpace?.WorkSpaceName;
        }
        else
        {
            result = new RA052();
        }
        return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA052[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA052[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA052[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
