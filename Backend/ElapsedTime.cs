using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public readonly record struct ElapsedTime
    {
        public double Milliseconds { get; }

        public ElapsedTime(double milliseconds)
        {
            if (milliseconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds), "Elapsed time cannot be negative.");
            }
            Milliseconds = milliseconds; // Sættes kun i constructoren
        }
        public override string ToString() => Milliseconds.ToString("F6"); 
    }
}
