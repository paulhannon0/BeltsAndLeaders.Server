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
using BeltsAndLeaders.Server.Business.Commands.Users.CreateUser;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();

            // Queries
            services.AddScoped<IGetWidgetQuery, GetWidgetQuery>();

            // Repositories
            services.AddScoped<IWidgetsRepository, MysqlWidgetsRepository>();
            services.AddScoped<IUsersRepository, MysqlUsersRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeltsAndLeaders.Server", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BeltsAndLeaders.Server");
            });

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
