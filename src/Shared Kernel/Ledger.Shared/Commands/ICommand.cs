using System;

namespace Ledger.Shared.Commands
{
    public interface ICommand
    {
        Guid CommandId { get; }
        DateTime DateCreated { get; }
    }
}
