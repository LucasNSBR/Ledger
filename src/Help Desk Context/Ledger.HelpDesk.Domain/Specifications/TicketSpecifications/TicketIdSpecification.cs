using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.TicketSpecifications
{
    public class TicketIdSpecification : BaseSpecification<Ticket>
    {
        private readonly Guid _id;

        public TicketIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<Ticket, bool>> ToExpression()
        {
            return t => t.Id == _id;
        }
    }
}
