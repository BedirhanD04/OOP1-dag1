using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class BasicWordSeacher : WordSearcher
    {
        // ": base(text, searchWord)" = ortak veriyi base constructor'a gönder (constructor zinciri)
        // ": base(text, searchWord)" = send fælles data til basiskonstruktøren (konstruktørkæde)
        public BasicWordSeacher(string text, string searchWord) : base(text, searchWord)
        {
            // Basic aramanın kendine ait ek bir verisi yok, o yüzden gövde boş
            // Basic-søgning har ikke egne ekstra data, så kroppen er tom
        }

        // override: Denne klasses specifikke version af den abstrakte Search()-metode i basisklassen (polymorfi)
        public override WordSearchDTO Search()
        {
            var stopWatch = System.Diagnostics.Stopwatch.StartNew(); // zaman ölçümü başlat

            int count = CountMatches(StringComparison.OrdinalIgnoreCase, false); // büyük/küçük harf duyarsız, tam kelime değil
            stopWatch.Stop();

            return new WordSearchDTO(
                SearchWord,
                new WordCount(count),
                new ElapsedTime(stopWatch.Elapsed.TotalMilliseconds)
            );

        }
    }
}
