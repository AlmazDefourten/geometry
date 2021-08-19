﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//проверка коммит
//сосать
namespace geometry
{
    class FunctionParsing : Funcs
    {        
        public string m_functionInput;
        private int startIndex = 0;
        private int endIndex = 0;

        
        public FunctionParsing(string functionInput) { m_functionInput = functionInput; }

        
        private static bool isNumber(char input)
        {
            if (input == '0' || input == '1' || input == '2' || input == '3' || input == '4' || input == '5'
                || input == '6' || input == '7' || input == '8' || input == '9' || input == '.') { return true; }
            else { return false; }
        }
        private Priorities getPriority(char input, bool binary = true)
        {
            if (input == 'c' || input == 's' || input == 't' || input == 'g') { return Priorities.funcArgument; }// g - котангенс
            else if (input == '^') { return Priorities.exponentiation; }
            else if (input == '(') { return Priorities.brackets; }
            else if (input == '*' || input == '/') { return Priorities.dividingAndMultiplication; }
            else if (input == '+' || ((input == '-') && (binary == true))) { return Priorities.minusAndPlus; }
            else if (isNumber(input)) { return Priorities.number; }
            
            return Priorities.errorPriority;
        }
        private Priorities getBestPriority(string input, bool isBinary = true)
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

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        
        private bool isOperator(char input)
        {
            if (input == '*' || input == '/' || input == '-' || input == '+' || input == '^')
            {
                return true;
            }
            return false;
        }

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

        public double calculation(Priorities bestPriority, string input)
        {
            // надо заменить скобки аргументов ф-й на [ и ] а так же поменять кодовыми буквами sin = s, cos = c и тд
            ///для чего меня скобки?
            input = input.Insert(0, "q");
            input = input + "q";
            while (bestPriority == Priorities.funcArgument)
            {

            }
            while (bestPriority == Priorities.brackets)
            {
                Debug.WriteLine(input + " q");
                int indexOfFirstBracket = input.LastIndexOf('(');
                int indexOfSecondBracket = input.LastIndexOf(")");
                string newString = input.Substring(indexOfFirstBracket + 1, indexOfSecondBracket - indexOfFirstBracket - 1);
                double znachenie = calculation(getBestPriority(newString), newString);
                input = input.Remove(indexOfFirstBracket, indexOfSecondBracket - indexOfFirstBracket + 1);
                input = input.Insert(indexOfFirstBracket, znachenie.ToString());
                bestPriority = getBestPriority(input);
                Debug.WriteLine(input + " в");
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
                        Debug.WriteLine(input + "o");
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
                            double result;
                                result = leftOperand / rightOperand;
                                input = input.Remove(startIndex, endIndex - startIndex + 1);
                                input = input.Insert(startIndex, result.ToString());
                                
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
                
                input = input.Replace("q", "");
                return Convert.ToDouble(input);
            }
            return 0;
        }
        double matchDecide()
        {
            Priorities bestPriority = getBestPriority(m_functionInput);
            int y;
            string perem = m_functionInput;
            double result = calculation(bestPriority, perem);
            return result;
        }

        public double getY(double x)
        {
            //пока что возвращает просто значение вычислений в дальнейшем будем выдавать решения, работа с неравенствами в другом классе
            //I don't understand chto делать, т.к. рклизовывать на калькулятор график функции? - не трогай просто работай с этим из другого класса где ты рисуешь
            return matchDecide();
        }
    }
}
