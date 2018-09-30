using System.Reflection;

namespace Ledger.Shared.ValueObjects
{
    public abstract class ValueObject<T> where T: ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            ValueObject<T> other = obj as ValueObject<T>;
            
            if (other is null)
                return false;

            bool isEqual = true;

            PropertyInfo[] properties = GetType().GetProperties();
            foreach (var property in properties)
            {
                dynamic thisValue = property.GetValue(this);
                dynamic otherValue = property.GetValue(other);

                if(thisValue != otherValue)
                    isEqual = false;
            }

            return isEqual;
        }

        public override int GetHashCode()
        {
            int totalSum = 0;
            PropertyInfo[] properties = GetType().GetProperties();

            foreach (var field in properties)
            {
                var fieldName = GetType().FullName + field.Name;
                var fieldValue = field.GetValue(this);
                if(fieldValue != null)
                {
                    totalSum += (fieldName.GetHashCode() + fieldValue.GetHashCode());
                }
            }

            return totalSum;
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !Equals(a, b);
        }

        public abstract override string ToString();
    }
}
