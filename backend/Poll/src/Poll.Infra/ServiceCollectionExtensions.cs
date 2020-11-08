using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Poll.Domain.Interfaces;
using Poll.Domain.Interfaces.ReadRepository;
using Poll.Infra.Base;
using Poll.Infra.Context;
using Poll.Infra.Repositories;
using Poll.Infra.Repositories.Read;
using System;
using Tnf.Dapper;

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
                })
                .AddDapper(options =>
                {
                     options.DbType = DapperDbType.Postgre;
                });


            //Read Repositories   
            services.AddScoped<ITaskReadRepository, TaskReadRepository>();

            //Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITaskRepository, TasksRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            return services;
        }

        public static IServiceCollection AddDapper(this IServiceCollection services, Action<DapperOptions> options)
        {
            if (services.IsRegistered(typeof(IDapperRepository)))
                return services;


            var internalOptions = new DapperOptions();

            services.AddTransient<DapperOptions>(a => internalOptions);

            options(internalOptions);

            switch (internalOptions.DbType)
            {
                case DapperDbType.Sqlite:
                case DapperDbType.Oracle:
                    throw new Exception("Not implemented dbtype");
                case DapperDbType.Postgre:
                    services.AddTransient<IDapperProvider, DapperPostgreProvider>();
                    break;
                default:
                    services.AddTransient<IDapperProvider, DapperSqlServerProvider>();
                    break;
            }


            return services;
        }
    }
}
