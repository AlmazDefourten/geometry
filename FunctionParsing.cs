﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace geometry
{
    class FunctionParsing
    {        
        public string m_functionInput;
        private int startIndex = 0;
        private int endIndex = 0;

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
        public FunctionParsing(string functionInput) { m_functionInput = functionInput; }

        private static bool isNumber(char input)
        {
            if (input == '0' || input == '1' || input == '2' || input == '3' || input == '4' || input == '5'
                || input == '6' || input == '7' || input == '8' || input == '9') { return true; }
            else { return false; }
        }
        private Priorities getPriority(char input)
        {
            if (input == '^') { return Priorities.funcArgument; }
            else if (input == '(') { return Priorities.brackets; }
            else if (input == '*' || input == '/') { return Priorities.dividingAndMultiplication; }
            else if (input == '+' || input == '-') { return Priorities.minusAndPlus; }
            else if (isNumber(input)) { return Priorities.number; }
            return Priorities.errorPriority;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private Priorities getBestPriority(string input)
        {
            Priorities bestPriority = Priorities.maxPriority;
            for (int i = 0; i < input.Length; i++)
            {
                Priorities priority;
                priority = getPriority(input[i]);
                if (priority < bestPriority) { bestPriority = priority; }
            }
            return bestPriority;
        }
        
        private bool isOperator(char input)
        {
            if (input == '*' || input == '/' || input == '-' || input == '+')
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
            if (perem == '-') { operand += "-"; }
            operand = ReverseString(operand);
            return operand;
        }

        private string getRightOperand(string input, int indexOfOperand)
        {
            indexOfOperand++;
            string operand = "";
            if (input[indexOfOperand] == '-') 
            { 
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

        public int calculation(Priorities bestPriority, string input)
        {
            // надо заменить скобки аргументов ф-й на [ и ] а так же поменять кодовыми буквами sin = s, cos = c и тд
            ///для чего меня скобки?
            input = input.Insert(0, "q");
            input = input + "q";
            while (bestPriority == Priorities.brackets)
            {
                int indexOfFirstBracket = input.LastIndexOf('(');
                int indexOfSecondBracket = input.IndexOf(")");
                string newString = input.Substring(indexOfFirstBracket + 1, indexOfSecondBracket - indexOfFirstBracket - 1);
                int znachenie = calculation(getBestPriority(newString), newString);
                input = input.Remove(indexOfFirstBracket, indexOfSecondBracket - indexOfFirstBracket + 1);
                input = input.Insert(indexOfFirstBracket, znachenie.ToString());
                Debug.WriteLine(input);
                bestPriority = getBestPriority(input);
            }
            while(bestPriority == Priorities.dividingAndMultiplication)
            {
                
                char operat;
                int inpLen = input.Length;
                for (int i = 0; i < inpLen; i++)
                {
                    if(isOperator(input[i]))
                    {
                        operat = input[i];
                        
                        int leftOperand = Convert.ToInt32(getLeftOperand(input, i));
                        int rightOperand = Convert.ToInt32(getRightOperand(input, i));
                        if (operat == '*')
                        {
                            int result = leftOperand * rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        else if (operat == '/') {
                            int result;
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
                char operat;
                int inpLen = input.Length;
                for (int i = 0; i < inpLen; i++)
                {
                    if (isOperator(input[i]))
                    {
                        operat = input[i];

                        int leftOperand = Convert.ToInt32(getLeftOperand(input, i));
                        int rightOperand = Convert.ToInt32(getRightOperand(input, i));
                        if (operat == '+')
                        {
                            int result = leftOperand + rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        else if (operat == '-')
                        {
                            int result;
                            result = leftOperand - rightOperand;
                            input = input.Remove(startIndex, endIndex - startIndex + 1);
                            input = input.Insert(startIndex, result.ToString());
                        }
                        i = startIndex - 1;
                        inpLen = input.Length;
                    }
                }
                bestPriority = getBestPriority(input);
            }
            if (bestPriority == Priorities.number)
            {
                input = input.Replace("q", "");
                return Convert.ToInt32(input);
            }
            return 0;
        }
        int matchDecide()
        {
            Priorities bestPriority = getBestPriority(m_functionInput);
            int y;
            string perem = m_functionInput;
            int result = calculation(bestPriority, perem);
            return result;
        }

        public int getY(int x)
        {
            //пока что возвращает просто значение вычислений в дальнейшем будем выдавать решения, работа с неравенствами в другом классе
            //I don't understand chto делать, т.к. рклизовывать на калькулятор график функции? - не трогай просто работай с этим из другого класса где ты рисуешь
            return matchDecide();
        }
    }
}
