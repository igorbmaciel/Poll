using MediatR;
using Poll.Domain.Base;
using Poll.Domain.Entities;
using Poll.Domain.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Threading;
using System.Threading.Tasks;
using Tnf.Notifications;
using Tnf.Repositories.Uow;

namespace Poll.Domain.Handlers
{
    public class AddTaskHandler : BaseRequestHandler, IRequestHandler<AddTaskCommand, AddTaskResponse>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITaskRepository _taskRepository;

        public AddTaskHandler(
          INotificationHandler notification,
          IUnitOfWorkManager unitOfWorkManager,
          ITaskRepository taskRepository
          ) : base(notification)
        {
            _taskRepository = taskRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<AddTaskResponse> Handle(AddTaskCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command))
                return null;

            var tasks = new Tasks();

            tasks.AddTask(command.Name, _notification);

            if (_notification.HasNotification())
                return null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _taskRepository.AddTask(tasks);
                uow.Complete();
            }

            return TaskResponse(tasks);
        }

        private AddTaskResponse TaskResponse(Tasks tasks)
        {
            return new AddTaskResponse()
            {
                Id = tasks.Id,
                Name = tasks.Name
            };
        }
    }
}
