using System;
using Figgle;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class Config
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