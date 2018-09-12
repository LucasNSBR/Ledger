using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Ledger.CrossCutting.ServiceBus.BackgroundTasks
{
    public class MassTransitProcess : IHostedService
    {
        private readonly IBusControl _bus;
        
        public MassTransitProcess(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync();
        }
    }
}
