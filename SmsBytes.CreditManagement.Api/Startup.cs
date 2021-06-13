using Micro.Auth.Sdk;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmsBytes.CreditManagement.Api.GraphQL.Extensions;
using SmsBytes.CreditManagement.Api.Internal.Configs;
using SmsBytes.CreditManagement.Api.Internal.StartupExtensions;

namespace SmsBytes.CreditManagement.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(Configuration);
            services.AddMetrics();
            services.ConfigureRequiredDependencies();
            services.ConfigureHealthChecks();
            services.AddControllers();
            services.ConfigureSwagger();
            services.RegisterWorker();
            services.ConfigureGraphql();
            services.ConfigureAuthServices(new Config
            {
                KeyStoreUrl = Configuration.GetSection("Services").Get<Services>().KeyStore.Url,
                ValidIssuer = "my_app_auth",
                ValidAudiences = new []{"my_app"}
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<SlackLoggingConfig> slackConfig)
        {
            loggerFactory.ConfigureLoggerWithSlack(slackConfig.Value, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.SetupAuth();
            app.SetupGraphQl();
            app.UseRouting();
            app.AddSwaggerWithUi();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.ConfigureHealthCheckEndpoint();
            });
        }
    }
}
