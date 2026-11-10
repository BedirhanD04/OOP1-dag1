using System;
using System.Collections.Generic;
using System.Text;
using Backend; // IWordSearchService


namespace OOP1
{
    public class UserMenu
    {
        private readonly IWordSearchService _searchService; // Feltet til at gemme backend-servicen
        private readonly SearchHistory _searchHistory; // Feltet til at gemme søgehistorikken

        public UserMenu(IWordSearchService searchService, SearchHistory searchHistory) // Constructor: DI injicerer automatisk IWordSearchService her
        {
            _searchService = searchService; // Gem den modtagne service i feltet
            _searchHistory = searchHistory; // Gem den modtagne søgehistorik i feltet
        }

        public void Run()
        {
            string text = ReadText();                            // metni al
            string word = ReadWord();                            // kelimeyi al
            SearchOptions options = ReadSearchOptions();         // arama seçeneklerini al
            bool running = true;                                   // program çalışıyor mu?
            bool needSearch = true;                                    // arama yapılması gerekiyor mu?

            while (running)
            {
                if(needSearch)
                {
                    Console.Clear();
                    WordSearchDTO resultt = _searchService.Search(text, word, options); // backend servisini kullanarak arama yap
                    _searchHistory.AddResult(resultt); // Arama sonucunu geçmişe ekle

                    Console.WriteLine("Søgeord: " + resultt.SearchWord);
                    Console.WriteLine("Antal forekomster: " + resultt.MatchCount);
                    if (options.showElapsedTime)
                    {
                        Console.WriteLine("Tid: " + resultt.ElapsedMilliseconds + " ms");
                    }
                }
                needSearch = true; // Varsayılan olarak bir sonraki döngüde arama yapılması gerekiyor
                Console.WriteLine("1: Søg efter nyt ord i samme tekst");            // menü Option
                Console.WriteLine("2: Søg samme ord i ny tekst");
                Console.WriteLine("4: Skift indstillinger");
                Console.WriteLine("5: Vis alle søgninger");  // yeni menü satırı
                Console.WriteLine("3: Afslut");
                Console.Write("Vælg: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        word = ReadWord(); // Samme tekst, nyt ord
                        break;
                    case "2":
                        text = ReadText(); // Ny tekst, samme ord
                        break;
                    case "3":
                        running = false; // Afslut programmet
                        break;
                    case "4":
                        options = ReadSearchOptions(); // Skift indstillinger
                        break;
                    case "5":
                        DisplaySearchHistory(); // Vis alle søgninger
                        needSearch = false; // Ingen ny søgning, bare vis historikken
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg. Afslutter...");
                        needSearch = false; // Ingen ny søgning, bare vis historikken
                        break;
                }
            }
      
        }

        private string ReadText()
        {
            while (true) // geçerli metin girilene kadar tekrar sor
            {
                Console.WriteLine("Indsæt teksten. Skriv END for at afslutte:"); // talimat

                string text = "";                  // metni burada biriktireceğiz
                string line = Console.ReadLine();  // ilk satırı oku

                while (line != "END")              // END gelene kadar oku
                {
                    text += line + "\n";           // satırı metne ekle
                    line = Console.ReadLine();     // sıradaki satırı oku
                }

                if (TextHelper.IsValidText(text))  // kural TextHelper'da, burada sadece çağırıyoruz
                {
                    return text;                   // geçerliyse metni döndür, döngü biter
                }

                Console.WriteLine("Teksten må ikke være tom."); // geçersizse uyar, döngü başa döner
            }
        }

        private string ReadWord()
        {
            while (true)
            {
                Console.WriteLine("Indsæt søgende ord:");
                string word = Console.ReadLine();
                if (TextHelper.IsValidText(word))
                {
                    return TextHelper.FormatText(word);
                }
                Console.WriteLine("Søgeordet må ikke være tomt.");
            }
        }

        private bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (y/n): ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y" || input == "yes") return true;

                else if (input == "n" || input == "no") return false;

                else Console.WriteLine("Ugyldigt valg. Indtast venligst 'y' eller 'n'.");
                
            }
        }

        private SearchOptions ReadSearchOptions()
        {
            bool caseSensitive = AskYesNo("Skal søgningen være store/små bogstaver følsom?");
            bool wholeWord = AskYesNo("Skal kun hele ord tælles?");
            bool showElapsedTime = AskYesNo("Vil du se varigheden af søgningen?");
            return new SearchOptions(caseSensitive, wholeWord, showElapsedTime);
        }

        private void DisplaySearchHistory()
        {
            var results = _searchHistory.GetResults(); // Søgehistorikken hentes fra SearchHistory
            if (results.Count == 0)
            {
                Console.WriteLine("Ingen tidligere søgninger.");
                return;
            }

            // PadRight(20): metni 20 karaktere tamamlar, böylece sütunlar hizalı olur
            Console.WriteLine("Søgeord".PadRight(20) + "Antal".PadRight(10) + "Tid (ms)");
            Console.WriteLine(new string('-', 40)); // ayırıcı çizgi
            foreach (WordSearchDTO r in results) // her sonuç için bir satır
            {
                Console.WriteLine(r.SearchWord.PadRight(20) + r.MatchCount.ToString().PadRight(10) + r.ElapsedMilliseconds);
            }

        }
    }
}
