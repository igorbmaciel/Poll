using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Poll.Web;
using Microsoft.AspNetCore.Builder;
using Poll.Domain;

namespace Poll.Tests
{
    public class StartupUnitPropertyTest
    {
        private IConfiguration Configuration { get; set; }

        public StartupUnitPropertyTest(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
         
            //Configura o setup de teste para AspNetCore
            services
                     .AddTnfAspNetCoreSetupTest();

            services.AddInfraDependency(Configuration);
            services.AddApplicationServiceDependency(Configuration);            

            var serviceProvider = services.BuildServiceProvider();

            ServiceLocator.SetLocatorProvider(serviceProvider);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // Configura o uso do teste
            app.UseTnfAspNetCoreSetupTest();

            app.UseTnfAspNetCoreSetupTest(options =>
            {
                options.UseDomainLocalization();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
