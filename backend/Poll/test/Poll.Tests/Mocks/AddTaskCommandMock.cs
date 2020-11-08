using Poll.Domain.Queries.Request;

namespace Poll.Tests.Mocks
{
    public class AddTaskCommandMock
    {
        public static AddTaskCommand GetValidDto()
        {
            return new AddTaskCommand()
            {
                Name = "Nome teste"
            };
        }

        public static AddTaskCommand GetInvalidDto()
        {
            return new AddTaskCommand()
            {
                Name = string.Empty
            };
        }
    }
}
