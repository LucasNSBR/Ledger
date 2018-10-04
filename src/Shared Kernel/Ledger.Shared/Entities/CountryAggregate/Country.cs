using System;

namespace Ledger.Shared.Entities.CountryAggregate
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

        public Country(Guid id, string shortName, string name)
        {
            Id = id;
            ShortName = shortName;
            Name = name;
        }
    }
}
