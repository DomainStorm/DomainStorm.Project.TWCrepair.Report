using DomainStorm.Framework.Services;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using static DomainStorm.Project.TWCrepair.Report.Web.ReportCommandModel.DA001.V1;
using Models = DomainStorm.Project.TWCrepair.Repository.Models;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Framework;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Post.V1;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using DomainStorm.Project.TWCrepair.Report.Web.Views.Dashboards;


namespace DomainStorm.Project.TWCrepair.Report.Web.Services.Impl.Staging
{
    public class DA001Service : IGetService<DA001, string>
    {
        private readonly IGetService<Department, string> _departmentService;
        private readonly IGetService<Post, string> _postService;
        private readonly GetRepository<IRepository<Models.CheckDailyReport>> _getCheckDailyReportRepository;

        public DA001Service(
            IGetService<Department, string> departmentService,
            IGetService<Post, string> postService,
            GetRepository<IRepository<Models.CheckDailyReport>> getCheckDailyReportRepository)
        {
            _departmentService = departmentService;
            _postService = postService;
            _getCheckDailyReportRepository = getCheckDailyReportRepository;
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

        public async Task<DA001> Query(QueryDA001 condition)
        {
            var result = new DA001();
            

            //取得前7個工作天的報表資料
            var date = DateTime.Today;
            while(result.dates.Count <7)
            {
                date = date.AddDays(-1);
                if(date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    result.dates.Add(date);
                }
            }
            result.dates = result.dates.Reverse<DateTime>().ToList();

            var repository = _getCheckDailyReportRepository();
            var dailyReports = await repository.GetListAsync(x => x.ReportDate >= result.dates.First() && x.ReportDate <= result.dates.Last());

            //取得工作區的所有層級的單位,職位
            var tree = await _departmentService.GetAsync<QuerChildren>(new QuerChildren
            {
                TreeFlag = 2,
                Deep = 0
            });
            result.LoadDepartments(tree);
            foreach (var reportDepartment in result.Items)
            {
                var department = await _departmentService.GetAsync<QueryPost>(new QueryPost
                {
                    DepartmentId = reportDepartment.DepartmentId.ToString(),
                    //IncludeDisabled = true     
                });
                if (department.PostDetails != null && department.PostDetails.Any())
                {
                    reportDepartment.PostIds = department.PostDetails!.Select(x => x.PostId).ToList();
                    reportDepartment.PostsCount = reportDepartment.PostIds.Count();
                }
            }



            for(var i = -1; i< result.dates.Count; i++)
            {
                if(i == -1)
                {
                    result.PlotlyJson.Data.First().Header.Values.Add(new List<string> { "單位\\日期" });
                }
                else
                {
                    result.PlotlyJson.Data.First().Header.Values.Add(new List<string> { result.dates[i].ToString("yyyy/MM/dd") });
                }

                var cellValues = new List<string>();
                foreach (var reportDepartment in result.Items)
                {
                    
                    if(i == -1)
                    {
                        cellValues.Add(reportDepartment.Name);
                    }
                    else
                    {
                        var reportCountOfPost = dailyReports.Count(
                           x => x.ReportDepartmentId == reportDepartment.DepartmentId
                           && reportDepartment.PostIds.Contains(x.ReportTeamMemberPostId)
                           && x.ReportDate == result.dates[i]);
                        cellValues.Add((reportDepartment.PostIds.Count - reportCountOfPost).ToString());
                    }
                }
                result.PlotlyJson.Data.First().Cells.Values.Add(cellValues);
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
