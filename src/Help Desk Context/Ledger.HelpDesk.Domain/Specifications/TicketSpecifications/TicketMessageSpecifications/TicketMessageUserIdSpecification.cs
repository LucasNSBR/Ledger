using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.Shared.Specifications;
using System;
using System.Linq.Expressions;

namespace Ledger.HelpDesk.Domain.Specifications.TicketSpecifications.TicketMessageSpecifications
{
    public class TicketMessageUserIdSpecification : BaseSpecification<TicketMessage>
    {
        private readonly Guid _userId;

        public TicketMessageUserIdSpecification(Guid userId)
        {
            _userId = userId;
        }

        public override Expression<Func<TicketMessage, bool>> ToExpression()
        {
            return m => m.UserId == _userId;
        }
    }
}
