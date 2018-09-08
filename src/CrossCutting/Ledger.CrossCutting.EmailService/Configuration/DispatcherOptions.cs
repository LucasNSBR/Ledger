namespace Ledger.CrossCutting.EmailService.Configuration
{
    public class DispatcherOptions 
    {
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
    }
}
