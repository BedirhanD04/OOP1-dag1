using System;
using System.Collections.Generic;
using System.Text;
using Backend; // IWordSearchService


namespace OOP1
{
    public class UserMenu
    {
        private readonly IWordSearchService _searchService; // Feltet til at gemme backend-servicen

        public UserMenu(IWordSearchService searchService) // Constructor: DI injicerer automatisk IWordSearchService her
        {
            _searchService = searchService; // Gem den modtagne service i feltet
        }

        public void Run()
        {
            string text = ReadText();                            // metni al
            string word = ReadWord();                            // kelimeyi al
            bool running = true;                                   // program çalışıyor mu?

            while (running)
            {
                Console.Clear();
                WordSearchDTO result = _searchService.Search(text, word); // backend servisini kullanarak arama yap
                Console.WriteLine("Søgeord: " + result.SearchWord);
                Console.WriteLine("Antal forekomster: " + result.MatchCount);
                // F3 = virgülden sonra 3 basamak
                Console.WriteLine("Tid: " + result.ElapsedMilliseconds.ToString("F3") + " ms");
                Console.WriteLine("1: Søg efter nyt ord i samme tekst");            // menü Option
                Console.WriteLine("2: Søg samme ord i ny tekst");
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
                    default:
                        Console.WriteLine("Ugyldigt valg. Afslutter...");
                        break;
                }
            }
      
        }

        private string ReadText()
        {
            Console.WriteLine("Indsæt teksten. Skriv END for at afslutte:"); // talimat / Instruktion

            string text = "";  // Vi vil samle teksten her

            string line = Console.ReadLine(); // ilk satırı oku / læse første linje

            while(line != "END") // Fortsætter indtil END
            {
                text += line + "\n"; //INSTAST LINJEN TIL TEXT
                line = Console.ReadLine(); // Læser næste linje 
            }
            return text; //retuner texten
        }

        private string ReadWord()
        {
            Console.WriteLine("indtast søgende ord");
            string word = Console.ReadLine();
            return TextHelper.FormatText(word);
        }
    }
}
