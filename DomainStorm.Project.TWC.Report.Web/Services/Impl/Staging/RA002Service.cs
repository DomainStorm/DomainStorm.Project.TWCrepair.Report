using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA002.V1;
using Models = DomainStorm.Project.TWC.Web.Models;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Web.CommandModel.Department.V1;
using LinqKit;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Authentication;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
{
    public class RA002Service : IGetService<RA002, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<Department, string> _departmentService;
        private readonly GetRepository<IRepository<Models.WaterRegisterChangeForm>> _waterRegisterChangeForm;


        public RA002Service(
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
        public Task<RA002> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA002> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryRA002 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class WaterRegisterChangeForm_RA002_Model
        {
            public string OperatingArea { get; set; }
            public DateTime ApplyDate { get; set; }
        }
        public async Task<RA002> Query(QueryRA002 condition)
        {
            var mainClaimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache)!;
            if (mainClaimsIdentity == null) throw new ArgumentNullException(nameof(mainClaimsIdentity));

            DateTime beginDate = DateTime.Parse($"{condition.Year}/1/1");
            DateTime endDate = beginDate.AddYears(1);

            var report = new RA002
            {
                Year = condition.Year,
                UserName = mainClaimsIdentity!.Name,
                DepartmentName = condition.DepartmentIds != null && condition.DepartmentIds.Any() ?
                    (await _departmentService.GetAsync(condition.DepartmentIds[0].ToString())).Name
                    : ""
            };

            var sites = await _departmentService.GetAsync<QuerySite>(new QuerySite
            {
                DepartmentIds = condition.DepartmentIds
            });
            var anotherCodes = sites.Departments!.Select(x => x.AnotherCode).ToArray();

            var pb = PredicateBuilder.New<Models.WaterRegisterChangeForm>();
            var exp = pb.Start(x => x.ApplyDate >= beginDate && x.ApplyDate < endDate && x.SerialNumber != null);
            if (condition.DepartmentIds != null && condition.DepartmentIds.Any()) //有指定單位的話, 原始資料的查詢也要限制
            {
                exp = pb.And(x => anotherCodes.Contains(x.OperatingArea));   
            }

            var data = await _waterRegisterChangeForm().GetListAsync<WaterRegisterChangeForm_RA002_Model>(
                exp,
                x => new WaterRegisterChangeForm_RA002_Model { 
                    OperatingArea = x.OperatingArea ,
                    ApplyDate = x.ApplyDate
                } );


            foreach (var site in sites.Departments!)
            {
                var siteCases = data.Where(x => x.OperatingArea == site.AnotherCode);
                var item = new RA002_Item
                {
                    AnotherCode = site.AnotherCode,
                    Name = site.Name,
                    C1 = siteCases.Count(x => x.ApplyDate.Month == 1),
                    C2 = siteCases.Count(x => x.ApplyDate.Month == 2),
                    C3 = siteCases.Count(x => x.ApplyDate.Month == 3),
                    C4 = siteCases.Count(x => x.ApplyDate.Month == 4),
                    C5 = siteCases.Count(x => x.ApplyDate.Month == 5),
                    C6 = siteCases.Count(x => x.ApplyDate.Month == 6),
                    C7 = siteCases.Count(x => x.ApplyDate.Month == 7),
                    C8 = siteCases.Count(x => x.ApplyDate.Month == 8),
                    C9 = siteCases.Count(x => x.ApplyDate.Month == 9),
                    C10 = siteCases.Count(x => x.ApplyDate.Month == 10),
                    C11 = siteCases.Count(x => x.ApplyDate.Month == 11),
                    C12 = siteCases.Count(x => x.ApplyDate.Month == 12)
                };
                item.Total = item.C1 + item.C2 + item.C3 + item.C4 + item.C5 + item.C6 + item.C7 + item.C8 + item.C9 + item.C10 + item.C11 + item.C12;
                report.Items.Add(item);
            }

            var sitesTotal = new RA002_Item
            {
                AnotherCode = "",
                Name = "總計",
                C1 = report.Items.Sum(x => x.C1),
                C2 = report.Items.Sum(x => x.C2),
                C3 = report.Items.Sum(x => x.C3),
                C4 = report.Items.Sum(x => x.C4),
                C5 = report.Items.Sum(x => x.C5),
                C6 = report.Items.Sum(x => x.C6),
                C7 = report.Items.Sum(x => x.C7),
                C8 = report.Items.Sum(x => x.C8),
                C9 = report.Items.Sum(x => x.C9),
                C10 = report.Items.Sum(x => x.C10),
                C11 = report.Items.Sum(x => x.C11),
                C12 = report.Items.Sum(x => x.C12),
                Total = report.Items.Sum(x => x.Total)
            };
            report.Items.Add(sitesTotal);

            return report;
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
    }
}
