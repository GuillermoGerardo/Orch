using ClosedXML.Excel;
using Figgle;
using Microsoft.EntityFrameworkCore;
using orch.Domain.Context;
using orch.Services.QueryProcess;
using Orch.App;
using Orch.Infrastructure.Interfaces;
using Orch.Infrastructure.Repository;
using Orch.Services.AbsenceProcess;
using Orch.Services.ExcellProcess;
using Orch.Services.PipeProcess;
using Serilog;

namespace App
{
    public static class Config
    {
        public static void InitDisplay()
        {
            using var fontStream = System.IO.File.OpenRead(".\\assets\\swampland.flf");
            var font = FiggleFontParser.Parse(fontStream);
            Console.WriteLine(font.Render("Orch"));
            Console.SetCursorPosition(10, Console.CursorTop - 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Orchestrator for cmd apps");
            Console.ResetColor();
        }

        public static void SwitchConsole(ConsoleColor color, string Message) 
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{Message}");
            Console.ResetColor();
        }

        public static IHostBuilder AbsenceBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args).ConfigureServices((HostContent, Services) =>
                {
                    Services.AddTransient<XLWorkbook>();
                    Services.StartDatabase(HostContent.Configuration, args[0]);
                    Services.AddTransient<AbsenceAssessment>();
                    Services.AddTransient<QueryService>();
                    Services.AddTransient<PipeSelector>();
                    Services.AddTransient<ExcellService>();
                    Services.AddScoped<IAbsenceAssessmentDetails, SoarianRepository>();
                    Services.AddScoped<IGenericQuery, SoarianRepository>();
                    Services.AddHostedService<AbsWorker>();
                })
            .UseSerilog((context, configuration) => {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        public static void StartDatabase(this IServiceCollection services, IConfiguration configuration, string instance)
        {
            services.AddDbContext<SoarianContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString(instance)
                , sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                    //    sqlOptions.CommandTimeout(0);
                });
            }, ServiceLifetime.Transient);
        }
    }
}