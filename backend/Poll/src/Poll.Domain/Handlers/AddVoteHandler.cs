using MediatR;
using Poll.Domain.AppConst;
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
    public class AddVoteHandler : BaseRequestHandler, IRequestHandler<AddVoteCommand, AddVoteResponse>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IVoteRepository _voteRepository;

        public AddVoteHandler(
          INotificationHandler notification,
          IUnitOfWorkManager unitOfWorkManager,
          IVoteRepository voteRepository,
          IEmployeeRepository employeeRepository,
          ITaskRepository taskRepository
          ) : base(notification)
        {
            _voteRepository = voteRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
        }

        public async Task<AddVoteResponse> Handle(AddVoteCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command))
                return null;

            var employee = await _employeeRepository.GetEmployeeById(command.EmployeeId);

            if (employee == null)
                InvalidEmployee(_notification);

            if (_notification.HasNotification()) return null;

            var tasks = await _taskRepository.GetTasksById(command.TaskId);

            if (tasks == null)
                InvalidTask(_notification);

            if (_notification.HasNotification()) return null;

            var vote = new Vote();

            vote.AddVote(command.EmployeeId, command.TaskId, command.Comment, _notification);

            if (_notification.HasNotification())
                return null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _voteRepository.AddVote(vote);
                uow.Complete();
            }

            return VoteResponse(vote);
        }

        private void InvalidTask(INotificationHandler notification)
        {
            notification.RaiseError(AppConsts.LocalizationSourceName, Tasks.EntityError.InvalidTask);
        }

        private void InvalidEmployee(INotificationHandler notification)
        {
            notification.RaiseError(AppConsts.LocalizationSourceName, Employee.EntityError.InvalidEmployee);
        }

        private AddVoteResponse VoteResponse(Vote vote)
        {
            return new AddVoteResponse()
            {
                Id = vote.Id,
                EmployeeId = vote.EmployeeId,
                TaskId = vote.TaskId,
                Comment = vote.Comment
            };
        }
    }
}
