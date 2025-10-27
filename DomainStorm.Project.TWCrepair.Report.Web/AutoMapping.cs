using AutoMapper;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using DomainStorm.Project.TWCrepair.Shared.ViewModel;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
namespace DomainStorm.Project.TWCrepair.Report.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Models.FixForm, RA002>();
            CreateMap<Models.FixForm, RA003>()
                .ForMember(x => x.GPSTmX, opt => opt.MapFrom(src => src.FixFormProperty != null ? src.FixFormProperty.GPSTmX : null))
                .ForMember(x => x.GPSTmY, opt => opt.MapFrom(src => src.FixFormProperty != null ? src.FixFormProperty.GPSTmY : null));
            CreateMap<Models.FixForm, RA004>();
            CreateMap<Models.FixForm, RA005>();
            CreateMap<Models.FixForm, TWCrepair.Shared.ViewModel.RA019FixForm>();
            CreateMap<Models.Budget.BudgetDoc, RA006>()
                .ForMember(dest => dest.PlanStartDate, opt => opt.MapFrom(src => src.PlanStartDate.HasValue ? src.PlanStartDate.Value.ToString("yyyy-MM-dd") : null))
                .ForMember(dest => dest.PlanEndDate, opt => opt.MapFrom(src => src.PlanEndDate.HasValue ? src.PlanEndDate.Value.ToString("yyyy-MM-dd") : null));
            CreateMap<Models.Budget.BudgetDoc, RA007>();
            CreateMap<Models.Budget.BudgetDoc, RA007>();
            CreateMap<Models.Budget.BudgetDoc, RA008>();
            CreateMap<Models.Budget.BudgetDoc, RA010>();
            CreateMap<Models.Budget.BudgetDoc, RA011>();
            CreateMap<Models.Budget.BudgetDoc, RA012>();
            CreateMap<Models.Budget.BudgetDoc, RA014>();
            CreateMap<Models.Budget.BudgetDoc, RA015>();
            CreateMap<Models.Budget.BudgetDocContract, RA017>();
            CreateMap<Models.Budget.BudgetDocContract, RA018>();
            CreateMap<Models.YearPlan.YearPlanReport, RA028>();
            CreateMap<Models.YearPlan.YearPlanReportInstrument, RA028Instrument>();
            CreateMap<Models.YearPlan.YearPlanReport, RA029>();
            CreateMap<Models.YearPlan.YearPlanWorkSpace, RA029WorkSpace>();
            CreateMap<Models.YearPlan.YearPlanSetAllZone, YearPlanStatistics>();
            CreateMap<Models.YearPlan.YearPlanWorkSpace, RA032Item>();
            CreateMap<Models.YearPlan.YearPlanWorkSpace, RA034Item>();
            CreateMap<Models.YearPlan.YearPlanBase, YearPlanExpenseAllocate>();
            CreateMap<Models.YearPlan.YearPlanWorkSpace, YearPlanExpenseAllocateWorkSpace>();
            CreateMap<Models.DepartmentWorkSpace, DepartmentWorkSpaceSimple>();


            CreateMap<Models.FixFormDispatch, TWCrepair.Shared.ViewModel.FixFormDispatch>();
            CreateMap<Models.Budget.BudgetDocContract, TWCrepair.Shared.ViewModel.FixFormDispatchContract>();
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

            CreateMap<Models.Budget.BudgetDocOutSourceUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocOutSourceDetailItem>()
                .ForMember(vm => vm.DayPrice, opt => opt.MapFrom(m => m.TotalDayPrice))
                .ForMember(vm => vm.NightPrice, opt => opt.MapFrom(m => m.TotalNightPrice));
            CreateMap<Models.Budget.BudgetDocOutSource, TWCrepair.Shared.ViewModel.BudgetDocOutSourceDetail>()
                .ForMember(vm => vm.BudgetDocOutSourceDetailItems, opt => opt.MapFrom(m => m.BudgetDocOutSourceUnitPrices));
            CreateMap<Models.Budget.BudgetDocOutSourceUnitPriceMember, TWCrepair.Shared.ViewModel.BudgetDocOutSourceUnitPriceMember>();
            CreateMap<Models.Budget.BudgetDocOutSourceUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocOutSourceUnitPrice>();
            CreateMap<Models.Budget.BudgetDocOutSource, TWCrepair.Shared.ViewModel.BudgetDocOutSourceResourceStatistics>();
            CreateMap<Models.Budget.ResourceWorkMaterial, TWCrepair.Shared.ViewModel.BudgetDocOutSourceResourceStatisticsItem>();

            CreateMap<Models.Budget.BudgetDocContractUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocContractDetailItem>()
               .ForMember(vm => vm.DayPrice, opt => opt.MapFrom(m => m.TotalDayPrice))
               .ForMember(vm => vm.NightPrice, opt => opt.MapFrom(m => m.TotalNightPrice));
            CreateMap<Models.Budget.BudgetDocContract, TWCrepair.Shared.ViewModel.BudgetDocContractDetail>()
                .ForMember(vm => vm.BudgetDocContractDetailItems, opt => opt.MapFrom(m => m.BudgetDocContractUnitPrices));
            CreateMap<Models.Budget.BudgetDocContractUnitPriceMember, TWCrepair.Shared.ViewModel.BudgetDocContractUnitPriceMember>();
            CreateMap<Models.Budget.BudgetDocContractUnitPrice, TWCrepair.Shared.ViewModel.BudgetDocContractUnitPrice>();
            CreateMap<Models.Budget.BudgetDocContract, TWCrepair.Shared.ViewModel.BudgetDocContractResourceStatistics>();
            CreateMap<Models.Budget.ContractResourceWorkMaterial, TWCrepair.Shared.ViewModel.BudgetDocContractResourceStatisticsItem>();
            CreateMap<Models.Budget.ResourceWorkMaterial, TWCrepair.Shared.ViewModel.ResourceWorkMaterial>();

            CreateMap<Models.YearPlan.YearPlanReportInstrument, RA039_Item>();
            CreateMap<Models.ExecuteControl, RA044_Item>();

        }
    }
}
