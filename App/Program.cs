using orch;
using App;
using System.CommandLine;
using System.CommandLine.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.CommandLine.Parsing;
using Serilog;
using System.Linq;

namespace Orch.App
{
    public class Orch
    {
        static async Task<int> Main(string[] args)
        {
            Config.InitDisplay();

            var dummyOption = new Option<bool>(
                    name: "--logging",
                    description:"Run the app without write operations",
                    getDefaultValue: () => false
                );
            dummyOption.AddAlias("-l");
            var excellOption = new Option<bool>(
                    name: "--audit",
                    description:"Mode to export app result values to excell file",
                    getDefaultValue: () => false
                );
            excellOption.AddAlias("-a");
            //var filterOption = new Option<string>("--filter", "Filter results from query") { IsRequired = false };
            var filterOption = new Option<string>(
                    name: "--filter",
                    description: "Filter results from query, add ':' Char to make it as array",
                    isDefault: false,
                    parseArgument: result =>
                    {
                        if (String.IsNullOrEmpty(result.Tokens.Single().Value) || !result.Tokens.Single().Value.Contains(':'))
                        {
                            result.ErrorMessage = "Incorrect format/No parameter, switch to false filter flag";
                            return "";
                        }
                        return result.Tokens.Single().Value;
                    }
                    //getDefaultValue: () => false
                );
            filterOption.AddAlias("-f");
            var environmentOption = new Option<string>("--environment", "Target environment") { IsRequired = true};
            environmentOption.FromAmong("DEV_A","DEV_B","QA_A","QA_B","PSUP_A","PSUP_B","PSUP_E","PROD_A","PROD_B","PROD_C","PROD_E");
            environmentOption.AddAlias("-e");
            var rootCommand = new RootCommand("Command Orchestrator");
            rootCommand.AddGlobalOption(dummyOption);
            rootCommand.AddGlobalOption(excellOption);

            var AbscenceAssessment = new Command("abs", "Command app for Abscence Assessment Mass erroneous for In-progress");
            rootCommand.Add(AbscenceAssessment);

            AbscenceAssessment.AddOption(environmentOption);
            AbscenceAssessment.AddOption(filterOption);
            AbscenceAssessment.SetHandler((dummy, audit, env, filter) =>
            {
                Config.SwitchConsole(ConsoleColor.Green, $"Abscence Assesment Environment - {env}");
                string[] argumentList = { env };
                Config.AbsenceBuilder(argumentList).Build().Run();
            }, dummyOption, excellOption, environmentOption, filterOption);


            return await rootCommand.InvokeAsync(args);
        }
    }
}