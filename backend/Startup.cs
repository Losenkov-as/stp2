using BCryptNet = BCrypt.Net.BCrypt;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            // use sql server db in production and sqlite db in development

            services.AddDbContext<DataContext>();

            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //  AddJsonOptions(x =>
            //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            //         services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            // configure DI for application services

            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IMachineService, MachineService>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            // migrate any database changes on startup (includes initial db creation)
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<DataContext>();
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();

            }

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());


            //{
            //    var role1 = new Role { Name = "Администратор" };
            //    var role2 = new Role { Name = "Пользователь" };
            //    var users = new List<User>
            //    {

            //        new User { Username = "admin", PasswordHash = BCryptNet.HashPassword("admin"), Roles = new List<Role>{role1} },
            //        new User { Username = "user", PasswordHash = BCryptNet.HashPassword("user"), Roles = new List<Role>{role2} }
            //    };
            //    using var scope = app.ApplicationServices.CreateScope();
            //    var dataContexxt = scope.ServiceProvider.GetRequiredService<DataContext>();
            //    dataContext.Users.AddRange(users);
            //    dataContext.SaveChanges();
            //}
        }


    }
}
