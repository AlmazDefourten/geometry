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
            funcArgument,
            brackets,
            exponentiation,
            dividingAndMultiplication,
            minusAndPlus,
            number,
            errorPriority,
            maxPriority
        }
        protected Priorities getPriority(char input, bool binary = true)
        {
            if (input == 'c' || input == 's' || input == 't' || input == 'g') { return Priorities.funcArgument; }// g - котангенс
            else if (input == '^') { return Priorities.exponentiation; }
            else if (input == '(') { return Priorities.brackets; }
            else if (input == '*' || input == '/') { return Priorities.dividingAndMultiplication; }
            else if (input == '+' || ((input == '-') && (binary == true))) { return Priorities.minusAndPlus; }
            else if (isNumber(input)) { return Priorities.number; }

            return Priorities.errorPriority;
        }
        protected Priorities getBestPriority(string input, bool isBinary = true)
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

    }
}
