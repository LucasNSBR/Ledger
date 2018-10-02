using Ledger.Shared.Entities.Locations;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.Shared.Locations.Specifications.CountrySpecifications
{
    public class CountryIdSpecification : BaseSpecification<Country>
    {
        private readonly Guid _id;

        public CountryIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Country, bool>> ToExpression()
        {
            return c => c.Id == _id;
        }
    }
}
