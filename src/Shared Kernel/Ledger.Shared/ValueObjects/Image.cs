using System;

namespace Ledger.Shared.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public byte[] Data { get; private set; }
        public string Name { get; private set; }

        protected Image() { }

        public Image(byte[] data)
        {
            Data = data;
            Name = Guid.NewGuid().ToString();
        }

        public Image(byte[] data, string name)
        {
            Data = data;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
