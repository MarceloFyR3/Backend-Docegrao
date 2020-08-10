using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DoceGrao.Api.Shared.Utilits
{
    public static class UtilBase
    {
        public static bool HasDigit(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;

            foreach (char c in text)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        public static bool HasLetter(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                    return true;
            }
            return false;
        }

        public static bool HasSpecialCharacter(string text)
        {
            return Regex.IsMatch(text, "[^0-9a-zA-Z]+");
        }
    }
}
