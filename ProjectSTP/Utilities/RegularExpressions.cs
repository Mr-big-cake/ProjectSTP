using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectSTP.Utilities
{
    static class RegularExpressions
    {
        public static bool IsValidName(string name)
        {
            string pattern = @"^[A-Za-zА-Яа-я]+$"; // Проверка на наличие только букв

            return Regex.IsMatch(name, pattern);
        }

        public static bool IsValidInteger(string input)
        {
            if (int.TryParse(input, out int number))
            {
                if (number >= int.MinValue && number <= int.MaxValue)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidClientName(string input)
        {
            // Физическое лицо
            string pattern1 = @"ИП\s[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+\s?[А-ЯЁ][а-яё]*";

            // Юр лицо
            string pattern2 = @"[А-ЯЁ]+\s""[А-ЯЁ][а-яё\s]*""";

            
            if (Regex.IsMatch(input, pattern1))
            {
                return true;
            }

            if (Regex.IsMatch(input, pattern2))
            {
                return true;
            }

            return false;
        }
        public static bool IsValidDecimal(string input)
        {
            decimal result;
            return decimal.TryParse(input, out result);
        }
    }
}
