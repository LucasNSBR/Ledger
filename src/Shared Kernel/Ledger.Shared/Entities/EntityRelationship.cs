using System;

namespace Ledger.Shared.Entities
{
    //Represents a Many-To-Many relationship
    public abstract class EntityRelationship<TKey> where TKey : IEquatable<TKey>
    {
        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
    }
}
