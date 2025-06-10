using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Project.TWCrepair.Repository.Models.Import;
using DomainStorm.Project.TWCrepair.Repository.Models.YearPlan;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA024.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 管線漏水密度及修理費用
/// </summary>
public class RA024Service : IGetService<RA024, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly GetRepository<IRepository<ImportPipe>> _getImportPipeRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA024Service(
        GetRepository<IRepository<FixForm>> getRepository,
        GetRepository<IRepository<ImportPipe>> getYearPlanRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _getImportPipeRepository = getYearPlanRepository;
        _mapper = mapper;
        _departmentService = departmentService;
    }

    public Task<RA024> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA024 e => QueryRA024(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA024> QueryRA024(QueryRA024 condition) 
    {
        var result = new RA024();
        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        result.DepartmentName = department.Name;

        if (condition.SiteId.HasValue)
        {
            var site = await _departmentService.GetAsync(condition.SiteId.Value.ToString());
            result.SiteName = site.Name;
        }
       

        var pb = PredicateBuilder.New<FixForm>();
        var pbImportPipe = PredicateBuilder.New<ImportPipe>();

        var exp = pb.Start(x => !x.IsRetrieved && !x.Deleted //排除移辦取回
                && x.ResponsibleReginId == condition.DepartmentId && x.FixFormProperty != null
                && x.FixFormProperty.CaseAttribute != null && x.FixFormProperty.CaseAttribute.Name == "漏水案件"
                && x.FixFormProperty.EquipmentAttribute != null && x.FixFormProperty.EquipmentAttribute.Name == "管線");


        var expImportPipe = pbImportPipe.Start(x => x.DepartmentId == condition.DepartmentId);


        if (condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.ResponsibleDepartmentId == condition.SiteId);
            expImportPipe = expImportPipe.And(x => x.SiteId == condition.SiteId);
        }

        condition.GetRange();
        result.DateRange = condition.DateRange;

        exp = pb.And(x => x.AcceptanceTime >= condition.FinalBeginDate && x.AcceptanceTime < condition.FinalEndDate);
        expImportPipe = expImportPipe.And(x => x.DataDate >= condition.FinalBeginDate && x.DataDate < condition.FinalEndDate);



        var fixForms = await _getRepository().GetListAsync<RA024FixForm>(exp, x => new RA024FixForm
        {
            FormId =  x.FormId,
            PipeKind = x.FixFormProperty.PipeKind != null ? x.FixFormProperty.PipeKind.Name : "",
            PipeDiameter = x.FixFormProperty.PipeDiameter != null ? x.FixFormProperty.PipeDiameter.Name : "",
            FinalCost_Total = x.FinalCost.FinalCost_Total,
        });

        var importPipes = await _getImportPipeRepository().GetListAsync<RA024ImportPipe>(expImportPipe, x => new RA024ImportPipe
        {
            PipeKind = x.PipeCode,
            PipeDiameter = x.Width.ToString(),
            Length = x.Length
        });
        result.GenerateData(fixForms, importPipes);

        
        return result;
    }

    public class YearPlanItemData
    {
        public double Amount { get; set; }

        public YearPlanSetAllZoneDataType DataType { get; set; }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA024[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
