using MediatR;
using Poll.Application.Interfaces;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace Poll.Application.Services
{
    public class TaskAppService : ApplicationServiceBase, ITaskAppService
    {
        private readonly IMediator _mediator;
        private readonly ITaskRepository _taskRepository;

        public TaskAppService(
           INotificationHandler notification,
           ITaskRepository taskRepository,
           IMediator mediator)
           : base(notification)
        {
            _mediator = mediator;
            _taskRepository = taskRepository;

        }

        public async Task<AddTaskResponse> AddTask(AddTaskCommand command)
        {
            var response = await _mediator.Send(command);

            if (Notification.HasNotification())
                return null;

            return response;
        }

        public Task<List<Tasks>> GetAllTasks()
            => _taskRepository.GetAllTasks();
    }
}
