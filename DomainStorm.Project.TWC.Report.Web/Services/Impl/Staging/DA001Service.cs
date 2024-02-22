using DomainStorm.Framework;
using DomainStorm.Framework.Authentication;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using DomainStorm.Project.TWC.Report.Web.Views.Dashboards;
using LinqKit;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.DA001.V1;
using static DomainStorm.Project.TWC.Web.CommandModel.Department.V1;
using Models = DomainStorm.Project.TWC.Web.Models;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
{
    public class DA001Service : IGetService<DA001, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<Department, string> _departmentService;
        private readonly GetRepository<IRepository<Models.WaterRegisterChangeForm>> _waterRegisterChangeForm;

        public DA001Service(
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


        public Task<DA001> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA001 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class WaterRegisterChangeForm_DA001_Model
        {
            public string OperatingArea { get; set; }
        }

        public async Task<DA001> Query(QueryDA001 condition)
        {

            var mainClaimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache);
            if (mainClaimsIdentity == null) throw new ArgumentNullException(nameof(mainClaimsIdentity));


            var result = new DA001
            {
                UserName = mainClaimsIdentity.Name!,
                DepartmentName = condition.DepartmentIds.Any() ?
                    (await _departmentService.GetAsync(condition.DepartmentIds[0].ToString())).Name
                    : ""
            };

            var applyDateEnd = DateTime.Today;
            var applyDateBegin = applyDateEnd.AddDays(-6);
            result.PlotlyJson.Layout.Title = result.PlotlyJson.Layout.Title.Replace("{ApplyDateBegin}", applyDateBegin.ToString("yyyy/MM/dd"))
                                            .Replace("{ApplyDateEnd}", applyDateEnd.ToString("yyyy/MM/dd"));
            applyDateEnd = applyDateEnd.AddDays(1);


            var sites = await _departmentService.GetAsync<QuerySite>(new QuerySite
            {
                DepartmentIds = condition.DepartmentIds
            });
            var anotherCodes = sites.Departments!.Select(x => x.AnotherCode).ToArray();

            var pb = PredicateBuilder.New<Models.WaterRegisterChangeForm>();
            var exp = pb.Start(x => x.ApplyDate >= applyDateBegin && x.ApplyDate < applyDateEnd && x.SerialNumber != null);
            if (condition.DepartmentIds.Any()) //有指定單位的話, 原始資料的查詢也要限制
            {
                exp = pb.And(x => anotherCodes.Contains(x.OperatingArea));
            }

            var formData = await _waterRegisterChangeForm().GetListAsync<WaterRegisterChangeForm_DA001_Model>(
                exp,
                x => new WaterRegisterChangeForm_DA001_Model { OperatingArea = x.OperatingArea });


            foreach (var site in sites.Departments!)
            {
                result.PlotlyJson.Data.First().X.Add(site.Name);
                result.PlotlyJson.Data.First().Y.Add(formData.Count(x => x.OperatingArea == site.AnotherCode).ToString());
            }
           
            return result;
        }



        public Task<DA001[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA001[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
