using AutoMapper;
using DomainStorm.Project.TWC.Report.Web.InputModel;
using DomainStorm.Project.TWC.Report.Web.Views;
using DomainStorm.Project.TWC.Web.Models;
using RA001 = DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA001;
using RA999 = DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA999;
using RA002 = DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA002;
namespace DomainStorm.Project.TWC.Report.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RA001_InputModel, RA001.V1.QueryRA001>().ForMember(d => d.DepartmentIds, opt => opt.MapFrom(src => new List<Guid>{ (Guid)src.DepartmentId! }));
            CreateMap<RA002_InputModel, RA002.V1.QueryRA002>().ForMember(d => d.DepartmentIds, opt => opt.MapFrom(src => new List<Guid>{ (Guid)src.DepartmentId! }));
            CreateMap<RA999_InputModel, RA999.V1.QueryRA999>().ForMember(d => d.DepartmentIds, opt => opt.MapFrom(src => new List<Guid>{ (Guid)src.DepartmentId! }));

            CreateMap<WaterRegisterChangeForm, RA999_Item>()
                .ForMember(item => item.TypeChangeName,
                           m => m.MapFrom(w => ViewModel.WaterRegisterChangeForm.GetTypeChangeName(w.TypeChange)));

        }
    }
}
