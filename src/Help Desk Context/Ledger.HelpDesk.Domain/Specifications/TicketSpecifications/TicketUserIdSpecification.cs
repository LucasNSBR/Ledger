using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.TicketSpecifications
{
    public class TicketUserIdSpecification : BaseSpecification<Ticket>
    {
        private readonly Guid _userId; 

        public TicketUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Ticket, bool>> ToExpression()
        {
            return t => t.TicketUserId == _userId;
        }
    }
}
