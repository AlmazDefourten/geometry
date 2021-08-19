using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry
{
    class HelpFuncs
    {
        protected static bool isNumber(char input)
        {
            if (input == '0' || input == '1' || input == '2' || input == '3' || input == '4' || input == '5'
                || input == '6' || input == '7' || input == '8' || input == '9' || input == ',') { return true; }
            else { return false; }
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        protected static bool isOperator(char input)
        {
            if (input == '*' || input == '/' || input == '-' || input == '+' || input == '^')
            {
                return true;
            }
            return false;
        }
    }
}
