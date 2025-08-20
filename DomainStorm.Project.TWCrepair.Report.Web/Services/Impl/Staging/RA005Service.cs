using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA005.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

public class RA005Service : IGetService<RA005, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA005Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA005> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA005 e => QueryRA005(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA005> QueryRA005(QueryRA005 condition) 
    {
        var result = new RA005();
        result.PrintDate = DateTime.Today;

        var fixForm = await _getRepository().GetAsync(condition.Id);
        if (fixForm.FixFormOutsourcingCost != null && fixForm.FixFormOutsourcingCost.FixFormOutsourcingCostItems != null)
        {
            fixForm.FixFormOutsourcingCost.FixFormOutsourcingCostItems = fixForm.FixFormOutsourcingCost.FixFormOutsourcingCostItems.OrderBy(x => x.Sort).ToList();
        }

        _mapper.Map(fixForm,result);
        result.HolidayCase = fixForm.FixFormDispatch != null && fixForm.FixFormDispatch.HolidayCase;


        if(fixForm.FixFormOutsourcingCost != null && fixForm.FixFormOutsourcingCost.Contractor != null)
        {
            result.Contractor = fixForm.FixFormOutsourcingCost.ContractorName!;
        }
        if(fixForm.FixFormDispatch != null && fixForm.FixFormDispatch.StartTime.HasValue)
        {
            result.StartTime = (fixForm.FixFormDispatch.StartTime.Value.Year - 1911) + "年" + fixForm.FixFormDispatch.StartTime.Value.ToString("MM年dd日HH時mm分");

        }
        else
        {
            result.StartTime = "　年　月　日　時　分";
        }
        

        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA005[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
