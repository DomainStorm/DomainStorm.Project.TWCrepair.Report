using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA020.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 漏水原因分析表
/// </summary>
public class RA020Service : IGetService<RA020, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA020Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
        _departmentService = departmentService;
    }

    public Task<RA020> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA020 e => QueryRA020(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA020> QueryRA020(QueryRA020 condition) 
    {
        
        var result = new RA020();


        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        result.DepartmentName = department.Name;

        if (condition.SiteId.HasValue)
        {
            var site = await _departmentService.GetAsync(condition.SiteId.Value.ToString());
            result.SiteName = site.Name;
        }
       

        var pb = PredicateBuilder.New<FixForm>();
        var exp = pb.Start(x => !x.IsRetrieved && !x.Deleted && x.ResponsibleReginId == condition.DepartmentId);   //排除移辦取回
        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.ResponsibleDepartmentId == condition.SiteId);
        }

        condition.GetRange();
        result.DateRange = condition.DateRange;

        exp = pb.And(x => x.AcceptanceTime >= condition.FinalBeginDate && x.AcceptanceTime < condition.FinalEndDate);

        var fixForms = await _getRepository().GetListAsync<FixFormSummary>(exp, x => new FixFormSummary
        {
            FormId = x.FormId,
            EquipmentAttribute = x.FixFormProperty != null && x.FixFormProperty.EquipmentAttribute != null ? x.FixFormProperty.EquipmentAttribute.Name : "",
            PipeKind = x.FixFormProperty != null && x.FixFormProperty.PipeKind != null ? x.FixFormProperty.PipeKind.Name : "",
            AccessoryEquipment = x.FixFormProperty != null && x.FixFormProperty.AccessoryEquipment != null ? x.FixFormProperty.AccessoryEquipment.Name : "",
            BoxAnnex = x.FixFormProperty != null && x.FixFormProperty.BoxAnnex != null ? x.FixFormProperty.BoxAnnex.Name : "",
            Reason = x.FixFormLeakage != null && x.FixFormLeakage.Reason != null ? x.FixFormLeakage.Reason.Name : "",
            FixSituation = x.FixFormLeakage != null && x.FixFormLeakage.FixSituation != null ? x.FixFormLeakage.FixSituation.Name : "",
            DailyAmount = x.FixFormLeakage != null ? x.FixFormLeakage.DailyAmount : 0,
            TotalAmount = x.FixFormLeakage != null ? x.FixFormLeakage.TotalAmount : 0,
            DistributeEquipmentAttribute = x.FixFormLeakage != null && x.FixFormLeakage.EquipmentAttribute != null ? x.FixFormLeakage.EquipmentAttribute.Name : "",
            Situation = x.FixFormLeakage != null && x.FixFormLeakage.Situation != null ? x.FixFormLeakage.Situation.Name : "",
            Source = x.Source != null ? x.Source.Name : ""
        });

        #region 管線設備
        //管線
        foreach (var kind in RA020.pipeKinds)
        {
            var kindFixForms = fixForms.Where(x => x.EquipmentAttribute == RA020.equipmentAttributes[0] && x.PipeKind == kind).ToArray();
            var equipmentData = GetEquipmentData(kindFixForms);
            result.AddEquipmentData(RA020.equipmentAttributes[0], kind, equipmentData);
        }

        //附屬設備
        foreach (var accessor in RA020.accessoryEquipments)
        {
            var accessorFixForms = fixForms.Where(x => x.EquipmentAttribute == RA020.equipmentAttributes[1] && x.AccessoryEquipment == accessor).ToArray();
            var equipmentData = GetEquipmentData(accessorFixForms);
            result.AddEquipmentData(RA020.equipmentAttributes[1], accessor, equipmentData);
        }

        //表箱另件
        foreach (var box in RA020.boxAnnexs)
        {
            var boxFixForms = fixForms.Where(x => x.EquipmentAttribute == RA020.equipmentAttributes[2] && x.BoxAnnex == box).ToArray();
            var equipmentData = GetEquipmentData(boxFixForms);
            result.AddEquipmentData(RA020.equipmentAttributes[2], box, equipmentData);
        }

        //其他
        var otherFixForms = fixForms.Where(x => x.EquipmentAttribute == RA020.equipmentAttributes[3]).ToArray();
        var otherEquipmentData = GetEquipmentData(otherFixForms);
        result.AddEquipmentData(RA020.equipmentAttributes[3], "", otherEquipmentData);

        //合計
        var totalEquipmentData = new EquipmentData(result.EquipmentDataDic.Values.ToArray());
        result.AddEquipmentData("合計", "", totalEquipmentData);
        #endregion


        //送配水及用水設備 (前兩個有兩層式的,用迴圈)
        for(var i = 0;i <2; i++)
        {
            for(var j= 0; j< 2; j++)
            {
                var distributeFixForms = fixForms.Where(x => x.DistributeEquipmentAttribute == RA020.distributeEquipments[i] && x.EquipmentAttribute == RA020.equipmentAttributes[j]).ToArray();
                var distributEquipmentData = GetDistributeEquipmentData(distributeFixForms);
                result.AddDistributeEquipmentData(RA020.distributeEquipments[i], RA020.equipmentAttributes[j], distributEquipmentData);
            }
        }
        //送配水及用水設備-其他
        var otherDistributeFixForms = fixForms.Where(x => x.DistributeEquipmentAttribute == RA020.distributeEquipments[2]).ToArray();
        var otherDistributEquipmentData = GetDistributeEquipmentData(otherDistributeFixForms);
        result.AddDistributeEquipmentData(RA020.distributeEquipments[2],"", otherDistributEquipmentData);
        //送配水及用水設備-合計
        var totalDistributeEquipmentData = new DistributeEquipmentData(result.DistributeEquipmentDataDic.Values.ToArray());
        result.AddDistributeEquipmentData("合計", "", totalDistributeEquipmentData);



        //漏水情形
        foreach (var situation in RA020.situations)
        {
            var situationFixForms = fixForms.Where(x => x.Situation == situation ).ToArray();
            var situationData = GetSituationData(situationFixForms);
            result.AddSituationData(situation, "", situationData);
        }
        var totalSituationData = new SituationData(result.SituationDataDic.Values.ToArray());
        result.AddSituationData("合計", "", totalSituationData);



        //案件來源
        foreach (var source in RA020.sources)
        {
            var sourceFixForms = fixForms.Where(x => x.Source== source).ToArray();
            var sourceData = GetSourceData(sourceFixForms);
            result.SourceDataDic.Add(source, sourceData);
        }
        var totalSourceData = new SourceData(result.SourceDataDic.Values.ToArray());
        result.SourceDataDic.Add("合計", totalSourceData);


        

        return result;
    }

    private EquipmentData GetEquipmentData(IReadOnlyCollection<FixFormSummary> fixForms)
    {
        var data = new EquipmentData();
        for(int i= 0; i < RA020.leakageReasons.Length; i++)
        {
            data.LeakageReason[i] = fixForms.Count(x => x.Reason == RA020.leakageReasons[i]);
        }

        for (int i = 0; i < RA020.fixSituations.Length; i++)
        {
            data.FixSituation[i] = fixForms.Count(x => x.FixSituation == RA020.fixSituations[i]);
        }

        data.DailyAmount = fixForms.Select(x => x.DailyAmount ?? 0).Sum();
        data.TotalAmount = fixForms.Select(x => x.TotalAmount ?? 0).Sum();
        return data;
    }

    private DistributeEquipmentData GetDistributeEquipmentData(IReadOnlyCollection<FixFormSummary> fixForms)
    {
        var data = new DistributeEquipmentData();
        data.Count = fixForms.Count();
        data.LeakageAmount = fixForms.Select(x => x.TotalAmount ?? 0).Sum();
        return data;
    }


    private SituationData GetSituationData(IReadOnlyCollection<FixFormSummary> fixForms)
    {
        var data = new SituationData();
        data.Count = fixForms.Count();
        data.LeakageAmount = fixForms.Select(x => x.TotalAmount ?? 0).Sum();
        return data;
    }


    private SourceData GetSourceData(IReadOnlyCollection<FixFormSummary> fixForms)
    {
        var data = new SourceData();
        data.Count = fixForms.Count();
        data.LeakageAmount = fixForms.Select(x => x.TotalAmount ?? 0).Sum();
        return data;
    }


    /// <summary>
    /// 簡化只取必要欄位
    /// </summary>
    public class FixFormSummary
    {
        public Guid FormId { get; set; }

        /// <summary>
        /// 設備屬性
        /// </summary>
        public string EquipmentAttribute { get; set; }

        /// <summary>
        /// 管種
        /// </summary>
        public string  PipeKind { get; set; }

        // <summary>
        /// 附屬設備
        /// </summary>
        public string AccessoryEquipment { get; set; }


        /// <summary>
        /// 表箱另件 
        /// </summary>
        public string BoxAnnex { get; set; }


        /// <summary>
        /// 漏水原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 修理狀況
        /// </summary>
        public string FixSituation { get; set; }

        /// <summary>
        /// 日漏水量
        /// </summary>
        public decimal? DailyAmount { get; set; }

        /// <summary>
        /// 總漏水量
        /// </summary>
        public decimal? TotalAmount { get; set; }


        /// <summary>
        /// 送配水管線設備屬性(漏水情形的)
        /// </summary>
        public string DistributeEquipmentAttribute { get; set; }


        /// <summary>
        /// 案件來源
        /// </summary>
        public string Source { get; set; }


        /// <summary>
        /// 漏水情形
        /// </summary>
        public string Situation { get; set; }

    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA020[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
