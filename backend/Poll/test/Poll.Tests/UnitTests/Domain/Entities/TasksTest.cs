using Microsoft.Extensions.DependencyInjection;
using Poll.Domain.Entities;
using Tnf.AspNetCore.TestBase;
using Tnf.Notifications;
using Xunit;

namespace Poll.Tests.UnitTests.Domain.Entities
{
    public class TasksTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly INotificationHandler _notificationHandler;

        public TasksTest()
        {
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Add_Tasks_With_Valid_Dto_Shoud_Not_Raise_Error()
        {
            //Arrange
            var name = "nome";

            var tasks = new Tasks();

            //Act
            var result = tasks.AddTask(name, _notificationHandler);


            //Assert
            Assert.False(_notificationHandler.HasNotification());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Tasks.EntityError.InvalidTaskName.ToString());          
        }


        [Fact]
        public void Add_Tasks_With_Invalid_Name_Shoud_Raise_Error()
        {
            //Arrange
            var name = string.Empty;

            var tasks = new Tasks();

            //Act
            var result = tasks.AddTask(name, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Tasks.EntityError.InvalidTaskName.ToString());    
        }
    }
}
