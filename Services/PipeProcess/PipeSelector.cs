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
        public ILogger<PipeSelector> _logger;
        public PipeSelector(ILogger<PipeSelector> logger) 
        {
            _logger = logger;
        }

        public string RunPipeCaseSelector(string option, string args)
        {
            _logger.LogInformation("Calling Pipe.");
            string sreturn = "";
            Process process = Process.Start("PipeSelector.exe", option);

            using (var client = new NamedPipeClientStream(".", "PipeSelector", PipeDirection.InOut))
            {
                client.Connect();
                using (var writer = new StreamWriter(client) { AutoFlush = true })
                using (var reader = new StreamReader(client))
                {
                    writer.WriteLine($"{args}");

                    sreturn = reader.ReadLine();
                    _logger.LogInformation($"Returning information from bridge: {sreturn}");
                    writer.Dispose();
                    reader.Dispose();
                }
                client.Dispose();
            }
            _logger.LogInformation("Closing pipe last step.");
            process.Dispose();
            return sreturn;
        }
    }
}
