using Microsoft.Extensions.Configuration;
using Poll.Application.Interfaces;
using Poll.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInfraDependency(configuration)
                .AddDomainDependency(configuration);

            services.AddTransient<IEmployeeAppService, EmployeeAppService>();       

            return services;
        }
    }
}
