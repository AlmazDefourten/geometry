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
        private string getLeftOperand(ref string input, int indexOfOperand)
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
        private string getRightOperand(ref string input, int indexOfOperand)
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
        string dividingFractions(string firstFraction, string secondFraction)
        {
            Debug.WriteLine(firstFraction + " firstFrac");
            Debug.WriteLine(secondFraction + " secondFrac");
            firstFraction = "q" + firstFraction + "q";
            secondFraction = "q" + secondFraction + "q";
            string firstNumerator = "", secondNumerator = "", firstDenominator = "", secondDenominator = "";
            int firstIndexOfDividingFirst = getFirstIndexOfDividing(ref firstFraction);
            int firstIndexOfDividingSecond = getFirstIndexOfDividing(ref secondFraction);
            if (firstIndexOfDividingFirst > 0 && firstIndexOfDividingSecond > 0)
            {
                firstNumerator = getLeftOperand(ref firstFraction, firstIndexOfDividingFirst);
                firstDenominator = getRightOperand(ref firstFraction, firstIndexOfDividingFirst);
                secondNumerator = getLeftOperand(ref secondFraction, firstIndexOfDividingSecond);
                secondDenominator = getRightOperand(ref secondFraction, firstIndexOfDividingSecond);
            }
            else if (firstIndexOfDividingFirst > 0 && firstIndexOfDividingSecond < 0)
            {
                firstNumerator = getLeftOperand(ref firstFraction, firstIndexOfDividingFirst);
                firstDenominator = getRightOperand(ref firstFraction, firstIndexOfDividingFirst);
                secondFraction = secondFraction.Replace("q", "");
                secondNumerator = secondFraction;
                secondDenominator = "1";
            }
            else if (firstIndexOfDividingFirst < 0 && firstIndexOfDividingSecond > 0)
            {
                firstDenominator = "1";
                firstFraction = firstFraction.Replace("q", "");
                firstNumerator = firstFraction;
                secondNumerator = getLeftOperand(ref secondFraction, firstIndexOfDividingSecond);
                secondDenominator = getRightOperand(ref secondFraction, firstIndexOfDividingSecond);
            }
            else
            {
                Debug.WriteLine("Error! in dividingFractions func.");
            }
            string resultNumerator = (Convert.ToInt32(firstNumerator) * Convert.ToInt32(secondDenominator)).ToString();
            string resultDenominator = (Convert.ToInt32(firstDenominator) * Convert.ToInt32(secondNumerator)).ToString();
            Debug.WriteLine(firstFraction + " firstFrac in end");
            Debug.WriteLine(secondFraction + " secondFrac in end");
            return resultNumerator + "/" + resultDenominator;
        }
        string multiplyingFractions(string firstFraction, string secondFraction)
        {
            string firstNumerator = "", secondNumerator = "", firstDenominator = "", secondDenominator = "";
            firstFraction = "q" + firstFraction + "q";
            secondFraction = "q" + secondFraction + "q";
            int firstIndexOfDividingFirst = getFirstIndexOfDividing(ref firstFraction);
            int firstIndexOfDividingSecond = getFirstIndexOfDividing(ref secondFraction);
            if (firstIndexOfDividingFirst > 0 && firstIndexOfDividingSecond > 0)
            {
                firstNumerator = getLeftOperand(ref firstFraction, firstIndexOfDividingFirst);
                firstDenominator = getRightOperand(ref firstFraction, firstIndexOfDividingFirst);
                secondNumerator = getLeftOperand(ref secondFraction, firstIndexOfDividingSecond);
                secondDenominator = getRightOperand(ref secondFraction, firstIndexOfDividingSecond);
            }
            else if (firstIndexOfDividingFirst > 0 && firstIndexOfDividingSecond < 0)
            {
                firstNumerator = getLeftOperand(ref firstFraction, firstIndexOfDividingFirst);
                firstDenominator = getRightOperand(ref firstFraction, firstIndexOfDividingFirst);
                secondFraction = secondFraction.Replace("q", "");
                secondNumerator = secondFraction;
                secondDenominator = "1";
            }
            else if (firstIndexOfDividingFirst < 0 && firstIndexOfDividingSecond > 0)
            {
                firstDenominator = "1";
                firstFraction = firstFraction.Replace("q", "");
                firstNumerator = firstFraction;
                secondNumerator = getLeftOperand(ref secondFraction, firstIndexOfDividingSecond);
                secondDenominator = getRightOperand(ref secondFraction, firstIndexOfDividingSecond);
            }
            else
            {
                Debug.WriteLine("Error! in multiolyingFractions func.");
            }
            string resultNumerator = (Convert.ToInt32(firstNumerator) * Convert.ToInt32(secondNumerator)).ToString();
            string resultDenomerator = (Convert.ToInt32(firstDenominator) * Convert.ToInt32(secondDenominator)).ToString();
            return resultNumerator + "/" + resultDenomerator;
        }
        //возвращает правое от оператора число в входящей строке
        public string calculation(Priorities bestPriority, string input, bool isExplicit = true)
        {
            bool isInputHaveOperatorChars = true;
            while (isInputHaveOperatorChars)
            {
                input = input.Insert(0, "q");
                input = input + "q";
                input = simplifyingTheFunctions(input);
                while (bestPriority == Priorities.brackets)
                {
                    int indexOfFirstBracket = input.LastIndexOf('(');
                    int indexOfSecondBracket = input.LastIndexOf(")");
                    string newString = input.Substring(indexOfFirstBracket + 1, indexOfSecondBracket - indexOfFirstBracket - 1);
                    string znachenie = calculation(getBestPriority(ref newString), newString, false);
                    input = input.Remove(indexOfFirstBracket, indexOfSecondBracket - indexOfFirstBracket + 1);
                    input = input.Insert(indexOfFirstBracket, znachenie.ToString());
                    bestPriority = getBestPriority(ref input);
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
                            rightOperand = Convert.ToDouble(getRightOperand(ref input, i));
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
                    bestPriority = getBestPriority(ref input);
                }
                while (bestPriority == Priorities.exponentiation)
                {
                    int inpLen = input.Length;
                    char operat = ' ';

                    for (int i = 0; i < inpLen; i++)
                    {
                        if (input[i] == '^')
                        {
                            string leftOp = getLeftOperand(ref input, i);
                            string rightOp = getRightOperand(ref input, i);
                            if (getFirstIndexOfDividing(ref leftOp) > 0 && getFirstIndexOfDividing(ref rightOp) < 0)
                            {
                                Debug.WriteLine(leftOp + " - leftOp");
                                Debug.WriteLine(rightOp + " - rightOp");
                                leftOp = "q" + leftOp + "q";
                                rightOp = "q" + rightOp + "q";
                                int indexOfDividing = getFirstIndexOfDividing(ref leftOp);
                                string numeratorOfFraction = getLeftOperand(ref leftOp, indexOfDividing);
                                string denominatorOfFraction = getRightOperand(ref leftOp, indexOfDividing);
                                rightOp = getRightOperand(ref input, i);
                                Debug.WriteLine(denominatorOfFraction + " denominatorOfFraction");
                                string numeratorOfResultFraction = (Math.Pow(Convert.ToInt32(numeratorOfFraction), Convert.ToInt32(rightOp.Replace("q", "")))).ToString();
                                string denominatorOfResultFraction = (Math.Pow(Convert.ToInt32(denominatorOfFraction), Convert.ToInt32(rightOp.Replace("q", "")))).ToString();
                                Debug.WriteLine(denominatorOfResultFraction + " deno of resultFraction");
                                string result = numeratorOfResultFraction + "/" + denominatorOfResultFraction;
                                input = input.Remove(startIndex, endIndex - startIndex + 1);
                                input = input.Insert(startIndex, result.ToString());
                                inpLen = input.Length;
                            }
                            else
                            {
                                operat = input[i];
                                double leftOperand = Convert.ToDouble(leftOp);
                                double rightOperand = Convert.ToDouble(rightOp);
                                double result = Math.Pow(leftOperand, rightOperand);
                                input = input.Remove(startIndex, endIndex - startIndex + 1);
                                input = input.Insert(startIndex, result.ToString());
                                inpLen = input.Length; //dd
                            }
                        }
                    }
                    bestPriority = getBestPriority(ref input);
                }
                while (bestPriority == Priorities.dividing)
                {
                    char operat = 'q';
                    int inpLen = input.Length;
                    for (int i = 0; i < inpLen; i++)
                    {
                        if (isOperator(input[i]))
                        {
                            operat = input[i];
                            string leftOp = getLeftOperand(ref input, i);
                            string rightOp = getRightOperand(ref input, i);
                            double dblResult = -1;
                            double leftOperand = 999, rightOperand = 999;
                            if (operat == '/')
                            {
                                if (getFirstIndexOfDividing(ref leftOp) > 0 || getFirstIndexOfDividing(ref rightOp) > 0)
                                {
                                    string strResult = dividingFractions(leftOp, rightOp);
                                    leftOp = getLeftOperand(ref input, i);  // do nothing, only changes a startIndex value
                                    rightOp = getRightOperand(ref input, i); // do nothing, only changes a endIndex value
                                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                                    input = input.Insert(startIndex, strResult.ToString());
                                }
                                else
                                {
                                    leftOperand = Convert.ToDouble(leftOp);
                                    rightOperand = Convert.ToDouble(rightOp);
                                    int thisGcd = gcd(Convert.ToInt32(leftOperand), Convert.ToInt32(rightOperand));
                                    if (isExplicit == true || thisGcd % rightOperand == 0)
                                    {
                                        dblResult = leftOperand / rightOperand;
                                        input = input.Remove(startIndex, endIndex - startIndex + 1);
                                        input = input.Insert(startIndex, dblResult.ToString());
                                    }
                                    else
                                    {
                                        input = input.Remove(i, 1);
                                        input = input.Insert(i, "b");
                                        leftOperand = Convert.ToDouble(leftOp);
                                        rightOperand = Convert.ToDouble(rightOp);
                                        string res = (leftOperand / thisGcd).ToString() + "b" + (rightOperand / thisGcd).ToString();
                                        input = input.Remove(startIndex, endIndex - startIndex + 1);
                                        input = input.Insert(startIndex, res.ToString());
                                    }
                                }
                            }
                            inpLen = input.Length;
                        }
                    }

                    bestPriority = getBestPriority(ref input);
                }
                while (bestPriority == Priorities.multiplication)
                {
                    char operat = 'q';
                    int inpLen = input.Length;
                    for (int i = 0; i < inpLen; i++)
                    {
                        if (isOperator(input[i]))
                        {
                            operat = input[i];
                            string leftOp = getLeftOperand(ref input, i);
                            string rightOp = getRightOperand(ref input, i);
                            string strResult = "";
                            double dblResult = -1;
                            double leftOperand = 999, rightOperand = 999;
                            if (operat == '*')
                            {
                                if (getFirstIndexOfDividing(ref leftOp) > 0 || getFirstIndexOfDividing(ref rightOp) > 0)
                                {
                                    strResult = multiplyingFractions(leftOp, rightOp);
                                    leftOp = getLeftOperand(ref input, i);  // do nothing, only changes a startIndex value
                                    rightOp = getRightOperand(ref input, i); // do nothing, only changes a endIndex value
                                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                                    input = input.Insert(startIndex, strResult.ToString());
                                }
                                else
                                {
                                    leftOperand = Convert.ToDouble(leftOp);
                                    rightOperand = Convert.ToDouble(rightOp);
                                    dblResult = leftOperand * rightOperand;
                                    input = input.Remove(startIndex, endIndex - startIndex + 1);
                                    input = input.Insert(startIndex, dblResult.ToString());
                                }
                            }
                            inpLen = input.Length;
                        }
                    }

                    bestPriority = getBestPriority(ref input);
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
                            double leftOperand = Convert.ToDouble(getLeftOperand(ref input, i));
                            double rightOperand = Convert.ToDouble(getRightOperand(ref input, i));
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
                    bestPriority = getBestPriority(ref input, isBinary);
                }
                if (bestPriority == Priorities.number)
                {
                    input = input.Replace("q", "");
                    return input;
                }
                isInputHaveOperatorChars = isStrHaveOperators(ref input);
            }
            return "error";
        }
        //вычисления, логика: по приоритетам вычисляются выражения, отталкиваясь от операторов, скобки вычисляются рекурентно
        public string matchDecide()
        {
            Priorities bestPriority = getBestPriority(ref m_functionInput);
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
