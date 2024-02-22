using DomainStorm.Framework.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DepartmentCommandModel = DomainStorm.Project.TWC.Web.CommandModel.Department.V1;

namespace DomainStorm.Project.TWC.Report.Web.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme)]
    [Route("api/department")]
    public class DepartmentQueryApi : ControllerBase
    {
        private readonly IGetService<ViewModel.Department, string> _departmentService;

        public DepartmentQueryApi(
            IGetService<ViewModel.Department, string> departmentService)
        {
            _departmentService = departmentService;
        }

        

        [HttpPost("queryByLevel")]
        public async Task<ActionResult<ViewModel.Department>> queryByLevel([FromBody] DepartmentCommandModel.QueryByLevel query)
        {
            return await _departmentService.GetAsync<DepartmentCommandModel.QueryByLevel>(query);
        }
		
		[HttpPost("getParentByLevel")]
		public async Task<ActionResult<ViewModel.Department>> GetParentByLevel([FromBody] DepartmentCommandModel.GetParentByLevel query)
		{
			return await _departmentService.GetAsync<DepartmentCommandModel.GetParentByLevel>(query);
		}

    }
}
