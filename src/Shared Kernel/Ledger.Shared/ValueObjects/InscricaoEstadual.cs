namespace Ledger.Shared.ValueObjects
{
    public class InscricaoEstadual : ValueObject<InscricaoEstadual>
    {
        public string NumeroInscricao { get; private set; }

        protected InscricaoEstadual() { }

        public InscricaoEstadual(string numeroInscricao)
        {
            NumeroInscricao = numeroInscricao;
        }

        public override string ToString()
        {
            return NumeroInscricao;
        }
    }
}
