using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace geometry
{
    class ParsingAndCalculation : Funcs
    {        
        public string m_functionInput;
        private int startIndex = 0;
        private int endIndex = 0;
        public ParsingAndCalculation(string functionInput) { m_functionInput = functionInput; }
        private string getLeftOperand(string input, int indexOfOperand)
        {
            indexOfOperand--;
            string operand = "";
            char perem = input[indexOfOperand];
            while (isNumber(perem))
            {
                operand += perem;
                startIndex = indexOfOperand;
                indexOfOperand--;
                perem = input[indexOfOperand];
                
            }
            if (perem == '-') { 
                operand += "-";
                startIndex = indexOfOperand;
            }
            operand = ReverseString(operand);
            return operand;
        }
        //возвращает левое от оператора число в входящей строке
        private string getRightOperand(string input, int indexOfOperand)
        {
            indexOfOperand++;
            string operand = "";
            if (input[indexOfOperand] == '-') 
            {
                endIndex = indexOfOperand;
                operand += "-";
                indexOfOperand++;
            }
            
            char perem = input[indexOfOperand];
            while (isNumber(perem))
            {
                operand += perem;
                endIndex = indexOfOperand;
                indexOfOperand++;
                perem = input[indexOfOperand];
            }
            return operand;
        }
        //возвращает правое от оператора число в входящей строке
        public string calculation(Priorities bestPriority, string input, bool isExplicit = true)
        {
            input = input.Insert(0, "q");
            input = input + "q";
            input = simplifyingTheFunctions(input);
            while (bestPriority == Priorities.brackets)
            {
                int indexOfFirstBracket = input.LastIndexOf('(');
                int indexOfSecondBracket = input.LastIndexOf(")");
                string newString = input.Substring(indexOfFirstBracket + 1, indexOfSecondBracket - indexOfFirstBracket - 1);
                string znachenie = calculation(getBestPriority(newString), newString);
                input = input.Remove(indexOfFirstBracket, indexOfSecondBracket - indexOfFirstBracket + 1);
                input = input.Insert(indexOfFirstBracket, znachenie.ToString());
                bestPriority = getBestPriority(input);
            }
            while (bestPriority == Priorities.funcArgument)
            {
                int inpLen = input.Length;
                for (int i = 0; i < inpLen; i++)
                {
                    if (isFuncOperator(input[i]))
                    {
                        startIndex = i;
                        double rightOperand;
                        char oper = ' ';
                        double result = 0;
                        rightOperand = Convert.ToDouble(getRightOperand(input, i));
                        oper = input[i];
                        if (oper == 's') { result = Math.Sin(rightOperand); }
                        else if (oper == 'c') { result = Math.Cos(rightOperand); }
                        else if (oper == 't') { result = Math.Tan(rightOperand); }
                        else if (oper == 'g') { result = 1 / Math.Tan(rightOperand); }
                        input = input.Remove(startIndex, endIndex - startIndex + 1);
                        input = input.Insert(startIndex, result.ToString());
                        inpLen = input.Length;
                    }
                }
                bestPriority = getBestPriority(input);
            }
            while (bestPriority == Priorities.exponentiation)
            {
                int inpLen = input.Length;
                char operat = ' ';
                for (int i = 0; i < inpLen; i++)
                {
                    if(input[i] == '^')
                    {
                        operat = input[i];
                        double leftOperand = Convert.ToDouble(getLeftOperand(input, i));
                        double rightOperand = Convert.ToDouble(getRightOperand(input, i));
                        double result = Math.Pow(leftOperand, rightOperand);
                        input = input.Remove(startIndex, endIndex - startIndex + 1);
                        input = input.Insert(startIndex, result.ToString());
                        inpLen = input.Length;
                    }
                }
                bestPriority = getBestPriority(input);
            }
            while (bestPriority == Priorities.dividingAndMultiplication)
            {
                
                char operat = 'q';
                int inpLen = input.Length;
                for (int i = 0; i < inpLen; i++)
                {
                    if(isOperator(input[i]))
                    {
                        operat = input[i];
                        
                        double leftOperand = Convert.ToDouble(getLeftOperand(input, i));
                        double rightOperand = Convert.ToDouble(getRightOperand(input, i));
                        if (operat == '*')
                        {
                            double result = leftOperand * rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        else if (operat == '/') {
                            double result = 0;
                            int thisGcd = gcd(Convert.ToInt32(leftOperand), Convert.ToInt32(rightOperand));
                            if (isExplicit == false)
                            {
                                if (thisGcd == rightOperand || isExplicit == true)
                                {
                                    result = leftOperand / rightOperand;
                                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                                    input = input.Insert(startIndex, result.ToString());
                                }
                                else
                                {
                                    input = input.Remove(i, 1);
                                    input = input.Insert(i, "b");
                                    string res = leftOperand/thisGcd + "b" + rightOperand/thisGcd;
                                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                                    input = input.Insert(startIndex, res.ToString());
                                }
                            }
                        }
                        inpLen = input.Length;
                    }
                }
                
                bestPriority = getBestPriority(input);
            }
            while (bestPriority == Priorities.minusAndPlus)
            {
                bool isBinary = true;
                char operat = ' ';
                int inpLen = input.Length;
                for (int i = 0; i < inpLen; i++)
                {
                    if (isOperator(input[i]) && (isNumber(input[i - 1])))
                    {
                        operat = input[i];
                        if (input[i - 1] == 'q')
                        {
                            break;
                        }
                        double leftOperand = Convert.ToDouble(getLeftOperand(input, i));
                        double rightOperand = Convert.ToDouble(getRightOperand(input, i));
                        if (operat == '+')
                        {
                            double result = leftOperand + rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        else if (operat == '-')
                        {
                            double result;
                            result = leftOperand - rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        i = startIndex - 1;
                        inpLen = input.Length;
                    }
                    else
                    {
                        isBinary = false;
                    }
                }
                

                
                bestPriority = getBestPriority(input, isBinary);
            }
            if (bestPriority == Priorities.number)
            {
                input = input.Replace("b", "/");
                input = input.Replace("q", "");
                return input;
            }
            return "error, end of while CALCULATION";
        }
        //вычисления, логика: по приоритетам вычисляются выражения, отталкиваясь от операторов, скобки вычисляются рекурентно
        public string matchDecide()
        {
            Priorities bestPriority = getBestPriority(m_functionInput);
            int y;
            string perem = m_functionInput;
            string result = calculation(bestPriority, perem, false);
            return result;
        }
        //я ебу нахуй ты это сунул
        public double getY(double x)
        {
            //пока что возвращает просто значение вычислений в дальнейшем будем выдавать решения, работа с неравенствами в другом классе
            //I don't understand chto делать, т.к. рклизовывать на калькулятор график функции? - не трогай просто работай с этим из другого класса где ты рисуешь
            return Convert.ToDouble(matchDecide());
        }
    }
}
