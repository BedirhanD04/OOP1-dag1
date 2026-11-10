using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class WordSearchService : IWordSearchService
    {
        public WordSearchDTO Search(string text, string searchWord)
        {
            // 1. Tiden starter her
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            int matchCount = 0;

            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(searchWord))
            {
                int index = 0;
                while ((index = text.IndexOf(searchWord, index, StringComparison.OrdinalIgnoreCase)) != -1)
                {

                    matchCount++;
                    index += searchWord.Length; // Aynı kelimenin üst üste sayılmaması için ilerliyoruz
                }
            }
            stopwatch.Stop();

            return new WordSearchDTO
            {
                SearchWord = searchWord,
                MatchCount = matchCount,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            };
        }
    }
}
