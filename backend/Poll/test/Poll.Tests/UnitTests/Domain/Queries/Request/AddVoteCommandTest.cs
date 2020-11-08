using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poll.Domain;
using Poll.Tests.Mocks;
using Tnf.Notifications;
using Tnf.TestBase;
using Xunit;
using static Poll.Domain.Queries.Request.AddVoteValidator;

namespace Poll.Tests.UnitTests.Domain.Queries.Request
{
    public class AddVoteCommandTest : TnfIntegratedTestBase
    {
        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            services.AddDomainDependency(builder.Build());
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf(config =>
            {
                config.UseDomainLocalization();
            });
        }

        [Fact]
        public void Should_Not_Have_Validation_Error_When_Valid_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddVoteCommandMock.GetValidDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddVoteCommandMock.GetInvalidDto();

            //call
            command.IsValid();

            //assert
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_EmployeeId_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddVoteCommandMock.GetInvalidEmployeeIdDto();

            //call
            command.IsValid();

            //assert
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_TaskId_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddVoteCommandMock.GetInvalidTaskIdDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Comment_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddVoteCommandMock.GetInvalidCommentDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeId);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidTaskId);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidComment);
        }
    }
}
