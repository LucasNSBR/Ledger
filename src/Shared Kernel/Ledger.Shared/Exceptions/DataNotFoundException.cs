using System;

namespace Ledger.Shared.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() : base("O objeto não foi encontrado no banco de dados")
        {
        }

        public DataNotFoundException(string message) : base(message)
        {
        }
    }
}
