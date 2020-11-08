using Microsoft.Extensions.DependencyInjection;
using Poll.Domain.Entities;
using Tnf.AspNetCore.TestBase;
using Tnf.Notifications;
using Xunit;

namespace Poll.Tests.UnitTests.Domain.Entities
{
    public class EmployeeTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly INotificationHandler _notificationHandler;

        public EmployeeTest()
        {
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Add_Employee_With_Valid_Dto_Shoud_Not_Raise_Error()
        {
            //Arrange
            var name = "nome";
            var email = "test@teste.com";
            var password = "password";


            var employee = new Employee();

            //Act
            var result = employee.AddEmployee(name, email, password, _notificationHandler);


            //Assert
            Assert.False(_notificationHandler.HasNotification());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeName.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeEmail.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeePassword.ToString());
        }


        [Fact]
        public void Add_Employee_With_Invalid_Name_Shoud_Raise_Error()
        {
            //Arrange
            var name = string.Empty;
            var email = "test@teste.com";
            var password = "password";


            var employee = new Employee();

            //Act
            var result = employee.AddEmployee(name, email, password, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeName.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeEmail.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeePassword.ToString());
        }

        [Fact]
        public void Add_Employee_With_Invalid_Email_Shoud_Raise_Error()
        {
            //Arrange
            var name = "nome";
            var email = string.Empty;
            var password = "password";


            var employee = new Employee();

            //Act
            var result = employee.AddEmployee(name, email, password, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeEmail.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeName.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeePassword.ToString());
        }

        [Fact]
        public void Add_Employee_With_Invalid_Password_Shoud_Raise_Error()
        {
            //Arrange
            var name = "nome";
            var email = "test@teste.com";
            var password = string.Empty;


            var employee = new Employee();

            //Act
            var result = employee.AddEmployee(name, email, password, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeePassword.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeEmail.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployeeName.ToString());
        }
    }
}
