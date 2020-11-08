using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Poll.Domain.Handlers;
using Poll.Domain.Interfaces;
using Poll.Tests.Mocks;
using Shouldly;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using Xunit;
using static Poll.Domain.Queries.Request.AddTaskValidator;

namespace Poll.Tests.UnitTests.Domain.Handlers
{
    public class AddTaskHandlerTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITaskRepository _tasksRepository;
        private readonly INotificationHandler _notificationHandler;

        public AddTaskHandlerTest()
        {
            _unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            _tasksRepository = Substitute.For<ITaskRepository>();
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Shoud_Resolve_All()
        {
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
            ServiceProvider.GetService<ITaskRepository>().ShouldNotBeNull();
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
        }

        private AddTaskHandler GetTasksHandler()
        {
            return new AddTaskHandler(
                _notificationHandler,
                _unitOfWorkManager,
                _tasksRepository
                );
        }

        [Fact]
        public async Task Should_Raise_Notification_When_Command_Is_Invalid()
        {
            //parameters
            var command = AddTaskCommandMock.GetInvalidDto();

            //call
            var handler = GetTasksHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskName);
        }

        [Fact]
        public async Task Should_AddTasks()
        {
            //parameters
            var command = AddTaskCommandMock.GetValidDto();         

            //call
            var handler = GetTasksHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.False(_notificationHandler.HasNotification());
            Assert.Equal(command.Name, result.Name);
        }      
    }
}
