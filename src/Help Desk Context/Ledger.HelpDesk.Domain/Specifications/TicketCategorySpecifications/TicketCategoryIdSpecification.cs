using Ledger.HelpDesk.Domain.Aggregates.CategoryAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.TicketCategorySpecifications
{
    public class TicketCategoryIdSpecification : BaseSpecification<TicketCategory>
    {
        private readonly Guid _id;

        public TicketCategoryIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<TicketCategory, bool>> ToExpression()
        {
            return c => c.Id == _id;
        }
    }
}
