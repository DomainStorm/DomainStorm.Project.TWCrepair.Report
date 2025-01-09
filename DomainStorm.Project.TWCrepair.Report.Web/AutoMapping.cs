using AutoMapper;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
namespace DomainStorm.Project.TWCrepair.Report.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Models.FixForm, RA002>();
            CreateMap<Models.FixFormDispatch, RA002_FixFormDispatch>();
            CreateMap<Models.FixFormProperty, RA002_FixFormProperty>();
            CreateMap<Models.Word, ViewModel.Word>();
        }
    }
}
