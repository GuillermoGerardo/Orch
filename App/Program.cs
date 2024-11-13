using orch;
using App;
using System.CommandLine;
using Microsoft.Extensions.Hosting;

namespace Orch.App
{
    public class Orch
    {
        static async Task<int> Main(string[] args)
        {
            Config.InitDisplay();

            var dummyOption = new Option<bool>(
                    name: "--dummy",
                    description:"Run the app without write operations",
                    getDefaultValue: () => false
                );
            dummyOption.AddAlias("-d");
            var excellOption = new Option<bool>(
                    name: "--audit",
                    description:"Mode to export app result values to excell file",
                    getDefaultValue: () => false
                );
            excellOption.AddAlias("-a");
            var environmentOption = new Option<string>("--environment", "Target environment") { IsRequired = true};
            environmentOption.FromAmong("DEV_A","DEV_B","QA_A","QA_B","PSUP_A","PSUP_B","PSUP_E","PROD_A","PROD_B","PROD_C","PROD_E");
            environmentOption.AddAlias("-e");
            var rootCommand = new RootCommand("Command Orchestrator");
            rootCommand.AddGlobalOption(dummyOption);
            rootCommand.AddGlobalOption(excellOption);

            var AbscenceAssessment = new Command("abs", "Command app for Abscence Assessment Mass erroneous for In-progress");
            rootCommand.Add(AbscenceAssessment);

            AbscenceAssessment.AddOption(environmentOption);
            // rootCommand.SetHandler((dummy, audit) => {
            //     if (dummy){
            //         Console.WriteLine("dummy mode");
            //     }else{
            //         Console.WriteLine("normal mode");
            //     }
            // }, dummyOption, excellOption);
            
            AbscenceAssessment.SetHandler((dummy, audit, env) => {
                Console.WriteLine("Abscence Assesment");
                
                //if (dummy){
                //    Console.WriteLine("dummy mode");
                //}else{
                //    Console.WriteLine("write mode");
                //}
                //if  (audit){
                //    Console.WriteLine("audit mode");
                //}else{
                //    Console.WriteLine("no-log mode");
                //}


            }, dummyOption, excellOption, environmentOption);

            

            return await rootCommand.InvokeAsync(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args).ConfigureServices((HostContent, Services) =>
                {
                    
                });



    }
}



// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

// var host = builder.Build();
// host.Run();
