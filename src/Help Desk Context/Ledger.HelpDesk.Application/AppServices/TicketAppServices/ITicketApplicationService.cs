using Ledger.HelpDesk.Domain.Aggregates.TicketAggregate;
using Ledger.HelpDesk.Domain.Commands.TicketCommands;
using System;
using System.Linq;

namespace Ledger.HelpDesk.Application.AppServices.TicketAppServices
{
    public interface ITicketApplicationService
    {
        IQueryable<Ticket> GetAllTickets();
        IQueryable<Ticket> GetByUserId(Guid userId);
        Ticket GetById(Guid id);
        void Register(RegisterTicketCommand command);
        void AttachIssuePicture(AttachIssuePictureCommand command);
        void AssignSupport(AssignSupportCommand command);
        void AddMessage(AddMessageCommand command);
        void Close(CloseTicketCommand command);
    }
}
