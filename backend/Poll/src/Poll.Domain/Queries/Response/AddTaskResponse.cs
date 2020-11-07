using System;

namespace Poll.Domain.Queries.Response
{
    public class AddTaskResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
