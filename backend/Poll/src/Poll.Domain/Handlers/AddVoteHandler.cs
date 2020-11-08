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
    public class AddVoteHandler : BaseRequestHandler, IRequestHandler<AddVoteCommand, AddVoteResponse>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IVoteRepository _voteRepository;

        public AddVoteHandler(
          INotificationHandler notification,
          IUnitOfWorkManager unitOfWorkManager,
          IVoteRepository voteRepository
          ) : base(notification)
        {
            _voteRepository = voteRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<AddVoteResponse> Handle(AddVoteCommand command, CancellationToken cancellationToken)
        {
            if (!IsValid(command))
                return null;

            var vote = new Vote();

            vote.AddVote(command.EmployeeId, command.TaskId, command.Comment);

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _voteRepository.AddVote(vote);
                uow.Complete();
            }

            return VoteResponse(vote);
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
