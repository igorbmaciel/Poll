using System;

namespace Poll.Domain.Queries.Response
{
    public class GetAllEmployeeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
