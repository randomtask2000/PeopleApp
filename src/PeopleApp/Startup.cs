using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PeopleApp.Models;
using Microsoft.EntityFrameworkCore;
using PeopleApp.Filters.ActionFilters;
using PeopleApp.Filters.ExceptionFilters;
using PeopleApp.Filters.ResourceFilters;

namespace PeopleApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _configuration = builder.Build();
        }

        public IConfigurationRoot _configuration { get; }
        private readonly IHostingEnvironment _hostingEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(_configuration);
            services.AddMvc(
                config =>
                {
                    config.Filters.Add(new GlobalFilter(loggerFactory));
                    config.Filters.Add(new GlobalLoggingExceptionFilter(loggerFactory));
                });

            // Register filter services
            services.AddScoped<LogActionFilter>();
            services.AddScoped<LoggingExceptionFilter>();
            services.AddScoped<ResourceFilter>();

            // Add connection string form appsettings.json file
            var dbPath = DbPath();

            var connection = _configuration.GetConnectionString("DefaultConnection") + dbPath;
            
            services.AddDbContext<PeopleContext>(options => options.UseSqlServer(connection));

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            // Add our Config object so it can be injected
            var settings = services.Configure<Settings>(_configuration.GetSection("Settings"));

            // *If* you need access to generic IConfiguration this is **required**
            services.AddSingleton<IConfiguration>(_configuration);
        }

        public string DbPath()
        {
            var dataPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            var databaseFile = _configuration.GetConnectionString("Database");
            var dbPath = System.IO.Path.Combine(dataPath, databaseFile);
            return dbPath;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=People}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
