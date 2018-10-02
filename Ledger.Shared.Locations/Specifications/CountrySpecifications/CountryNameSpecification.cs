using Ledger.Shared.Entities.Locations;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.CountrySpecifications
{
    public class CountryNameSpecification : BaseSpecification<Country>
    {
        private readonly string _name;

        public CountryNameSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<Country, bool>> ToExpression()
        {
            return c => c.Name.ToLower() == _name.ToLower();
        }
    }
}
