using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public interface IWordSearchService
    {
        WordSearchDTO Search(string text, string searchWord);
    }
}