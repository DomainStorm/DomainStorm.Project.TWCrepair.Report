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
            CreateMap<Models.FixFormDigFillItem, ViewModel.FixFormDigFillItem>();
            CreateMap<Models.FixFormDigFillAsphaltRepair, ViewModel.FixFormDigFillAsphaltRepair>();
            CreateMap<Models.FixFormDigFill, ViewModel.FixFormDigFill>()
                .ForMember(vm => vm.ExcavatorItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.Excavator).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ManualItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.Manuaul).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.AsphaltRemoveItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.AsphaltRemove).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ConcreteRemoveItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.ConcreteRemove).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ConcreteRepairItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.ConcreteRepair).OrderBy(x => x.Sort)));

        }
    }
}
