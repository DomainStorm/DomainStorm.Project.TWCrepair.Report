using System.Net;
using Dapr.Client;
using DomainStorm.Framework;
using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Post.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
{
    public class DepartmentService : IGetService<Department, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly IInvokeMethod _invokeMethod;

        public DepartmentService(TokenProvider tokenProvider, IInvokeMethod invokeMethod)
        {
            _tokenProvider = tokenProvider;
            _invokeMethod = invokeMethod;
        }

        public async Task<Department> GetAsync(string id)
        {
            return await _invokeMethod.InvokeMethodAsync<Department>(
                HttpMethod.Get,
                "JwtAuthApi",
                $"api/department/{id}", _tokenProvider);
        }

        

        public Task<Department[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryOrganizationTree e => QueryOrganizationTree(e),
                QuerySite e => QuerySite(e),
                QueryByLevel e => QueryByLevel(e.Level),
				GetParentByLevel e => GetParentByLevel(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<Department> QueryOrganizationTree(QueryOrganizationTree condition)
        {
            var department = await _invokeMethod.InvokeMethodAsync<QueryOrganizationTree, Department>(
                HttpMethod.Post,
                "JwtAuthApi",
                $"api/organizationTree",
                condition,
                _tokenProvider);
            return department;
        }

        public async Task<Department> QuerySite(Framework.BlazorComponent.CommandModel.Department.V1.QuerySite condition)
        {

            var sites = new List<Department>();
            if (condition.DepartmentIds == null)
                condition.DepartmentIds = new List<Guid>();
            if (!condition.DepartmentIds.Any())
                condition.DepartmentIds.Add(Guid.Empty);


            foreach (var departmentId in condition.DepartmentIds)
            {
                var departments = await _invokeMethod.InvokeMethodAsync<List<Department>>(
                    HttpMethod.Get,
                    "JwtAuthApi",
                    $"api/department/{departmentId.ToString()}/childrenWithAnotherCode", _tokenProvider);

                //站所及其以下的股, 都會有 anotherCode(且一樣), 但只需要取得到站所為止就好,先排序把層級高的列前面
                departments.OrderBy(x => x.FullName.Split(',').Length).ToList().ForEach(d =>
                {
                    if (!sites.Any(x => x.AnotherCode == d.AnotherCode)) //已存在的不加入,以先找到的(站所)為準
                    {
                        sites.Add(d);
                    }
                });
            }

            return new Department
            {
                Departments = sites
            };

        }

        public async Task<Department> QueryByLevel(int level)
        {
            //var fullDepartments = await QueryOrganizationTree(new QueryOrganizationTree());
            //var departments = fullDepartments.Departments!.Traverse(x => x.Departments!).Where(x => x.Level.HasValue && x.Level == level).ToList();
            
            var departments = await _invokeMethod.InvokeMethodAsync<List<Department>>(
                HttpMethod.Get,
                "JwtAuthApi",
                $"api/department/getByLevel/{level}", _tokenProvider);
            return new Department
            {
                Departments = departments
            };
        }


        //private void AddChildSite(Department nowDepartment , List<Department> target)
        //{
        //    if (!string.IsNullOrEmpty(nowDepartment.AnotherCode))  //本身有站所代碼的, 就不要再往下找子單位了
        //    {
        //        target.Add(nowDepartment);
        //    }
        //    else if (nowDepartment.Departments != null)
        //    {
        //        foreach (var child in nowDepartment.Departments)
        //        {
        //            AddChildSite(child, target);
        //        }
        //    }
        //}
		
		public async Task<Department> GetParentByLevel(Framework.BlazorComponent.CommandModel.Department.V1.GetParentByLevel condition)
		{
            try
            {
                var department = await _invokeMethod.InvokeMethodAsync<Department>(
                    HttpMethod.Get,
                    "JwtAuthApi",
                    $"api/department/{condition.DepartmentId}/getParentByLevel/{condition.Level}", _tokenProvider);

                return department;
            }
            catch (InvocationException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                    throw new ArgumentNullException(nameof(condition));

                throw;
            }
        }


        public Task<Department[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
