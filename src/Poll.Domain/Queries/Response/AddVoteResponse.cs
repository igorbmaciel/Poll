using System;

namespace Poll.Domain.Queries.Response
{
    public class AddVoteResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid TaskId { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
