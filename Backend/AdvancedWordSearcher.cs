using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    // Nedarver fra WordSearcher. Bruger samme basisklasse som Basic, men skriver sin egen søgeversion
    public class AdvancedWordSearcher : WordSearcher
    {
        // Sadece Advanced'e ait veri: kullanıcının seçtiği ayarlar.
        // private: base'e ya da başka sınıflara lazım değil, sadece bu sınıf kullanı
        private readonly SearchOptions _options;

        public AdvancedWordSearcher(string text, string searchWord, SearchOptions options) : base(text, searchWord)
        {
            _options = options;
        }

        public override WordSearchDTO Search()
        {
            var stopWatch = System.Diagnostics.Stopwatch.StartNew(); // zaman ölçümü başlat

            StringComparison comparison = _options.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int count = CountMatches(comparison, _options.WholeWord); // ayarlara göre eşleşmeleri say
            stopWatch.Stop();
            return new WordSearchDTO(
                SearchWord,
                new WordCount(count),
                new ElapsedTime(stopWatch.Elapsed.TotalMilliseconds)
            );
        }


    }
}
