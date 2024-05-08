using DomainStorm.Framework.Services;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA002.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Post.V1;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Authentication;
using DomainStorm.Framework.Authentication.Claims;

namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA002Service : IGetService<DA002, string>
    {
        private readonly GetRepository<IRepository<Models.FixForm>> _getFixFormRepository;
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;

        public DA002Service(
            TokenProvider tokenProvider,
            ICache cache,
            GetRepository<IRepository<Models.FixForm>> getFixFormRepository)
        {
            _tokenProvider = tokenProvider;
            _cache = cache;
            _getFixFormRepository = getFixFormRepository;
        }

        public Task<DA002> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA002> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryDA002 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<DA002> Query(QueryDA002 condition)
        {
            var mainClaimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache)!;
            if (mainClaimsIdentity == null) throw new ArgumentNullException(nameof(mainClaimsIdentity));
            var departmentId = Guid.Parse(mainClaimsIdentity.FindFirst(c => c.Type == ClaimTypes.DepartmentId)!.Value);

            //取得最新7個工作天的異動
            var date = DateTime.Today;
            var workDateCount = 0;
            while (workDateCount < 7)
            {
                date = date.AddDays(-1);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workDateCount++;
                }
            }
            
            var repository = _getFixFormRepository();
            var fixForms = await repository.GetListAsync<DA002_Item>(x =>
                x.CheckFormTransfer != null && x.CheckFormTransfer.CheckForm.DepartmentId == departmentId  //自己單位的檢漏案件移轉出的修漏案件
                    && x.Form.ModifyTime >= date    //最近 N 天的異動
                    && x.Form.ModifyActionType != "新增",   //排除新增
                f => new DA002_Item
                {
                    FixCaseNo = f.FixCaseNo!,
                    CheckCaseNo = f.SourceCheckCaseNo!,
                    ModifyTime = f.Form.ModifyTime!.Value,
                    ModifyNotes = f.Form.ModifyNotes!
                });

            var result = new DA002();
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.CheckCaseNo ?? "").ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.FixCaseNo ?? "").ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.ModifyTime.ToString("yyyy/MM/dd")).ToList());
            result.PlotlyJson.Data.First().Cells.Values.Add(fixForms.Select(x => x.ModifyNotes ?? "").ToList());
            return result;
        }

        

        public Task<DA002[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DA002[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DA002[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
