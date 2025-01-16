using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA004.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

public class RA004Service : IGetService<RA004, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA004Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA004> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA004> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA004 e => QueryRA004(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA004> QueryRA004(QueryRA004 condition) 
    {
        var result = new RA004();
        result.PrintDate = DateTime.Today;

        var fixForm = await _getRepository().GetAsync(condition.Id);
        _mapper.Map(fixForm, result);

        if(fixForm.FixFormOutsourcingCost != null && fixForm.FixFormOutsourcingCost.Contractor != null)
        {
            result.Contractor = fixForm.FixFormOutsourcingCost.Contractor.Name;
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

    public Task<RA004[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA004[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA004[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
