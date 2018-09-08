namespace Ledger.CrossCutting.EmailService.Configuration
{
    public class DispatcherOptions 
    {
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
        public string SendAddress { get; set; }
        public string SenderName { get; set; }
    }
}
