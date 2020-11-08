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
                EmployeeId = new Guid("e76b1346-c33d-4032-baf3-892a8e45c7ae"),
                TaskId  = new Guid("24477800-16ec-4d92-8363-b3c8848ee9ba"),
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
