namespace Ledger.CrossCutting.Identity.Aggregates.UserAggregate
{
    public class Role 
    {
        public string Name { get; private set; }

        public Role(string name)
        {
            Name = name;
        }
    }
}
