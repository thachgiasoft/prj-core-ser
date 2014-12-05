using System.Configuration;

namespace Vietbait.Config
{
    internal class Item
    {
        public Item(string name, string value, string desc)
        {
            Name = name;
            Value = value;
            Desc = desc;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Desc { get; set; }
    }
}