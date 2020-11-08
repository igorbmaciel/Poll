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
using static Poll.Domain.Queries.Request.AddEmployeeValidator;

namespace Poll.Tests.UnitTests.Domain.Handlers
{
    public class AddEmployeeHandlerTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly INotificationHandler _notificationHandler;

        public AddEmployeeHandlerTest()
        {
            _unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            _employeeRepository = Substitute.For<IEmployeeRepository>();
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Shoud_Resolve_All()
        {
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
            ServiceProvider.GetService<IEmployeeRepository>().ShouldNotBeNull();
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
        }

        private AddEmployeeHandler GetEmployeeHandler()
        {
            return new AddEmployeeHandler(
                _notificationHandler,
                _unitOfWorkManager,
                _employeeRepository
                );
        }

        [Fact]
        public async Task Should_Raise_Notification_When_Command_Is_Invalid()
        {
            //parameters
            var command = AddEmployeeCommandMock.GetInvalidDto();

            //call
            var handler = GetEmployeeHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);     
        }

        [Fact]
        public async Task Should_AddEmployee()
        {
            //parameters
            var command = AddEmployeeCommandMock.GetValidDto();         

            //call
            var handler = GetEmployeeHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.False(_notificationHandler.HasNotification());
            Assert.Equal(command.Name, result.Name);
            Assert.Equal(command.Email, result.Email);
        }      
    }
}
