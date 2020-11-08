using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poll.Domain;
using Poll.Tests.Mocks;
using Tnf.Notifications;
using Tnf.TestBase;
using Xunit;
using static Poll.Domain.Queries.Request.AddEmployeeValidator;

namespace Poll.Tests.UnitTests.Domain.Queries.Request
{
    public class AddEmployeeCommandTest : TnfIntegratedTestBase
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
            var command = AddEmployeeCommandMock.GetValidDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);        
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddEmployeeCommandMock.GetInvalidDto();

            //call
            command.IsValid();

            //assert
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Name_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddEmployeeCommandMock.GetInvalidNameDto();

            //call
            command.IsValid();

            //assert
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Email_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddEmployeeCommandMock.GetInvalidEmailDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);
        }

        [Fact]
        public void Should_Have_Validation_Error_When_Invalid_Password_Command()
        {
            var notificationHandler = ServiceProvider.GetRequiredService<INotificationHandler>();
            var command = AddEmployeeCommandMock.GetInvalidPasswordDto();

            //call
            command.IsValid();

            //assert
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeName);
            Assert.DoesNotContain(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeeEmail);
            Assert.Contains(command.ValidationResult.Errors, e => e.CustomState is EntityError.InvalidEmployeePassword);
        }
    }
}
