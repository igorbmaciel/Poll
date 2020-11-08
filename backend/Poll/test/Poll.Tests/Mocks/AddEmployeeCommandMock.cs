using Poll.Domain.Queries.Request;

namespace Poll.Tests.Mocks
{
    public class AddEmployeeCommandMock
    {
        public static AddEmployeeCommand GetValidDto()
        {
            return new AddEmployeeCommand()
            {
                Name = "Nome teste",
                Email = "Teste@teste.com",
                Password = "Testesenha"
            };
        }

        public static AddEmployeeCommand GetInvalidDto()
        {
            return new AddEmployeeCommand()
            {
                Name = string.Empty,
                Email = string.Empty,
                Password = string.Empty
            };
        }

        public static AddEmployeeCommand GetInvalidNameDto()
        {
            return new AddEmployeeCommand()
            {
                Name = string.Empty,
                Email = "Teste@teste.com",
                Password = "Testesenha"
            };
        }

        public static AddEmployeeCommand GetInvalidEmailDto()
        {
            return new AddEmployeeCommand()
            {
                Name = "Nome teste",
                Email = string.Empty,
                Password = "Testesenha"
            };
        }

        public static AddEmployeeCommand GetInvalidPasswordDto()
        {
            return new AddEmployeeCommand()
            {
                Name = "Nome teste",
                Email = "Teste@teste.com",
                Password = string.Empty
            };
        }
    }
}
