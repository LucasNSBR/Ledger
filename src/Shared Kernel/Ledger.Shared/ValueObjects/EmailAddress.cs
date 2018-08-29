namespace Ledger.Shared.ValueObjects
{
    public class EmailAddress : ValueObject<EmailAddress>
    {
        public string Email { get; private set; }

        public EmailAddress() { }

        public EmailAddress(string email)
        {
            Email = email;
        }

        public override string ToString()
        {
            return Email;
        }
    }
}
