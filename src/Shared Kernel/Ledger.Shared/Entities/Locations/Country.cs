namespace Ledger.Shared.Entities.Locations
{
    public class Country : Entity<Country>
    {
        public string ShortName { get; private set; }
        public string Name { get; private set; }

        public Country(string shortName, string name)
        {
            ShortName = shortName;
            Name = name;
        }
    }
}
