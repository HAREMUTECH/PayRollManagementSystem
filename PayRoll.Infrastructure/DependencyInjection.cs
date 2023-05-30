using Azure.Storage.Blobs;
using FluentValidation;
//using Infrastructure.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayRoll.Application.Interfaces;
using PayRoll.Domain.Entities;
using PayRoll.Infrastructure.Implementation;
using PayRoll.Infrastructure.Persistence.Context;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

namespace PayRoll.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region pipeline

            /*
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            */

            #endregion

            services.AddSingleton<IServiceProvider>(sp => sp);
            services.AddHttpContextAccessor();

            //services.AddScoped<ExceptionMiddleware>();
            services.AddTransient<ICadreService, CadreService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILevelService, LevelService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ISalaryOptionService, SalaryOptionService>();
            services.AddTransient<IPayRollManagementService, PayRollManagementService>();
            services.AddHttpClient();
            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;

        }


        public static void AddAzureStorageService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(options =>
            {
                return new BlobServiceClient(configuration.GetConnectionString("AzureStorageConnectionString"));
            });
        }



        public static void SerilogSettings(this IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(new JsonFormatter())
                .WriteTo.MSSqlServer("Data Source=54.155.204.222;Initial Catalog=HrSystemLogs;Persist Security Info=False;User Id=sa;Password=3nFF6@5raYOf3;Encrypt=false;TrustServerCertificate=true",
                                     new MSSqlServerSinkOptions
                                     {
                                         TableName = "HRRecruitmentLog",
                                         SchemaName = "hrsystemslog",
                                         AutoCreateSqlTable = true
                                     })
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .CreateLogger();

        }
    }
}