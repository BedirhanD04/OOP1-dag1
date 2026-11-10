using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public readonly record struct WordCount // readonly record struct: değer tipi (value type) ve IMMUTABLE (oluşturulduktan sonra değişmez)
    {
        public int Value { get; } // Det er kun get ikke set, så Kan ikke ændres udefra
        public WordCount(int value) // Constructor: parametre ile değer atanır
        {
            if(value < 0) // Negativt antal match giver ikke mening, typen tillader det ikke
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Word count cannot be negative.");
            }
            Value = value; // Tildeles kun i constructoren, hvis den er gyldig
        }
        public override string ToString() => Value.ToString(); // ToString override: returnerer kun tallet som string

    }
}
