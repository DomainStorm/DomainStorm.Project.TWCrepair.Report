using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA043.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 毀損計算營業損失
/// </summary>
public class RA043Service : IGetService<RA043, string>
{
    private readonly GetRepository<IRepository<Repository.Models.FixForm>> _getRepository;
    private readonly GetRepository<IRepository<Repository.Models.Word>> _getWordRepository;

    private IMapper _mapper;

    public RA043Service(
        GetRepository<IRepository<Repository.Models.FixForm>> getRepository,
        GetRepository<IRepository<Repository.Models.Word>> getWordRepository,
       IMapper mapper
        )
    {
        _getRepository = getRepository;
        _getWordRepository = getWordRepository;
        _mapper = mapper;
    }

    public Task<RA043> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA043 e => QueryRA043(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA043> QueryRA043(QueryRA043 condition)
    {
        var fixForm = await _getRepository().GetAsync(condition.Id);
        var result = new RA043
        {
            FixCaseNo = fixForm.FixCaseNo
        };

        var diameter = fixForm.FixFormDispatch.PipeDiameter?.Name!;
        result.Diameter = int.Parse(diameter);
        result.Material_Cost = fixForm.FinalCost.FinalCost_Material ?? 0;
        result.Outsourcing_Cost = fixForm.FinalCost.FinalCost_Outsourcing ?? 0;

        result.Miscellaneous = (int)Math.Round(
            (result.Material_CostInt + result.Outsourcing_CostInt) * 1.5M,
            0, MidpointRounding.AwayFromZero);

        result.RoadRepairCost = (int)Math.Round(fixForm.FixFormProperty?.RoadRepairCost ?? 0 , 2, MidpointRounding.AwayFromZero);
        result.Other = (int)Math.Round(
            fixForm.FixFormDigFill?.AsphaltProxyCost ?? 0 + fixForm.FixFormDigFill?.ConcreteProxyCost ?? 0,
            0, MidpointRounding.AwayFromZero);

        result.Duration = (int)(fixForm.FixFormLeakage?.Duration * 60 * 60);
        result.Area = fixForm.FixFormLeakage?.Area;
        result.PressureBefore = fixForm.FixFormLeakage?.PressureBefore;

        if(fixForm.FixFormProperty != null && fixForm.FixFormProperty.FixTime.HasValue
            && fixForm.FixFormDispatch != null && fixForm.FixFormDispatch.StartTime.HasValue )
        {
            var minutes =  (fixForm.FixFormProperty!.FixTime.Value - fixForm.FixFormDispatch.StartTime.Value).TotalMinutes;
            result.HourToClose = (int)Math.Ceiling(minutes / 60.0);
        }

        var temp = RA043DiamterMapToUnit.RA043DiamterMapToUnits.FirstOrDefault(x => x.Diameter == result.Diameter);
        if(temp != null)
        {
            result.UnitOfWater2 = temp.UnitOfWater;
        }
        else if(result.Diameter < RA043DiamterMapToUnit.RA043DiamterMapToUnits.First().Diameter)
        {
            result.UnitOfWater2 = RA043DiamterMapToUnit.RA043DiamterMapToUnits.First().UnitOfWater;
        }
        else if(result.Diameter > RA043DiamterMapToUnit.RA043DiamterMapToUnits.Last().Diameter)
        {
            result.UnitOfWater2 = RA043DiamterMapToUnit.RA043DiamterMapToUnits.Last().UnitOfWater;
        }
        else
        {
            throw new Exception($"從管徑({result.Diameter})無法取得【照本公司挖斷管線應賠償營業損失計算標準辦理】之度數");

        }
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA043[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }




}
