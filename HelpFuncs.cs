using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry
{
    class HelpFuncs
    {
        protected int gcd(int a, int b)
        {
            while (a > 0 && b > 0)
            {
                if (a > b) { a = a % b; }
                else { b = b % a; }
            }
            return (a + b);
        }
        //нахождение нод по алоритму евклида
        protected static bool isNumber(char input)
        {
            if (input == '0' || input == '1' || input == '2' || input == '3' || input == '4' || input == '5'
                || input == '6' || input == '7' || input == '8' || input == '9' || input == ',' || input == 'b') { return true; }
            else { return false; }
        }
        //проверяет, является ли данный символ цифрой или для дабл ','
        protected int getFirstIndexOfDividing(ref string input)
        {
            int inpLen = input.Length;
            for (int i = 0; i < inpLen; i++)
            {
                if(input[i] == 'b')
                {
                    return i;
                }
            }
            return -1;
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        //разворачивает строку
        protected static bool isOperator(char input)
        {
            if (input == '*' || input == '/' || input == '-' || input == '+' || input == '^')
            {
                return true;
            }
            return false;
        }
        //проверяет, является ли данный символ оператором
        /*void simplifyingTheFraction(ref string input)
        {
            int inpLen = input.Length;
            for (int i = 0; i < inpLen; i++)
            {
                if (input[i] == '/')
            }
        }
        */
    }
}
