using Ledger.Shared.ValueObjects;
using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketStatus : ValueObject<TicketStatus>
    {
        public Status Status { get; private set; }
        public DateTime DateOpened { get; private set; }
        public DateTime? DateClosed { get; private set; }

        protected TicketStatus()
        {
        }

        private TicketStatus(Status status, DateTime? dateClosed = null)
        {
            Status = status;
            DateOpened = DateTime.Now;
            DateClosed = dateClosed;
        }

        public static TicketStatus SetOpen()
        {
            return new TicketStatus(Status.Open);
        }

        public static TicketStatus SetClosed()
        {
            return new TicketStatus(Status.Closed, DateTime.Now);
        }

        public override string ToString()
        {
            return Status.ToString();
        }
    }
}
