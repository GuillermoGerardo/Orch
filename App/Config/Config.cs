using System;
using Figgle;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ClosedXML.Excel;
using orch;
using orch.Domain.Context;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Orch.App;
using Orch.Services.AbsenceProcess;
using Orch.Services.PipeProcess;
using Orch.Infrastructure.Interfaces;
using Orch.Infrastructure.Repository;
using orch.Services.QueryProcess;
using Orch.Services.ExcellProcess;

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
                    //Services.AddHostedService<Worker>();
                })
            .UseSerilog((context, configuration) => {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

        //public static void RegisterServices(IServiceCollection services)
        //{
        //    //services.AddTransient<Appsettings>();
        //    //services.AddTransient<ModalityRecords>();
        //    //services.AddTransient<ExcellRunService>();
        //    //services.AddScoped<IModalityDetailsRepository, ModalityDetailsRepository>();
        //    //services.AddTransient<XLWorkbook>();

        //}

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
            // services.AddTransient<Soarian_Clin_Prd_1Context>();
        }

        //     public static void StartDatabase(this IServiceCollection services, IConfiguration configuration, string instance)
        //    {
        //         // services.AddDbContext<Soarian_Clin_Prd_1Context>(options => {
        //         //     options.UseSqlServer(configuration.GetConnectionString(instance)
        //         //     ,sqlServerOptionsAction: sqlOptions =>
        //         //     {
        //         //        sqlOptions.EnableRetryOnFailure();
        //         //     //    sqlOptions.CommandTimeout(0);
        //         //     });
        //         //     }, ServiceLifetime.Transient);
        //         // services.AddTransient<Soarian_Clin_Prd_1Context>();
        //    } 
    }
}