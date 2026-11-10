using System;
using System.Collections.Generic;
using System.Text;

namespace Backend
{
    public class TextHelper
    {
        public static bool IsValidText(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }


        public static string FormatText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;

            }
            return text.Trim();

        }
    }
}
