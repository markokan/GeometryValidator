﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil
{
    public static class StackExtension
    {
        public static string GetStack(this List<char> chars)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in chars)
            {
                str.Append(item);
            }

            return str.ToString();
        }

        public static string Generate(this string msg, int amount)
        {
            StringBuilder ge = new StringBuilder();
            for (int i = 0; i <= amount; i++)
                ge.Append(msg);

            return ge.ToString();
        }
    }
}
