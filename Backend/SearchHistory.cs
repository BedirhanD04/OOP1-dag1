using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class SearchHistory
    {
        // private: listen kan ikke ændres udefra, kun denne klasse har adgang (inkapsling)
        private readonly List<WordSearchDTO> _result = new List<WordSearchDTO>();

        // Tilføjer et nyt søgeresultat til listen
        public void AddResult(WordSearchDTO result)
        {
            _result.Add(result); // Tilføj resultatet til slutningen af listen
        }

        public IReadOnlyList<WordSearchDTO> GetResults()
        {
            var sortedResults = new List<WordSearchDTO>(_result); // Opret en kopi af listen

            // Sort() iki sonucu nasıl karşılaştıracağını bilmek ister.
            // (a, b) => ... "a ve b'yi şöyle karşılaştır" demek (lambda, bir shorthand)
            sortedResults.Sort((a, b) => string.Compare(a.SearchWord, b.SearchWord, StringComparison.OrdinalIgnoreCase));
            return sortedResults;// salt okunur liste olarak döndür: dışarıdan Add/Remove yapılamaz
        }

    }
}
