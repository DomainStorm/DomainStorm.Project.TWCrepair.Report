using DomainStorm.Framework.BlazorComponent.ViewModel;
using DomainStorm.Framework.Services;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Department.V1;
using static DomainStorm.Framework.BlazorComponent.CommandModel.Post.V1;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Mock
{
    public class DepartmentService : IGetService<Department, string>
    {
        public Task<Department> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        

        public Task<Department[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Department[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
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
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public async Task<Department> QueryByLevel(int level)
        {
            //var fullDepartments = await QueryOrganizationTree(new QueryOrganizationTree());
            //var departments = fullDepartments.Departments!.Traverse(x => x.Departments!).Where(x => x.Level.HasValue && x.Level == level).ToList();

            var departments = new Department[]
                {
                    new Department
                    {
                        DepartmentId = Guid.NewGuid(),
                        FullName = "第一區管理處",
                        Code = "Code001",
                        Name = "第一區管理處",
                        Departments = new Department[]
                        {

                        }
                    }
                };

            return new Department
            {
                Departments = departments
            };
        }

        public async Task<Department> QueryOrganizationTree(QueryOrganizationTree condition)
        {
            //目前主要在測"站所" 和 "股" 的 AnotherCode 一樣, 但報表只需取得到 "站所"名稱
            var department = new Department
            {
                Departments = new Department[]
                {
                    new Department
                    {
                        DepartmentId = Guid.NewGuid(),
                        FullName = "第一區管理處",
                        Code = "Code001",
                        Departments = new Department[]
                        {
                            new Department
                            {
                                DepartmentId = Guid.NewGuid(),
                                FullName = "基隆服務所",
                                Code = "Code001001",
                                AnotherCode = "11",
                                Departments = new Department[]
                                {
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "一股",
                                         Code = "Code001001001",
                                         AnotherCode = "11"
                                    },
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "二股",
                                         Code = "Code001001002",
                                         AnotherCode = "11"
                                    }
                                }
                            },
                            new Department
                            {
                                DepartmentId = Guid.NewGuid(),
                                FullName = "淡水營運所",
                                Code = "Code001002",
                                AnotherCode = "1B",
                                Departments = new Department[]
                                {
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "一股",
                                         Code = "Code001002001",
                                         AnotherCode = "1B"
                                    },
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "二股",
                                         Code = "Code001002002",
                                         AnotherCode = "1B"
                                    }
                                }
                            }
                        }
                    },
                    new Department
                    {
                        DepartmentId = Guid.NewGuid(),
                        FullName = "第二區管理處",
                        Code = "Code002",
                        Departments = new Department[]
                        {
                            new Department
                            {
                                DepartmentId = Guid.NewGuid(),
                                FullName = "桃園服務所",
                                Code = "Code002001",
                                AnotherCode = "22",
                                Departments = new Department[]
                                {
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "一股",
                                         Code = "Code002001001",
                                         AnotherCode = "22"
                                    },
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "二股",
                                         Code = "Code002001002",
                                         AnotherCode = "22"
                                    }
                                }
                            },
                            new Department
                            {
                                DepartmentId = Guid.NewGuid(),
                                FullName = "中壢服務所",
                                Code = "Code002002",
                                AnotherCode = "23",
                                Departments = new Department[]
                                {
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "一股",
                                         Code = "Code002002001",
                                         AnotherCode = "23"
                                    },
                                    new Department
                                    {
                                         DepartmentId = Guid.NewGuid(),
                                         FullName = "二股",
                                         Code = "Code002002002",
                                         AnotherCode = "23"
                                    }
                                }
                            }
                        }
                    },
                }
            };
            return department;
        }

        public async Task<Department> QuerySite(QuerySite condition)
        {
            var fullDepartments = await QueryOrganizationTree(new QueryOrganizationTree());
            var sites = new List<Department>();
            AddChildSite(fullDepartments, sites);
            return new Department
            {
                Departments = sites.OrderBy(x => x.AnotherCode).ToList()
            };

        }
        private void AddChildSite(Department nowDepartment, List<Department> target)
        {
            if (!string.IsNullOrEmpty(nowDepartment.AnotherCode))
            {
                target.Add(nowDepartment);
            }
            else if (nowDepartment.Departments != null)
            {
                foreach (var child in nowDepartment.Departments)
                {
                    AddChildSite(child, target);
                }
            }
        }
    }
}
