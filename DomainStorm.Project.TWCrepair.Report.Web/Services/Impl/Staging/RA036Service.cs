using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using System.IO.Pipelines;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA036.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表六、各系統管徑、管長暨附屬設備統計表
/// </summary>
public class RA036Service : IGetService<RA036, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.Word>> _getWordRepository;
    private readonly GetRepository<IRepository<Repository.Models.Import.ImportPipe>> _getImportPipeRepository;
    private readonly GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> _getZoneRepository;
    private IMapper _mapper;

    public RA036Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Repository.Models.Word>> getWordRepository,
        GetRepository<IRepository<Repository.Models.Import.ImportPipe>> getImportPipeRepository,
        GetRepository<IRepository<Repository.Models.YearPlan.YearPlanSetAllZone>> getZoneRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _getImportPipeRepository = getImportPipeRepository;
        _getZoneRepository = getZoneRepository;
        _mapper = mapper;
    }

    public Task<RA036> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA036 e => QueryRA036(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA036> QueryRA036(QueryRA036 condition)
    {
        var planReport = await _getRepository().GetAsync(condition.Id);

        var result = new RA036
        {
            DepartmentName = planReport.DepartmentName,
            Year = planReport.Year - 1911,
        };

       


        if (planReport.YearPlanBase != null)
        {
            foreach(var ws in planReport.YearPlanBase.YearPlanWorkSpaces)
            {
                result.WorkSapces.Add(new RA036WorkSapce
                {
                    DepartmentId = planReport.DepartmentId,
                    SiteId = ws.SiteId,
                    SiteName = ws.SiteName,
                    WaterSupplySystemId = ws.WaterSupplySystemId,
                });
            }
        }
        
        
        //至少需要有2個工作區, 否則報表格式 columnSpan 會 < 0
        for (var i =  result.WorkSapces.Count ; i <2; i++)
        {
            result.WorkSapces.Add(new RA036WorkSapce
            {
                SiteName = "",
                WaterSupplySystemName = "",
                SiteId = Guid.Empty,
                WaterSupplySystemId = Guid.Empty
            });
        }

        //取得該區處(先不分廠所)的所有匯入的管徑資料
        var startMonthYear = $"{planReport.Year}01";
        var endMonthYear = $"{planReport.Year}10";
        var importPipes = await _getImportPipeRepository().GetListAsync<SimpleImportPipe>(
            x => x.DepartmentId == planReport.DepartmentId && x.YearMonth.CompareTo(startMonthYear) >= 0 && x.YearMonth.CompareTo(endMonthYear) <= 0,
            x => new SimpleImportPipe
            {
                WaterSupplySystemId = x.SystemId,
                SiteId = x.SiteId,
                Width = x.Width,
                Length = x.Length
            }
            );
        
        

        //取得詞庫所有管徑
        var pipes = await _getWordRepository().GetListAsync(x => x.Parent.Code == "F00");

        foreach(var pipe in pipes)
        {
            var newData = new RA036PiepSiteData
            {
                Name = pipe.Name
            };

            foreach(var ws in result.WorkSapces)
            {
                //目前管徑的目前廠所的管長
                var length = importPipes.Where(x => x.WaterSupplySystemId == ws.WaterSupplySystemId && x.SiteId == ws.SiteId && x.Width == int.Parse(pipe.Name))
                    .Sum(x => x.Length);
                newData.Lengthes.Add(length);
            }

            
            
            result.PiepSiteDatas.Add(newData);
        }
        //新增一筆合計列
        var totalData = new RA036PiepSiteData
        {
            Name = "合計"
        };
        for (int i = 0; i < result.WorkSapces.Count; i++)
        {
            var length = result.PiepSiteDatas.Select(x => x.Lengthes[i]).Sum();
            totalData.Lengthes.Add(length);
        }
        result.PiepSiteDatas.Insert(0, totalData);

        //todo : 300 折合和附屬設備的管長還沒統計
        return result;
    }

    public class SimpleImportPipe
    {
       
        /// <summary>
        /// 廠所代碼
        /// </summary>
        public Guid? SiteId { get; set; }

        /// <summary>
        /// 供水系統代碼
        /// </summary>
        public Guid? WaterSupplySystemId { get; set; }

       

        public int Width { get; set;  }

        public double Length { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA036[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
