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
            CreateMap<Models.FixForm, RA003>();
            CreateMap<Models.FixForm, RA004>();
            CreateMap<Models.FixForm, RA005>();
            CreateMap<Models.Budget.BudgetDoc, RA006>();
            CreateMap<Models.Budget.BudgetDoc, RA007>();
            CreateMap<Models.Budget.BudgetDoc, RA008>();
            CreateMap<Models.Budget.BudgetDoc, RA010>();
            CreateMap<Models.Budget.BudgetDoc, RA011>();
            CreateMap<Models.FixFormDispatch, TWCrepair.Shared.ViewModel.FixFormDispatch>();
            CreateMap<Models.FixFormProperty, TWCrepair.Shared.ViewModel.FixFormProperty>();
            CreateMap<Models.Word, TWCrepair.Shared.ViewModel.Word >();
            CreateMap<Models.FixFormDigFillItem, TWCrepair.Shared.ViewModel.FixFormDigFillItem>();
            CreateMap<Models.FixFormDigFillAsphaltRepair, TWCrepair.Shared.ViewModel.FixFormDigFillAsphaltRepair>();
            CreateMap<Models.FixFormDigFill, TWCrepair.Shared.ViewModel.FixFormDigFill>()
                .ForMember(vm => vm.ExcavatorItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.Excavator).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ManualItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.Manuaul).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.AsphaltRemoveItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.AsphaltRemove).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ConcreteRemoveItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.ConcreteRemove).OrderBy(x => x.Sort)))
                .ForMember(vm => vm.ConcreteRepairItems, opt => opt.MapFrom(m => m.FixFormDigFillItems.Where(x => x.DigFillItemType == Models.FixFormDigFillItemType.ConcreteRepair).OrderBy(x => x.Sort)));
            CreateMap<Models.FixFormLeakage, TWCrepair.Shared.ViewModel.FixFormLeakage >();
            CreateMap<Models.AttachmentFile, TWCrepair.Shared.ViewModel.AttachmentFile>();
            CreateMap<Models.FixFormAuditSupervisor, TWCrepair.Shared.ViewModel.FixFormAuditSupervisor>();
            CreateMap<Models.FixFormAudit, TWCrepair.Shared.ViewModel.FixFormAudit>();
            CreateMap<Models.FixFormAuditAttachment, TWCrepair.Shared.ViewModel.FixFormAuditAttachment>()
                .ForMember(vm => vm.Url, opt => opt.MapFrom(m => m.AttachmentFile.Url))
                .ForMember(vm => vm.ThumbnailUrl, opt => opt.MapFrom(m => m.AttachmentFile.ThumbnailUrl))
                .ForMember(vm => vm.Category, opt => opt.MapFrom(m => m.AttachmentFile.Category))
                .ForMember(vm => vm.Description, opt => opt.MapFrom(m => m.AttachmentFile.Description))
                .ForMember(vm => vm.Name, opt => opt.MapFrom(m => m.AttachmentFile.Name));

            CreateMap<Models.FixFormMaterialCostItem, TWCrepair.Shared.ViewModel.FixFormMaterialCostItem>();
            CreateMap<Models.Material, TWCrepair.Shared.ViewModel.Material>();
            CreateMap<Models.FixFormScrapCostItem, TWCrepair.Shared.ViewModel.FixFormScrapCostItem>();
            CreateMap<Models.FixFormOutsourcingCost, TWCrepair.Shared.ViewModel.FixFormOutsourcingCost>();
            CreateMap<Models.FixFormOutsourcingCostItem, TWCrepair.Shared.ViewModel.FixFormOutsourcingCostItem>();
            CreateMap<Models.Budget.BudgetDocUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocDetailItem>()
                .ForMember(vm => vm.DayPrice, opt => opt.MapFrom(m => m.TotalDayPrice))
                .ForMember(vm => vm.NightPrice, opt => opt.MapFrom(m => m.TotalNightPrice));
            CreateMap<Models.Budget.BudgetDoc, TWCrepair.Shared.ViewModel.BudgetDocDetail>()
                .ForMember(vm => vm.BudgetDocDetailItems, opt => opt.MapFrom(m => m.BudgetDocUnitPrices));
            CreateMap<Models.Budget.BudgetDocUnitPriceMember, TWCrepair.Shared.ViewModel.BudgetDocUnitPriceMember>();
            CreateMap<Models.Budget.BudgetDocUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocUnitPrice>();
            CreateMap<Models.Budget.ResourceWorkMaterial, TWCrepair.Shared.ViewModel.BudgetDocResourceStatisticsItem>();
        }
    }
}
