using Poll.Domain.Queries.Request;
using System;

namespace Poll.Tests.Mocks
{
    public class AddVoteCommandMock
    {
        public static AddVoteCommand GetValidDto()
        {
            return new AddVoteCommand()
            {
                EmployeeId = Guid.NewGuid(),
                TaskId  = Guid.NewGuid(),
                Comment = "new comment"
            };
        }

        public static AddVoteCommand GetInvalidDto()
        {
            return new AddVoteCommand()
            {
                EmployeeId = new Guid(),
                TaskId = new Guid(),
                Comment = string.Empty
            };
        }

        public static AddVoteCommand GetInvalidEmployeeIdDto()
        {
            return new AddVoteCommand()
            {
                EmployeeId = new Guid(),
                TaskId = Guid.NewGuid(),
                Comment = "new comment"
            };
        }

        public static AddVoteCommand GetInvalidTaskIdDto()
        {
            return new AddVoteCommand()
            {
                EmployeeId = Guid.NewGuid(),
                TaskId = new Guid(),
                Comment = "new comment"
            };
        }

        public static AddVoteCommand GetInvalidCommentDto()
        {
            return new AddVoteCommand()
            {
                EmployeeId = Guid.NewGuid(),
                TaskId = Guid.NewGuid(),
                Comment = string.Empty
            };
        }
    }
}
