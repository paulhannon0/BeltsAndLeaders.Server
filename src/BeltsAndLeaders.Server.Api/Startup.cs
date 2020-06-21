using FluentMigrator.Runner;
using BeltsAndLeaders.Server.Api.Extensions;
using BeltsAndLeaders.Server.Business.Commands.Widgets.CreateWidget;
using BeltsAndLeaders.Server.Business.Queries.Widgets.GetWidget;
using BeltsAndLeaders.Server.Data.Repositories;
using BeltsAndLeaders.Server.Data.Repositories.Mysql;
using BeltsAndLeaders.Server.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeltsAndLeaders.Server.Business.Commands.Widgets.UpdateWidget;
using BeltsAndLeaders.Server.Business.Commands.Widgets.DeleteWidget;

namespace BeltsAndLeaders.Server.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddFluentMigratorCore()
                .ConfigureRunner
                (
                    builder => builder
                        .AddMySql5()
                        .WithGlobalConnectionString(ConfigurationProfile.SelectedDatabaseConnectionString)
                        .ScanIn(typeof(IRecord).Assembly).For.Migrations()
               );

            // Commands
            services.AddScoped<ICreateWidgetCommand, CreateWidgetCommand>();
            services.AddScoped<IUpdateWidgetCommand, UpdateWidgetCommand>();
            services.AddScoped<IDeleteWidgetCommand, DeleteWidgetCommand>();

            // Queries
            services.AddScoped<IGetWidgetQuery, GetWidgetQuery>();

            // Repositories
            services.AddScoped<IWidgetsRepository, MysqlWidgetsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseHttpExceptionHandling();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Database.Create();
            Database.Migrate(migrationRunner);
        }
    }
}
