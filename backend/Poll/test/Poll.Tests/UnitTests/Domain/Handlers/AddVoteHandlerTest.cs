using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Poll.Domain.Entities;
using Poll.Domain.Handlers;
using Poll.Domain.Interfaces;
using Poll.Tests.Mocks;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Notifications;
using Tnf.Repositories.Uow;
using Xunit;
using static Poll.Domain.Queries.Request.AddVoteValidator;

namespace Poll.Tests.UnitTests.Domain.Handlers
{
    public class AddVoteHandlerTest : TnfAspNetCoreIntegratedTestBase<StartupUnitPropertyTest>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITaskRepository _tasksRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly INotificationHandler _notificationHandler;

        public AddVoteHandlerTest()
        {
            _unitOfWorkManager = Substitute.For<IUnitOfWorkManager>();
            _tasksRepository = Substitute.For<ITaskRepository>();
            _employeeRepository = Substitute.For<IEmployeeRepository>();
            _voteRepository = Substitute.For<IVoteRepository>();
            _notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
        }

        [Fact]
        public void Shoud_Resolve_All()
        {
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
            ServiceProvider.GetService<ITaskRepository>().ShouldNotBeNull();
            ServiceProvider.GetService<INotificationHandler>().ShouldNotBeNull();
        }

        private AddVoteHandler GetVoteHandler()
        {
            return new AddVoteHandler(
                _notificationHandler,
                _unitOfWorkManager,
                _voteRepository,
                _employeeRepository,
                _tasksRepository
                );
        }

        [Fact]
        public async Task Should_Raise_Notification_When_Command_Is_Invalid()
        {
            //parameters
            var command = AddVoteCommandMock.GetInvalidDto();

            //call
            var handler = GetVoteHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }

        [Fact]
        public async Task Should__Raise_Notification_When_Employee_Not_Exists()
        {
            //parameters
            var command = AddVoteCommandMock.GetValidDto();

            Employee employee = null;

            _employeeRepository.GetEmployeeById(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return employee;
            });

            //call
            var handler = GetVoteHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Employee.EntityError.InvalidEmployee.ToString());
        }

        [Fact]
        public async Task Should__Raise_Notification_When_Tasks_Not_Exists()
        {
            //parameters
            var command = AddVoteCommandMock.GetValidDto();

            var employee = new Employee();
            var id = new Guid("e76b1346-c33d-4032-baf3-892a8e45c7ae");            
;           employee.GetType().GetProperty("Id").SetValue(employee, id, null);        

            Tasks tasks = null;

            _employeeRepository.GetEmployeeById(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return employee;
            });

            _tasksRepository.GetTasksById(command.TaskId).ReturnsForAnyArgs(x =>
            {
                return tasks;
            });

            //call
            var handler = GetVoteHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Tasks.EntityError.InvalidTask.ToString());
        }

        [Fact]
        public async Task Should__Raise_Notification_When_Already_Voted()
        {
            //parameters
            var command = AddVoteCommandMock.GetValidDto();

            var employee = new Employee();
            var employeeId = new Guid("e76b1346-c33d-4032-baf3-892a8e45c7ae");
            employee.GetType().GetProperty("Id").SetValue(employee, employeeId, null);

            var tasks = new Tasks();
            var tasksId = new Guid("24477800-16ec-4d92-8363-b3c8848ee9ba");
            tasks.GetType().GetProperty("Id").SetValue(tasks, tasksId, null);

            _employeeRepository.GetEmployeeById(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return employee;
            });

            _tasksRepository.GetTasksById(command.TaskId).ReturnsForAnyArgs(x =>
            {
                return tasks;
            });

            _voteRepository.ValidateVoteByEmployeeId(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return true;
            });

            //call
            var handler = GetVoteHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.Null(result);
            Assert.True(_notificationHandler.HasNotification());
            Assert.Contains(_notificationHandler.GetAll(), e => e.DetailedMessage == Vote.EntityError.EmployeeAlreadyVoted.ToString());
        }

        [Fact]
        public async Task Should_AddVote()
        {
            //parameters
            var command = AddVoteCommandMock.GetValidDto();

            var employee = new Employee();
            var employeeId = new Guid("e76b1346-c33d-4032-baf3-892a8e45c7ae");
            employee.GetType().GetProperty("Id").SetValue(employee, employeeId, null);

            var tasks = new Tasks();
            var tasksId = new Guid("24477800-16ec-4d92-8363-b3c8848ee9ba");
            tasks.GetType().GetProperty("Id").SetValue(tasks, tasksId, null);

            _employeeRepository.GetEmployeeById(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return employee;
            });

            _tasksRepository.GetTasksById(command.TaskId).ReturnsForAnyArgs(x =>
            {
                return tasks;
            });

            _voteRepository.ValidateVoteByEmployeeId(command.EmployeeId).ReturnsForAnyArgs(x =>
            {
                return false;
            });

            //call
            var handler = GetVoteHandler();

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(result);
            Assert.False(_notificationHandler.HasNotification());
            Assert.Equal(command.EmployeeId, result.EmployeeId);
            Assert.Equal(command.TaskId, result.TaskId);
            Assert.Equal(command.Comment, result.Comment);
        }
    }
}
