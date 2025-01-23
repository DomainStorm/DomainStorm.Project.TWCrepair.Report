using AutoMapper;
using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Repository.CommandModel.Report.V1;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl;

public class ReportInputOutputService<TViewModel, TInputModel, TQueryModel> : IReportInputOutputService<TViewModel, TInputModel, TQueryModel> where TQueryModel : IQuery
{
    private readonly IGetService<TViewModel, string> _viewModelGetService;
    private readonly IGetService<Stream, ReportConvertRequest> _reportService;
    private readonly IMapper _mapper;

    public ReportInputOutputService(IGetService<TViewModel, string> viewModelGetService, IMapper mapper, IGetService<Stream, ReportConvertRequest> reportService)
    {
        _viewModelGetService = viewModelGetService;
        _reportService = reportService;
        _mapper = mapper;
    }

    public Task<TViewModel> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<TViewModel> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return await _viewModelGetService.GetAsync<TQueryModel>(condition);
    }

    public Task<TViewModel[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TViewModel[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<TViewModel[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public TQueryModel InputToQueryMap(TInputModel inputModel)
    {
        return _mapper.Map<TInputModel, TQueryModel>(inputModel);
    }

    public Task<Stream> ConvertAsync(ReportConvertRequest reportConvertRequest)
    {
        return _reportService.GetAsync(reportConvertRequest);
    }
}