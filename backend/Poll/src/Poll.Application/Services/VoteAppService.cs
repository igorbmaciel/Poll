using MediatR;
using Poll.Application.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace Poll.Application.Services
{
    public class VoteAppService : ApplicationServiceBase, IVoteAppService
    {
        private readonly IMediator _mediator;

        public VoteAppService(
           INotificationHandler notification,
           IMediator mediator)
           : base(notification)
        {
            _mediator = mediator;

        }

        public async Task<AddVoteResponse> AddVote(AddVoteCommand command)
        {
            var response = await _mediator.Send(command);

            if (Notification.HasNotification())
                return null;

            return response;
        }
    }
}
