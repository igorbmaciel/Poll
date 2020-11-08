using System;

namespace Poll.Domain.Queries.Response
{
    public class GetTasksResponse
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
    }
}
