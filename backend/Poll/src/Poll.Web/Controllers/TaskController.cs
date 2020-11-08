using Microsoft.AspNetCore.Mvc;
using Poll.Application.Interfaces;
using Poll.Domain.Entities;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;

namespace Poll.Web.Controllers
{
    [Route(RouteConsts.Task)]
    public class TaskController : TnfController
    {
        private readonly ITaskAppService _taskAppService;

        public TaskController(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTaskResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateAsync(AddTaskCommand command)
        {
            var response = await _taskAppService.AddTask(command);

            return CreateResponseOnPost(response, RouteResponseConsts.Task);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetAllTasksResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            var response = await _taskAppService.GetAllTasks();
            return CreateResponseOnGet(response, RouteResponseConsts.Task);
        }

    }
}
