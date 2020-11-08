using Microsoft.Extensions.DependencyInjection;
using Poll.Domain.Entities;
using System;
using Tnf.AspNetCore.TestBase;
using Tnf.Notifications;
using Xunit;


namespace Poll.Tests.UnitTests.Domain.Entities
{
    public class VoteTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly INotificationHandler _notificationHandler;

        public VoteTest()
        {
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Add_Vote_With_Valid_Dto_Shoud_Not_Raise_Error()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var tasksId = Guid.NewGuid();
            var comment = "new comment";


            var vote = new Vote();

            //Act
            var result = vote.AddVote(employeeId, tasksId, comment, _notificationHandler);


            //Assert
            Assert.False(_notificationHandler.HasNotification());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidEmployeeId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidTaskId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidComment.ToString());
        }


        [Fact]
        public void Add_Vote_With_Invalid_EmployeeId_Shoud_Raise_Error()
        {
            //Arrange
            var employeeId = new Guid();
            var tasksId = Guid.NewGuid();
            var comment = "new comment";


            var vote = new Vote();

            //Act
            var result = vote.AddVote(employeeId, tasksId, comment, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidEmployeeId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidTaskId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidComment.ToString());
        }

        [Fact]
        public void Add_Vote_With_Invalid_TasksId_Shoud_Raise_Error()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var tasksId = new Guid();
            var comment = "new comment";


            var vote = new Vote();

            //Act
            var result = vote.AddVote(employeeId, tasksId, comment, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidTaskId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidEmployeeId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidComment.ToString());
        }

        [Fact]
        public void Add_Vote_With_Invalid_Comment_Shoud_Raise_Error()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var tasksId = Guid.NewGuid();
            var comment = string.Empty;


            var vote = new Vote();

            //Act
            var result = vote.AddVote(employeeId, tasksId, comment, _notificationHandler);


            //Assert
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidComment.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidEmployeeId.ToString());
            Assert.DoesNotContain(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.InvalidTaskId.ToString());
        }
    }
}
