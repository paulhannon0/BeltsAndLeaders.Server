using FluentMigrator.Runner;
using BeltsAndLeaders.Server.Api.Extensions;
using BeltsAndLeaders.Server.Data.Repositories;
using BeltsAndLeaders.Server.Data.Repositories.Mysql;
using BeltsAndLeaders.Server.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BeltsAndLeaders.Server.Business.Commands.Users.CreateUser;
using Microsoft.OpenApi.Models;
using BeltsAndLeaders.Server.Business.Queries.Users.GetUser;
using BeltsAndLeaders.Server.Business.Queries.Users.GetAllUsers;
using BeltsAndLeaders.Server.Business.Commands.Users.UpdateUser;
using BeltsAndLeaders.Server.Business.Commands.Users.DeleteUser;
using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.CreateMaturityCategory;
using BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetAllMaturityCategories;
using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.DeleteMaturityCategory;
using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.UpdateMaturityCategory;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.CreateMaturityLevel;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetAllMaturityLevels;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.DeleteMaturityLevel;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.UpdateMaturityLevel;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevelsByCategoryId;
using BeltsAndLeaders.Server.Business.Commands.Achievements.CreateAchievement;
using System.Text.Json.Serialization;
using BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievementsByUserId;
using System;

namespace BeltsAndLeaders.Server.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private string allowOriginsPolicyName = "_allowOrigins";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy
                (
                    name: this.allowOriginsPolicyName,
                    builder =>
                    {
                        builder
                            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
            });

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
            services.AddScoped<ICreateUserCommand, CreateUserCommand>();
            services.AddScoped<IUpdateUserCommand, UpdateUserCommand>();
            services.AddScoped<IDeleteUserCommand, DeleteUserCommand>();
            services.AddScoped<ICreateMaturityCategoryCommand, CreateMaturityCategoryCommand>();
            services.AddScoped<IUpdateMaturityCategoryCommand, UpdateMaturityCategoryCommand>();
            services.AddScoped<IDeleteMaturityCategoryCommand, DeleteMaturityCategoryCommand>();
            services.AddScoped<ICreateMaturityLevelCommand, CreateMaturityLevelCommand>();
            services.AddScoped<IUpdateMaturityLevelCommand, UpdateMaturityLevelCommand>();
            services.AddScoped<IDeleteMaturityLevelCommand, DeleteMaturityLevelCommand>();
            services.AddScoped<ICreateAchievementCommand, CreateAchievementCommand>();

            // Queries
            services.AddScoped<IGetUserQuery, GetUserQuery>();
            services.AddScoped<IGetAllUsersQuery, GetAllUsersQuery>();
            services.AddScoped<IGetMaturityCategoryQuery, GetMaturityCategoryQuery>();
            services.AddScoped<IGetAllMaturityCategoriesQuery, GetAllMaturityCategoriesQuery>();
            services.AddScoped<IGetMaturityLevelQuery, GetMaturityLevelQuery>();
            services.AddScoped<IGetAllMaturityLevelsQuery, GetAllMaturityLevelsQuery>();
            services.AddScoped<IGetMaturityLevelsByCategoryIdQuery, GetMaturityLevelsByCategoryIdQuery>();
            services.AddScoped<IGetAchievementQuery, GetAchievementQuery>();
            services.AddScoped<IGetAchievementsByUserIdQuery, GetAchievementsByUserIdQuery>();

            // Repositories
            services.AddScoped<IUsersRepository, MysqlUsersRepository>();
            services.AddScoped<IMaturityCategoriesRepository, MysqlMaturityCategoriesRepository>();
            services.AddScoped<IMaturityLevelsRepository, MysqlMaturityLevelsRepository>();
            services.AddScoped<IAchievementsRepository, MysqlAchievementsRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BeltsAndLeaders.Server", Version = "v1" });
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

            app.UseCors(this.allowOriginsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Database.Create();
            Database.Migrate(migrationRunner);
        }
    }
}
