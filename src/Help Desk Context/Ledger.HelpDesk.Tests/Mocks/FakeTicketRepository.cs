using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Repositories.TicketRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ledger.HelpDesk.Tests.Mocks
{
    public class FakeTicketRepository : ITicketRepository
    {
        private readonly List<Ticket> _tickets;

        public FakeTicketRepository()
        {
            _tickets = new List<Ticket>
            {
                new Ticket(new Guid("36f90131-8ab3-4764-a56c-2ee78284562f"), "Meu sistema não funciona", "não funciona", new Guid("3fe0fcc8-b00d-40ec-ac18-cbeb769ff216"), new Guid("5227c760-f6f0-4e72-b3fa-19059c58d8e3"))
            };
        }

        public IQueryable<Ticket> GetAllTickets()
        {
            return _tickets.AsQueryable();
        }

        public Ticket GetById(Guid id)
        {
            return _tickets.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Ticket> GetByUserId(Guid userId)
        {
            return _tickets.Where(t => t.TicketUserId == userId).AsQueryable();
        }

        public void Register(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            int index = _tickets.FindIndex(a => a.Id == ticket.Id);
            _tickets[index] = ticket;
        }
    }
}
