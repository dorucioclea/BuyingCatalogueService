using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHSD.BuyingCatalogue.API.Extensions;
using NHSD.BuyingCatalogue.API.Infrastructure;
using NHSD.BuyingCatalogue.API.Infrastructure.HealthChecks;
using NHSD.BuyingCatalogue.Application;
using NHSD.BuyingCatalogue.Application.SolutionList.Mapping;
using NHSD.BuyingCatalogue.Capabilities.Application;
using NHSD.BuyingCatalogue.Capabilities.Application.Mapping;
using NHSD.BuyingCatalogue.Contracts;
using NHSD.BuyingCatalogue.Contracts.Infrastructure;
using NHSD.BuyingCatalogue.Persistence;

namespace NHSD.BuyingCatalogue.API
{
    /// <summary>
    /// Represents a bootstrapper for the application. Used as a starting point to configure the API.
    /// </summary>
    public sealed class Startup
    {
        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <remarks>This method gets called by the runtime. Use this method to add services to the container.</remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            var myAssemblies = new[]
            {
                Assembly.GetAssembly(typeof(SolutionListAutoMapperProfile)),
                Assembly.GetAssembly(typeof(CapabilityAutoMapperProfile)),
            };

            services
                .AddTransient<ISettings, Settings>()
                .AddAutoMapper(myAssemblies)
                .RegisterApplication()
                .RegisterCapabilitiesApplication()
                .RegisterPersistence()
                .AddCustomHealthCheck()
                .AddCustomSwagger()
                .AddCustomMvc()
                .RegisterRequests();
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment details.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger()
                   .UseSwaggerUI(options =>
                  {
                      options.SwaggerEndpoint("/swagger/v1/swagger.json", "Buying Catalog API V1");
                  });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Live)
                });

                endpoints.MapHealthChecks("/health/dependencies", new HealthCheckOptions
                {
                    Predicate = (healthCheckRegistration) => healthCheckRegistration.Tags.Contains(HealthCheckTags.Dependencies)
                });

                endpoints.MapControllers();
            });
        }
    }
}
