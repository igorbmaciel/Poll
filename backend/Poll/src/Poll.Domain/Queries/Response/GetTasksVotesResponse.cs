﻿using System;
using System.Collections.Generic;

namespace Poll.Domain.Queries.Response
{
    public class GetTasksVotesResponse
    {
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public List<GetEmployeeVotesResponse> EmployeeVotes { get; set; }
    }
}
