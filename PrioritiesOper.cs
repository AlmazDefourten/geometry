using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geometry
{
    
    class PrioritiesOper : HelpFuncs
    {
        public enum Priorities
        {
            brackets,
            funcArgument,
            exponentiation,
            dividing,
            multiplication,
            minusAndPlus,
            number,
            errorPriority,
            maxPriority
        }
        //перечислитель для приоритетов
        protected Priorities getPriority(char input, bool binary = true)
        {
            if (input == 'c' || input == 's' || input == 't' || input == 'g') { return Priorities.funcArgument; }// g - котангенс
            else if (input == '^') { return Priorities.exponentiation; }
            else if (input == '(') { return Priorities.brackets; }
            else if (input == '/') { return Priorities.dividing; }
            else if (input == '*') { return Priorities.multiplication; }
            else if (input == '+' || ((input == '-') && (binary == true))) { return Priorities.minusAndPlus; }
            else if (isNumber(input)) { return Priorities.number; }

            return Priorities.errorPriority;
        }
        //вычисляет уровень приоритета символа-оператора
        protected Priorities getBestPriority(ref string input, bool isBinary = true)
        {
            Priorities bestPriority = Priorities.maxPriority;
            for (int i = 0; i < input.Length; i++)
            {
                Priorities priority;
                priority = getPriority(input[i], isBinary);
                if (priority < bestPriority) { bestPriority = priority; }
            }
            return bestPriority;
        }
        //вычисляет наивысший уровень приоритета в строке
    }
}
