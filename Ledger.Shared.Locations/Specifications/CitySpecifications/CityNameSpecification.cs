using Ledger.Shared.Entities.Locations;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.CitySpecifications
{
    public class CityNameSpecification : BaseSpecification<City>
    {
        private readonly string _name;

        public CityNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<City, bool>> ToExpression()
        {
            return c => c.Name.ToLower().Contains(_name.ToLower());
        }
    }
}
