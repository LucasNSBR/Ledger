namespace Ledger.Shared.Entities
{
    public class State : Entity<State>
    {
        public string Initials { get; private set; }
        public string Name { get; private set; }

        public State(string initials, string name)
        {
            Initials = initials;
            Name = name;
        }
    }
}
