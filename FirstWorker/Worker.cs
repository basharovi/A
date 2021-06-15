using System;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var jsonResult = JsonConvert.SerializeObject(CreateSamplePayload());
                _logger.LogInformation($"\n {jsonResult} \n");

                var delayDuration = Convert.ToInt32(Program._configuration.GetSection("DelayDuration").Value);
                await Task.Delay(1000 * delayDuration, stoppingToken);
            }
        }
         
        private static object[] CreateSamplePayload()
        {
            var payloads = new object[]
            {
                new
                {
                    timeStamp = DateTime.Now.ToString("dddd, dd MMMM yyyy, HH:mm:ss tt"),

                    deviceId = new Random().Next(1, 3),

                    power = new
                    {
                        plugL1 = "175.5",
                        plugL2 = "2414.4",
                        plugL3 = "123.2",
                        serverRack = "1472.7",
                        lab = "141.0"
                    },

                    enery = new
                    {
                        plugL3 = "19972.234",
                        serverRack = "13949.493",
                        lab = "3032.629"
                    }
                }
            };

            return payloads;
        }
    }
}
