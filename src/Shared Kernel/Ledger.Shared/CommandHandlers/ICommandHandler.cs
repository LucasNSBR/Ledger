using Ledger.Shared.Commands;
using System.Threading.Tasks;

namespace Ledger.Shared.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
