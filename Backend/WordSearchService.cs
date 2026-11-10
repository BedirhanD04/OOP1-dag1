using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Backend
{
    public class WordSearchService : IWordSearchService
    {
        public WordSearchDTO Search(string text, string searchWord, SearchOptions options)
        {
            // 1. Tiden starter her
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Bestem sammenligningstypen baseret på valgmuligheden  // store og små bogstaver er forskellige  // store og små bogstaver er ens
            StringComparison comparison = options.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int matchCount = 0;
            int index = 0;

            while((index = text.IndexOf(searchWord, index, comparison)) != -1)
            {
                // Hvis WholeWordOnly er true, skal vi kontrollere, om det fundne ord er et helt ord // Tam kelime seçili değilse her eşleşme sayılır. Seçiliyse sadece tam kelime olanlar sayılı
                if (!options.WholeWord || IsWholeWord(text, index, searchWord.Length))
                {
                    matchCount++;   // eşleşmeyi say 
                }
                index += searchWord.Length; // Flyt indekset fremad for at fortsætte søgningen // eşleşmeyi atla, devam et
            }

            stopwatch.Stop();

            // Yeni: değerler record'un parantezindeki SIRAYLA verilir
            // Ny: værdierne angives i den RÆKKEFØLGE, som de står i record'ens parentes
            return new WordSearchDTO
            (
                searchWord,
                new WordCount(matchCount),
                new ElapsedTime(stopwatch.Elapsed.TotalMilliseconds)
            );
            

            
        }

        // Bulunan eşleşmenin tam kelime olup olmadığını kontrol eder // Tjekker om det fundne match er et helt ord
        public static bool IsWholeWord(string text, int start, int length)
        {
            bool StartOk = start == 0 || !char.IsLetterOrDigit(text[start - 1]); // Hvis der ikke er et bogstav/tal før (eller det er starten af teksten), er starten gyldig

            int end = start + length; // positionen umiddelbart efter matchet // eşleşmeden hemen sonraki konum

            // Sonunda harf/rakam yoksa (ya da metnin sonuysa) bitiş uygundur
            // Hvis der ikke er et bogstav/tal efter (eller det er slutningen af teksten), er slutningen gyldig
            bool EndOk = end == text.Length || !char.IsLetterOrDigit(text[end]); 

            return StartOk && EndOk;     // ikisi de uygunsa tam kelimedir // Hvis begge er gyldige, er det et helt ord
        }
    }
}
