using DomainStorm.Framework;
using DomainStorm.Framework.Services;
using DomainStorm.Framework.SqlDb;
using DomainStorm.Project.TWC.Report.Web.ViewModel;
using static DomainStorm.Project.TWC.Report.Web.ReportCommandModel.RA999.V1;
using Models = DomainStorm.Project.TWC.Web.Models;
using DomainStorm.Project.TWC.Report.Web.Views;
using static DomainStorm.Project.TWC.Web.CommandModel.Department.V1;
using LinqKit;
using DomainStorm.Framework.Caching;
using DomainStorm.Framework.Authentication;
using AutoMapper;

namespace DomainStorm.Project.TWC.Report.Web.Services.Impl.Staging
{
    public class RA999Service : IGetService<RA999, string>
    {
        private readonly TokenProvider _tokenProvider;
        private readonly ICache _cache;
        private readonly IGetService<Department, string> _departmentService;
        private readonly IGetService<AutoLoginToken, string> _autoLoginTokenService;
        private readonly GetRepository<IRepository<Models.WaterRegisterChangeForm>> _waterRegisterChangeForm;
        private readonly IMapper _mapper;

        public RA999Service(
            TokenProvider tokenProvider,
            ICache cache,
            IGetService<Department, string> departmentService, 
            IGetService<AutoLoginToken, string> autoLoginTokenService,
            GetRepository<IRepository<Models.WaterRegisterChangeForm>> waterRegisterChangeForm,
            IMapper mapper
        )
        {
            _tokenProvider = tokenProvider;
            _cache = cache; 
            _departmentService = departmentService;
            _autoLoginTokenService = autoLoginTokenService;
            _waterRegisterChangeForm = waterRegisterChangeForm;
            _mapper = mapper;

        }
        public Task<RA999> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA999> GetAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            return condition switch
            {
                QueryRA999 e => Query(e),
                _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
            };
        }

        public class WaterRegisterChangeForm_RA01_Model
        {
            public string OperatingArea { get; set; }
        }
        public async Task<RA999> Query(QueryRA999 condition)
        {
            var mainClaimsIdentity = await _tokenProvider.GetMainClaimsIdentityAsync(_cache)!;
            if (mainClaimsIdentity == null) throw new ArgumentNullException(nameof(mainClaimsIdentity));

            var autologinToken = await _autoLoginTokenService.GetAsync(string.Empty);

            var report = new RA999
            {
                ApplyDateBegin = condition.ApplyDateBegin,
                ApplyDateEnd = condition.ApplyDateEnd,
                UserName = mainClaimsIdentity!.Name,
                Token = autologinToken.Token,
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

            var forms = await _waterRegisterChangeForm().GetListAsync(exp);

            report.Items = _mapper.Map<ICollection<Models.WaterRegisterChangeForm>, RA999_Item[]>(forms);
            return report;
        }


        public Task<RA999[]> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RA999[]> GetListAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RA999[]> GetListAsync<TQuery>(IQuery condition) where TQuery : IQuery
        {
            throw new NotImplementedException();
        }
    }
}
