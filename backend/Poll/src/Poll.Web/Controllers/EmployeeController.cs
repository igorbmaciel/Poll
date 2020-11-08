using Microsoft.AspNetCore.Mvc;
using Poll.Application.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;

namespace Poll.Web.Controllers
{
    [Route(RouteConsts.Employee)]
    public class EmployeeController : TnfController
    {
        private readonly IEmployeeAppService _employeeAppService;

        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddEmployeeResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateAsync([FromBody] AddEmployeeCommand command)
        {
            var response = await _employeeAppService.AddEmployee(command);

            return CreateResponseOnPost(response, RouteResponseConsts.Employee);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllEmployeeResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var response = await _employeeAppService.GetAllEmployees();
            return CreateResponseOnGet(response, RouteResponseConsts.Task);
        }
    }
}
