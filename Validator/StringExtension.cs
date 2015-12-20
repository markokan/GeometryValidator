using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil
{
    public static class StringExtension
    {
        public static string RemoveExtraWhiteSpace(this string input)
        {
            StringBuilder builder = new StringBuilder();

            char[] chars = input.ToCharArray();

            bool lastWasEmpty = false;

            for (int i = 0; i < chars.Length; i++)
            {
                if (lastWasEmpty == false || chars[i] != ' ')
                {
                    builder.Append(chars[i]);
                }

                if (chars[i] == ' ') lastWasEmpty = true;
                else lastWasEmpty = false;

            }

            return builder.ToString();
        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
