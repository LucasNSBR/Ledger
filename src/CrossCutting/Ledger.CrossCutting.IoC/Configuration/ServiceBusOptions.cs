namespace Ledger.CrossCutting.IoC.Configuration
{
    public class ServiceBusOptions
    {
        public string HostAddress { get; set; }
        public string RabbitMqHostUser { get; set; }
        public string RabbitMqHostPassword { get; set; }
    }
}
