using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class InfixToPostfix
    {
        public List<string[]> PostFix;
        public InfixToPostfix()
        {
            PostFix = new List<string[]>();
        }

        // The main method that converts given infix expression
        // to postfix expression.
        public void Convert(List<string[]> infixList)
        {
            foreach (string[] infixToken in infixList)
            {
                bool isAdded = false;
                string infix = infixToken[1];
                // Initializing empty String for result
                string expression = "";

                // Initializing empty stack
                Stack<char> operatorStack = new Stack<char>();

                for (int i = 0; i < infix.Length; ++i)
                {
                    // Operands  ( 1,2,3,... )
                    // Operators ( +,-,*,/ )
                    char currentChar = infix[i];

                    bool isOperand = char.IsDigit(currentChar);

                    if (isOperand) // If the scanned character is an operand
                    {
                        expression += currentChar; // put it in the postfix expression
                    }
                    else if (IsOperator(currentChar))
                    {
                        while (operatorStack.Count > 0 && GetPrecedence(currentChar) <= GetPrecedence(operatorStack.Peek()))
                        {
                            expression += operatorStack.Pop();
                        }
                        operatorStack.Push(currentChar);
                    }
                    else if (currentChar == '(') // If the scanned character is a '('
                    {
                        operatorStack.Push(currentChar); // push it to the stack.
                    }
                    else if (currentChar == ')') // If the scanned character is a ')'
                    {
                        char nextChar = operatorStack.Peek();
                        while (operatorStack.Count != 0 && nextChar != '(')
                        {
                            expression += operatorStack.Pop();
                            nextChar = operatorStack.Peek();
                        }

                        if (operatorStack.Count != 0 && operatorStack.Peek() != '(')
                        {
                            PostFix.Add(new string[] { infixToken[0], "Mismatched parentheses" });
                            isAdded = true;
                        }
                        else
                        {
                            operatorStack.Pop();
                        }
                    }
                    else
                    {
                        PostFix.Add(new string[] { infixToken[0], "Invalid Expression" });
                        isAdded = true;
                    }
                }

                // Pop all the operators from the stack
                while (operatorStack.Count != 0)
                {
                    expression += operatorStack.Pop();
                }

                if (!isAdded)
                {
                    PostFix.Add(new string[] { infixToken[0], expression });
                }
            }
        }
        //  a private method that takes an operator character
        //  as input and returns its precedence value as an integer.
        private int GetPrecedence(char oprtr)
        {
            switch (oprtr)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                default: return -1;
            }
        }
        //  private method that takes a character as input and returns a boolean value
        //  indicating whether the character is an operator or not.
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }
    }
}
