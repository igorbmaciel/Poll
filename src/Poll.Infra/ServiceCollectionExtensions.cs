using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poll.Domain.Interfaces;
using Poll.Infra.Context;
using Poll.Infra.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTnfEntityFrameworkCore()
                .AddTnfDbContext<PollContext>((config) =>
                {
                    if (config.ExistingConnection != null)
                        config.DbContextOptions.UseNpgsql(config.ExistingConnection);
                    else
                        config.DbContextOptions.UseNpgsql(configuration[$"ConnectionStrings:PostgresSQL"]);
                });


            //Read Repositories           

            //Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
