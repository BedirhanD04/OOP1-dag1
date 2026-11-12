using System;
using System.Collections.Generic;
using System.Text;
using Backend; // IWordSearchService


namespace OOP1
{
    public class UserMenu
    {
        private readonly SearchHistory _searchHistory; // Feltet til at gemme søgehistorikken

        public UserMenu(SearchHistory searchHistory) // Constructor: DI injicerer automatisk IWordSearchService her
        {
            _searchHistory = searchHistory; // Gem den modtagne søgehistorik i feltet
        }

        public void Run()
        {

            bool advanced = ReadSearchMode(); // 1) ÖNCE arama tipi (ödevin istediği sıra)
            string text = ReadText(); // 2) Metin girilir
            string word = ReadWord(); // 3) Aranacak kelime girilir

            // Basic için sabit varsayılan ayarlar, Advance seçilirse kullanıcıya sorulur
            SearchOptions options = new SearchOptions(false, false, true);
            
                if (advanced)
                {
                    options = ReadSearchOptions(); // 4) Arama seçenekleri girilir
                }
            

            bool running = true;
            bool needSearch = true; // Arama yapılması gerekip gerekmediğini kontrol etmek için

            while (running)
            {
                if (needSearch)
                {
                  Console.Clear();

                    // Base tipte değişken: içinde Basic de olabilir Advanced de
                    WordSearcher searcher = CreateSearcher(advanced, text, word, options); // Factory method: hangi tipte arama yapılacağını belirler
                    WordSearchDTO result = searcher.Search(); // hangisi olduğuna göre doğru Search() çalışır
                    _searchHistory.AddResult(result); // Sonuçlar kaydedilir

                    Console.WriteLine("Søgeord: " + result.SearchWord);
                    Console.WriteLine("Antal forekomster: " + result.MatchCount);

                    // Basic'te süre her zaman gösterilir, Advance'te kullanıcının seçimine bağlı
                    if(!advanced || options.ShowElapsedTime)
                    {
                        Console.WriteLine("Varighed: " + result.ElapsedMilliseconds + " ms");
                    }

                }
                needSearch = true; // Sonraki döngüde arama yapılması gerektiğini varsayalım

                Console.WriteLine("1: Søg efter nyt ord i samme tekst");
                Console.WriteLine("2: Søg samme ord i ny tekst");
                Console.WriteLine("3: Afslut");
                Console.WriteLine("4: Skift indstillinger (kun Advance)");
                Console.WriteLine("5: Vis alle søgninger");
                Console.WriteLine("6: Skift søgetype");
                Console.Write("Vælg: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        word = ReadWord();                             // yeni kelime, aynı metin
                        break;
                    case "2":
                        text = ReadText();                             // yeni metin, aynı kelime
                        break;
                    case "3":
                        running = false;
                        break;
                    case "4":
                        if (advanced)
                        {
                            options = ReadSearchOptions();             // sadece Advance'te ayar değişir
                        }
                        else
                        {
                            Console.WriteLine("Indstillinger findes kun i Advance søgning.");
                            needSearch = false;                        // arama yapma, mesaj ekranda kalsın
                        }
                        break;
                    case "5":
                        DisplaySearchHistory();
                        needSearch = false;
                        break;
                    case "6":
                        advanced = ReadSearchMode();                   // tipi değiştir
                        if (advanced)
                        {
                            options = ReadSearchOptions();             // Advance'e geçildiyse ayarları sor
                        }
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        needSearch = false;
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

        private bool ReadSearchMode()
        {
            while (true)
            {
                Console.WriteLine("Vælg søgemetode: 1) Basic, 2) Advanced");
                string input = Console.ReadLine();
                if (input == "1") return false; // Basic
                else if (input == "2") return true; // Advanced
                else Console.WriteLine("Ugyldigt valg. Indtast venligst '1' eller '2'.");
            }
        }

        private WordSearcher CreateSearcher(bool advanced, string text, string word, SearchOptions options)
        {

            if (advanced)
            {
                return new AdvancedWordSearcher(text, word, options); // 3 parametreli constructor
            }

                return new BasicWordSeacher(text, word); // 2 parametreli constructor
            
        }
    }
}
