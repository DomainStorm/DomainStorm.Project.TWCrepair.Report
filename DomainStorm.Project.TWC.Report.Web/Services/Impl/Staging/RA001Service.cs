using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA001.V1;
using Models = DomainStorm.Project.TWC.Web.Models;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Web.CommandModel.Department.V1;
using LinqKit;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Authentication;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
{
    public class RA001Service : IGetService<RA001, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<Department, string> _departmentService;
        private readonly GetRepository<IRepository<Models.WaterRegisterChangeForm>> _waterRegisterChangeForm;

        public RA001Service(
            TokenProvider tokenProvider,
            ICache cache,
            IGetService<Department, string> departmentService,
            GetRepository<IRepository<Models.WaterRegisterChangeForm>> waterRegisterChangeForm
        )
        {
            _tokenProvider = tokenProvider;
            _cache = cache; 
            _departmentService = departmentService;
            _waterRegisterChangeForm = waterRegisterChangeForm;

        }
        public Task<RA001> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryRA001 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class WaterRegisterChangeForm_RA01_Model
        {
            public string OperatingArea { get; set; }
        }
        public async Task<RA001> Query(QueryRA001 condition)
        {
            var mainClaimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache)!;
            if (mainClaimsIdentity == null) throw new ArgumentNullException(nameof(mainClaimsIdentity));

            var report = new RA001
            {
                ApplyDateBegin = condition.ApplyDateBegin,
                ApplyDateEnd = condition.ApplyDateEnd,
                UserName = mainClaimsIdentity!.Name,
                DepartmentName = condition.DepartmentIds != null && condition.DepartmentIds.Any() ?  
                    (await _departmentService.GetAsync(condition.DepartmentIds[0].ToString()) ).Name
                    : ""            
            };
            condition.ApplyDateEnd = condition.ApplyDateEnd.AddDays(1);

            var sites = await _departmentService.GetAsync<QuerySite>(new QuerySite
            {
                DepartmentIds = condition.DepartmentIds
            });
            var anotherCodes = sites.Departments!.Select(x => x.AnotherCode).ToArray();

            var pb = PredicateBuilder.New<Models.WaterRegisterChangeForm>();
            var exp = pb.Start(x => x.ApplyDate >= condition.ApplyDateBegin && x.ApplyDate < condition.ApplyDateEnd && x.SerialNumber != null);
            if (condition.DepartmentIds != null && condition.DepartmentIds.Any()) //有指定單位的話, 原始資料的查詢也要限制
            {
                exp = pb.And(x => anotherCodes.Contains(x.OperatingArea));   
            }

            var data = await _waterRegisterChangeForm().GetListAsync<WaterRegisterChangeForm_RA01_Model>(
                exp,
                x => new WaterRegisterChangeForm_RA01_Model { OperatingArea = x.OperatingArea } );


            foreach (var site in sites.Departments!)
            {
                report.Items.Add(new RA001_Item
                {
                    AnotherCode = site.AnotherCode,
                    Name = site.Name,
                    Count = data.Count(x => x.OperatingArea == site.AnotherCode)
                });
            }
            report.Items.Add(new RA001_Item
            {
                AnotherCode = "",
                Name = "總計",
                Count = report.Items.Sum(x => x.Count)
            });
            return report;
        }


        public Task<RA001[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RA001[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
