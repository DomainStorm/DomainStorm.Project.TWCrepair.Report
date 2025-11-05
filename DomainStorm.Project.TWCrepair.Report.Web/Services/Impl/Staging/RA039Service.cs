using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA039.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 年度計畫報告-附表十、儀器需求統計
/// </summary>
public class RA039Service : IGetService<RA039, string>
{
    private readonly GetRepository<IRepository<YearPlanReport>> _getRepository;
    private readonly GetRepository<IRepository<Word>> _getWordRepository;
    private readonly GetRepository<IRepository<Instrument>> _getInstrumentRepository;
    private IMapper _mapper;

    public RA039Service(
        GetRepository<IRepository<YearPlanReport>> getRepository,
        GetRepository<IRepository<Word>> getWordRepository,
        GetRepository<IRepository<Instrument>> getInstrumentRepository,
        IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _getInstrumentRepository = getInstrumentRepository;
        _mapper = mapper;
    }

    public Task<RA039> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA039 e => QueryRA039(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA039> QueryRA039(QueryRA039 condition)
    {
        var planReport = await condition.GetModel(_getRepository());
        var result = new RA039
        {
            DepartmentName = planReport.DepartmentName,
            OnSitePeople = planReport.OnSitePeople
        };

        var wordRepository = _getWordRepository();


        //載入詞庫裡的"財產設備",並加上前置字元
        string[] prefixLetters = { "A.", "B.", "C.", "D.", "E.", "F.", "G.", "H.", "I.", "J." };
        result.Catagories = (await wordRepository.GetListAsync(x => x.Parent.Name == "財產設備"))
            .OrderBy(x => x.Sort).Select((x, i) => new RA039_Catagory
            {
                Id = x.Id,
                Name = prefixLetters[i] + x.Name,
            }).ToList();
            
        

        if (!planReport.YearPlanReportInstruments.Any() && condition.Initialize)
        {
            short sort = 0;

            //無儀器資料, 載入初始資料(目前區處的儀器)
            var deptInstruments = await _getInstrumentRepository().GetListAsync
                (x => x.DepartmentId == planReport.DepartmentId);

            foreach(var catagory in result.Catagories)
            {
                var equipmentWordIds = deptInstruments.Where(x => x.CategoryWordId!.Value == catagory.Id)
                    .Select(x => x.EquipmentWordId)  //不是直接取得儀器, 只要取得儀器設備的代碼就好
                    .Distinct();
                    
                foreach(var equipmentWordId in equipmentWordIds)
                {
                    var equipment = await wordRepository.GetAsync(equipmentWordId);
                    var newItem = new RA039_Item
                    {
                        CategoryName = catagory.Name,
                        EquipmentWordId = equipmentWordId,
                        Name = equipment.Name,
                        Sort = sort++
                    };
                    result.Items.Add(newItem);
                }
            }
        }
        else
        {
            foreach (var catagory in result.Catagories)
            {
                foreach (var modelInst in planReport.YearPlanReportInstruments.Where(x => x.Equipment.ParentId == catagory.Id))
                {
                    var newItem = _mapper.Map<RA039_Item>(modelInst);
                    newItem.CategoryName = catagory.Name;
                }
            }
        }

            return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA039[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
