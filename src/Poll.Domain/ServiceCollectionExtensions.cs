using MediatR;
using Microsoft.Extensions.Configuration;
using Poll.Domain.Queries.Request;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMediatR();

            return services;
        }

        private static void ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AddEmployeeCommand).GetTypeInfo().Assembly);
        }
    }
}