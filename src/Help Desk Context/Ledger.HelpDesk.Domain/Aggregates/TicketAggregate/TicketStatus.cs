using System;

namespace Ledger.HelpDesk.Domain.Aggregates.TicketAggregate
{
    public class TicketStatus
    {
        public Status Status { get; private set; }
        public DateTime DateOpened { get; private set; }
        public DateTime? DateClosed { get; private set; }

        public void SetOpen()
        {
            DateOpened = DateTime.Now;
            Status = Status.Open;
        }

        public void SetClosed()
        {
            DateClosed = DateTime.Now;
            Status = Status.Closed;
        }
    }
}
