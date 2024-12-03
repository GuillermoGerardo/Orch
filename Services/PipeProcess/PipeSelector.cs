using Microsoft.Extensions.Logging;
using Orch.Services.AbsenceProcess;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orch.Services.PipeProcess
{
    public class PipeSelector
    {
        public static ILogger<PipeSelector> _logger;
        public PipeSelector(ILogger<PipeSelector> logger) 
        {
            _logger = logger;
        }

        public static string RunPipe(string option, string args)
        {
            string sreturn = "";
            //Process process = Process.Start("PipeSelector.exe", option);

            //string breaker = "";

            
            //Task.Delay(5000);
            using (var client = new NamedPipeClientStream(".", "PipeSelector", PipeDirection.InOut))
            {
                client.Connect();
                using (var writer = new StreamWriter(client) { AutoFlush = true })
                using (var reader = new StreamReader(client))
                {
                    //writer.AutoFlush = true;
                    writer.WriteLine($"{args}");

                    sreturn = reader.ReadLine();
                    _logger.LogInformation($"Returning information from bridge: {sreturn}");
                }
            }
            return sreturn;
        }
    }
}
