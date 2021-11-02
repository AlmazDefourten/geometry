using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry
{
    class Funcs : PrioritiesOper
    {
        protected static bool isFuncOperator(char input)
        {
            if (input == 's' || input == 'c' || input == 't' || input == 'g')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //проверяет является ли символ триг функцией
        protected string simplifyingTheFunctions(string input, bool isExplicitCalc = true)
        {
            input = input.Replace("pi", "p");
            switch (isExplicitCalc)
            {
                case (true):
                    input = input.Replace("p", Math.PI.ToString());
                    break;
                case (false):

                    break;
            }
            input = input.Replace("cos", "c");
            input = input.Replace("sin", "s");
            input = input.Replace("tg", "t");
            input = input.Replace("ctg", "g");
            return input;
        }
        
        //упрощение функций и дробей с pi and x
    }
}
