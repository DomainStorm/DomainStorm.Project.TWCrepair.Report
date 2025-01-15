using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Repository.Models;
using FluentValidation;
using LinqKit;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA002.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

public class RA002Service : IGetService<RA002, string>
{
    private readonly GetRepository<IRepository<FixForm>> _getRepository;
    private readonly IMapper _mapper;

    public RA002Service(
        GetRepository<IRepository<FixForm>> getRepository,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _mapper = mapper;
    }

    public Task<RA002> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA002> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA002 e => QueryRA002(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA002> QueryRA002(QueryRA002 condition) 
    {
        var result = new RA002();
        result.PrintDate = DateTime.Today;

        var fixForm = await _getRepository().GetAsync(condition.Id);
        fixForm.Sorting();
        if(fixForm.FixFormOutsourcingCost != null)
        {
            result.FinalCost_Outsourcing = fixForm.FixFormOutsourcingCost.FinalTotal;
        }
        if(fixForm.FixFormDigFill != null)
        {
            result.FinalCost_RoadRightProxy = (fixForm.FixFormDigFill.AsphaltProxyCost ?? 0)
                + (fixForm.FixFormDigFill.ConcreteProxyCost ?? 0);
        }
        if(fixForm.FixFormMaterialCostItems != null)
        {
            result.FinalCost_Material = fixForm.FixFormMaterialCostItems.Sum(x => x.TotalPrice);
        }
        //if (fixForm.FixFormScrapCostItems != null)
        //{
        //    result.FinalCost_Material += fixForm.FixFormScrapCostItems.Sum(x => x.TotalPrice);
        //}



        result.FinalCost_Total = (result.FinalCost_Outsourcing ?? 0)
            + (result.FinalCost_RoadRightProxy ?? 0)
            + (result.FinalCost_EmployeeSalary ?? 0)
            + (result.FinalCost_Material ?? 0)
            + (result.FinalCost_Other ?? 0);

        _mapper.Map(fixForm, result);
        
        return result;
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA002[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA002[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA002[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
