using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace WoofpakGamingSiteServerApp.Hosted_Services
{
    public class ExtraLifeDonationMonitorService : IHostedService, IDisposable
    {
        private bool disposedValue;
        private int executionCount = 0;
        private readonly ILogger<ExtraLifeDonationMonitorService> _logger;
        private Timer _timer;

        public ExtraLifeDonationMonitorService(ILogger<ExtraLifeDonationMonitorService> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ExtraLifeDonationMonitorService registered in Startup");
            _timer = new Timer(Poll, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        private void Poll(object state)
        {
            _logger.LogInformation("RaffleTicketMonitorService Poll()");

            var client = new RestClient("https://extralife.donordrive.com/api/teams/56545");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            _logger.LogInformation(response.Content);
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ExtraLifeDonationMonitorService registered in Startup");
            return Task.CompletedTask;
        }
    }
}
