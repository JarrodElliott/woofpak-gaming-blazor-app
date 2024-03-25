using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Hosted_Services
{
    public class RaffleTicketMonitorService : IHostedService, IDisposable
    {
        private bool disposedValue;
        private int executionCount = 0;
        private readonly ILogger<RaffleTicketMonitorService> _logger;
        private Timer _timer;

        public RaffleTicketMonitorService(ILogger<RaffleTicketMonitorService> logger)
        {
            _logger = logger;
        }
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ExtraLifeDonationMonitorService registered in Startup");
            _timer = new Timer(Poll, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void Poll(object state)
        {   
            _logger.LogInformation("RaffleTicketMonitorService Poll()");
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ExtraLifeDonationMonitorService registered in Startup");
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                _timer?.Dispose();
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RaffleTicketMonitorService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
