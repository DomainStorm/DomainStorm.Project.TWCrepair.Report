using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA050.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 檢漏系統-年度計畫-系統成果報告書-二.檢修漏成果計算統計表
/// </summary>
public class RA050Service : IGetService<RA050, string>
{
    private readonly GetRepository<IRepository<Repository.Models.CheckSysAchievement>> _getCheckSysAchievementRepository;
    private readonly IMapper _mapper;
    

    public RA050Service(
       GetRepository<IRepository<Repository.Models.CheckSysAchievement>> getCheckSysAchievementRepository,
       IMapper mapper

       )
    {
        _getCheckSysAchievementRepository = getCheckSysAchievementRepository;
        _mapper = mapper;   
    }

    public Task<RA050> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA050> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA050 e => QueryRA050(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA050> QueryRA050(QueryRA050 condition)
    {
        RA050 result;
        var checkAchivement = (await _getCheckSysAchievementRepository().GetListAsync(x => x.WorkSpaceId == condition.WorkSpaceId)).FirstOrDefault();
        if(checkAchivement != null) 
        {
            result = _mapper.Map<RA050>(checkAchivement);
            result.WorkSpaceName = checkAchivement.WorkSpace?.WorkSpaceName;
        }
        else
        {
            result = new RA050();
        }
        return result;
    }

    

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA050[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA050[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA050[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
