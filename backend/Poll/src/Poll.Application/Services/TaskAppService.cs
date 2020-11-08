using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using Poll.Application.Interfaces;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Domain.Interfaces.ReadRepository;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;
using System.Linq;

namespace Poll.Application.Services
{
    public class TaskAppService : ApplicationServiceBase, ITaskAppService
    {
        private readonly IMediator _mediator;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskReadRepository _taskReadRepository;

        public TaskAppService(
           INotificationHandler notification,
           ITaskRepository taskRepository,
           ITaskReadRepository taskReadRepository,
           IMediator mediator)
           : base(notification)
        {
            _mediator = mediator;
            _taskRepository = taskRepository;
            _taskReadRepository = taskReadRepository;
        }

        public async Task<AddTaskResponse> AddTask(AddTaskCommand command)
        {
            var response = await _mediator.Send(command);

            if (Notification.HasNotification())
                return null;

            return response;
        }

        public async Task<List<GetAllTasksResponse>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return TasksResponse(tasks);
        }

        public async Task<List<GetTasksVotesResponse>> GetTasksEmployeeVotes()
        {
            var tasksVotes = await _taskReadRepository.GetTasksVotes();
            var tasksVotesList = new List<GetTasksVotesResponse>();
            var employeeVotesList = new List<GetEmployeeVotesResponse>();

            GetTasksVotes(tasksVotes, tasksVotesList);
            GetEmployeeVotes(tasksVotes, employeeVotesList);

            return ReturnTasksVotes(tasksVotesList, employeeVotesList);
        }

        private List<GetTasksVotesResponse> ReturnTasksVotes(List<GetTasksVotesResponse> tasksVotesList, List<GetEmployeeVotesResponse> employeeVotesList)
        {
            tasksVotesList.ForEach(t => t.EmployeeVotes.AddRange(employeeVotesList.Where(x => x.TasksId == t.TaskId)));

            return tasksVotesList;
        }

        private void GetEmployeeVotes(List<GetTasksResponse> tasksVotes, List<GetEmployeeVotesResponse> employeeVotesList)
        {
            tasksVotes.ForEach(t => employeeVotesList.Add(new GetEmployeeVotesResponse()
            {
                TasksId = t.TaskId,
                EmployeeName = t.EmployeeName,
                Date = t.Date
            }));
        }

        private void GetTasksVotes(List<GetTasksResponse> tasksVotes, List<GetTasksVotesResponse> tasksVotesList)
        {
            tasksVotes.Select(x => new { x.TaskId, x.TaskName }).Distinct().ToList().ForEach(t =>
                tasksVotesList.Add(new GetTasksVotesResponse()
                {
                    TaskId = t.TaskId,
                    TaskName = t.TaskName,
                    EmployeeVotes = new List<GetEmployeeVotesResponse>()
                })
            );
        }

        private List<GetAllTasksResponse> TasksResponse(List<Tasks> tasks)
        {
            var tasksResponseList = new List<GetAllTasksResponse>();
            tasks.ForEach(t => tasksResponseList.Add(new GetAllTasksResponse()
            {
                Id = t.Id,
                Name = t.Name
            }));

            return tasksResponseList;
        }
    }
}
