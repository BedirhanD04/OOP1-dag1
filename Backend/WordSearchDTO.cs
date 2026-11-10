using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class WordSearchDTO
    {
        public string SearchWord { get; set; } = string.Empty;
        public int MatchCount { get; set; }
        public double ElapsedMilliseconds { get; set; }
    }
}
