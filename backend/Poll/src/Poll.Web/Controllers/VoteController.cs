using Microsoft.AspNetCore.Mvc;
using Poll.Application.Interfaces;
using Poll.Domain.Queries.Request;
using Poll.Domain.Queries.Response;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;

namespace Poll.Web.Controllers
{
    [Route(RouteConsts.Vote)]
    public class VoteController : TnfController
    {
        private readonly IVoteAppService _voteAppService;

        public VoteController(IVoteAppService voteAppService)
        {
            _voteAppService = voteAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddVoteResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateAsync([FromBody] AddVoteCommand command)
        {
            var response = await _voteAppService.AddVote(command);

            return CreateResponseOnPost(response, RouteResponseConsts.Vote);
        }
    }
}
