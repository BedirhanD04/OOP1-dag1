using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    //abstract: bu sınıftan doğrudan "new WordSearcher()" yazılamaz, sadece miras alınır
    public abstract class WordSearcher
    {
       // sadece bu sınıf ve ondan türeyenler erişir
       protected string Text { get;}
       protected string SearchWord { get;}

        // base constructor: 
        protected WordSearcher(string text, string searchWord)
        {
            Text = text;
            SearchWord = searchWord;
        }

        // abstract metot: gövdesi YOK, her derived sınıf kendi versiyonunu yazmak ZORUNDA
        //(all derived classes skal skrive deres egen version af metoden)
        public abstract WordSearchDTO Search();

        // protected yardımcı: iki derived sınıf da kullanır, kod tekrarlanmaz (kapsülleme)
        protected int CountMatches(StringComparison comparison, bool wholeWord)
        {
            int matchCount = 0;
            int index = 0;

            while ((index = Text.IndexOf(SearchWord, index, comparison)) != -1)
            {
                if (!wholeWord || IsWholeWord(index, SearchWord.Length)) // tam kelime isteniyorsa kontrol et
                {
                    {
                        matchCount++;
                    }
                }
                index += SearchWord.Length;
            }
            return matchCount;
        }

        protected bool IsWholeWord(int start, int length)
        {
            bool StartOk = start == 0 || !char.IsLetterOrDigit(Text[start - 1]);
            int end = start + length;
            bool EndOk = end == Text.Length || !char.IsLetterOrDigit(Text[end]);
            return StartOk && EndOk;
        }
    }
}
