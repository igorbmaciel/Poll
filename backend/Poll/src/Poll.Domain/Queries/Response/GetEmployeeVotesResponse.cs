using Newtonsoft.Json;
using System;

namespace Poll.Domain.Queries.Response
{
    public class GetEmployeeVotesResponse
    {
        [JsonIgnore]
        public Guid TasksId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
    }
}
