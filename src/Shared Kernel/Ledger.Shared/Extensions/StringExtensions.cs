using System.Text;

namespace Ledger.Shared.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}
