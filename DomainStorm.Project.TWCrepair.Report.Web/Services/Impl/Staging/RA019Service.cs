using AutoMapper;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.EventSourcing;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWCrepair.Report.Web.Views;
using FluentValidation;
using LinqKit;
using System.Security.Policy;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.RA019.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging;

/// <summary>
/// 漏水情形管制月報表
/// </summary>
public class RA019Service : IGetService<RA019, string>
{
    private readonly GetRepository<IRepository<Models.FixForm>> _getRepository;
    private readonly IGetService<Department, string> _departmentService;
    private readonly IMapper _mapper;

    public RA019Service(
        GetRepository<IRepository<Models.FixForm>> getRepository,
        IGetService<Department, string> departmentService,
        IMapper mapper)
    {
        _getRepository = getRepository;
        _departmentService = departmentService;
        _mapper = mapper;
    }

    public Task<RA019> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        return condition switch
        {
            QueryRA019 e => QueryRA019(e),
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    private async Task<RA019> QueryRA019(QueryRA019 condition) 
    {
        var result = new RA019();

        var department = await _departmentService.GetAsync(condition.DepartmentId.ToString());
        result.DepartmentName = department.Name;

        if (condition.SiteId.HasValue)
        {
            var site = await _departmentService.GetAsync(condition.SiteId.Value.ToString());
            result.Items.Add(new RA019Item
            {
                SiteId = site.DepartmentId,
                SiteName = site.Name
            });

            result.SiteName = site.Name;
        }
        else
        {
            var children =( await _departmentService.GetAsync<QueryChildren>(new QueryChildren {
                ParentDepartmentId = condition.DepartmentId
            })).Departments;

            foreach (var site in children!)
            {
                result.Items.Add(new RA019Item
                {
                    SiteId = site.DepartmentId,
                    SiteName = site.Name
                });
            }
        }

        var pb = PredicateBuilder.New<Models.FixForm>();
        var exp = pb.Start(x => !x.IsRetrieved && !x.Deleted && x.ResponsibleReginId == condition.DepartmentId);   //排除移辦取回
        if(condition.SiteId.HasValue)
        {
            exp = pb.And(x => x.ResponsibleDepartmentId == condition.SiteId);
        }

        DateTime beginDate, endDate;
        if(!string.IsNullOrEmpty(condition.YearMonth))
        {
            if(!DateTime.TryParse(condition.YearMonth + "/01", out beginDate))
            {
                throw new ValidationException($"年月錯誤:{condition.YearMonth}");
            }
            endDate = beginDate.AddMonths(1);
            result.DateRange = beginDate.ToString("yyyy年MM月");
        }
        else if(condition.AcceptanceDateBegin.HasValue && condition.AcceptanceDateEnd.HasValue)
        {
            beginDate = condition.AcceptanceDateBegin.Value;
            endDate = condition.AcceptanceDateEnd.Value.AddDays(1);
            result.DateRange = $"從{condition.AcceptanceDateBegin.Value.ToString("yyyy-MM-dd")}至{condition.AcceptanceDateEnd.Value.ToString("yyyy-MM-dd")}";
        }
        else if(condition.Year.HasValue &&  !string.IsNullOrEmpty(condition.Season))
        {
            int season = 0;
            //第一季,第二季,第三季,第四季
            if(condition.Season == "第一季")
            {
                season = 1;
            }
            else if (condition.Season == "第二季")
            {
                season = 2;
            }
            else if (condition.Season == "第三季")
            {
                season = 3;
            }
            else if (condition.Season == "第四季")
            {
                season = 4;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(condition.Season), condition.Season, null);
            }
            result.DateRange = $"{condition.Year}年{condition.Season}";

            var month = 3 * (season - 1) + 1;
            beginDate = new DateTime(condition.Year.Value, month, 1);
            endDate = beginDate.AddMonths(3);
        }
        else if (condition.Year.HasValue &&  !string.IsNullOrEmpty(condition.Half))
        {
            int month, monthCount;
            if(condition.Half=="全年度")
            {
                month = 1;
                monthCount = 12;
            }
            else if (condition.Half == "上半年")
            {
                month = 1;
                monthCount = 6;
            }
            else if (condition.Half == "下半年")
            {
                month = 7;
                monthCount = 6;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(condition.Half), condition.Half, null);
            }
            result.DateRange = $"{condition.Year}年{condition.Half}";
            beginDate = new DateTime(condition.Year.Value, month, 1);
            endDate = beginDate.AddMonths(monthCount);
        }
        else
        {
            throw new ValidationException("各項日期條件皆未輸入正確");
            
        }

        exp = pb.And(x => x.AcceptanceTime >= beginDate && x.AcceptanceTime < endDate);

        var fixForms = await _getRepository().GetListAsync<FixFormSummary>(exp, x => new FixFormSummary
        {
            ResponsibleReginId = x.ResponsibleReginId,
            ResponsibleReginName = x.ResponsibleReginName,
            ResponsibleDepartmentId = x.ResponsibleDepartmentId,
            ResponsibleDepartmentName = x.ResponsibleDepartmentName,
            Dispatch = x.FixFormDispatch != null,
            DispatchTime = x.FixFormDispatch != null ? x.FixFormDispatch.DispatchTime : null,
            FixDeadline = x.FixFormProperty != null?  x.FixFormProperty.FixDeadline : null,
            FixTime = x.FixFormProperty != null ? x.FixFormProperty.FixTime : null,
            CaseEmergency = x.CaseEmergency!.Name
        });


        foreach(var site in result.Items)
        {
            var siteItems = fixForms.Where(x => x.ResponsibleDepartmentId == site.SiteId);
            site.UnDispatch = siteItems.Count(x => !x.Dispatch);
            site.UnUrgentCase = FixFormSummary.Calculate(siteItems.Where(x => x.CaseEmergency != "緊急" && x.Dispatch).ToArray());
            site.UrgentCase = FixFormSummary.Calculate(siteItems.Where(x => x.CaseEmergency == "緊急" && x.Dispatch).ToArray());
        }
        result.Sum();

        return result;
    }

    /// <summary>
    /// 資料量很大,先簡化只取必要欄位
    /// </summary>
    public class FixFormSummary
    {
        /// <summary>
        /// 權責單位(區處)
        /// </summary>
        public Guid? ResponsibleReginId { get; set; }

        /// <summary>
        /// 權責單位名稱(區處)
        /// </summary>
        public string? ResponsibleReginName { get; set; }


        /// <summary>
        /// 權責單位(廠所)
        /// </summary>
        public Guid? ResponsibleDepartmentId { get; set; }

        /// <summary>
        /// 權責單位名稱(廠所)
        /// </summary>
        public string? ResponsibleDepartmentName { get; set; }


        /// <summary>
        /// 是否已派工
        /// </summary>
        public bool Dispatch { get; set; }


        /// <summary>
        /// 派工時間
        /// </summary>

        public DateTime? DispatchTime { get; set; }

        /// <summary>
        /// 修復期限
        /// </summary>
        public DateTime? FixDeadline { get; set; }

        /// <summary>
        /// 修復時間
        /// </summary>
        public DateTime? FixTime { get; set; }

        /// <summary>
        /// 緊急性
        /// </summary>
        public string CaseEmergency { get; set; }


        private double? _overDueDays { get; set; }
        private double OverDueDays
        {
            get
            {
                if (!_overDueDays.HasValue)
                {
                    if (FixDeadline.HasValue && FixTime.HasValue && FixTime.Value.Date > FixDeadline.Value.Date)
                    {
                        _overDueDays =( FixTime.Value.Date - FixDeadline.Value.Date).TotalDays;
                    }
                    else
                    {
                        _overDueDays = -1;
                    }
                }
                return _overDueDays.Value;
            }

        }


        public  static RA019_CaseNumber Calculate(IReadOnlyCollection<FixFormSummary> fixforms)
        {
            var result = new RA019_CaseNumber();
            result.Dispatch = fixforms.Count();
            result.FinishNotOverDue = fixforms.Count(x => x.FixTime.HasValue && x.FixTime <= x.FixDeadline);
            result.FinishOverDue1 = fixforms.Count(x => x.OverDueDays > 0 && x.OverDueDays <= 1);
            result.FinishOverDue3 = fixforms.Count(x => x.OverDueDays > 1 && x.OverDueDays <=3 );
            result.FinishOverDueOver3 = fixforms.Count(x => x.OverDueDays > 3);
            return result;
        }
    }

    public Task<DateTime> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RA019[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
    {
        throw new NotImplementedException();
    }

    public Task<DateTime[]> GetListAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    

    
}
