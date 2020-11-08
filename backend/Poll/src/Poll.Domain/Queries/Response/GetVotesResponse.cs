using System;

namespace Poll.Domain.Queries.Response
{
    public class GetVotesResponse
    {
        public string TaskName { get; set; }
        public int QuantityVotes { get; set; }
    }
}
