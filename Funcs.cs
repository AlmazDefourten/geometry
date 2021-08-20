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
        protected string simplifyingTheFunctions(string input)
        {
            input = input.Replace("pi", Math.PI.ToString());
            input = input.Replace("cos", "c");
            input = input.Replace("sin", "s");
            input = input.Replace("tg", "t");
            input = input.Replace("ctg", "g");
            return input;
        }
        //упрощение функций и pi(для явного вычисления с pi, потом добавлю еще одно вычисление)
    }
}
