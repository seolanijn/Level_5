using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ExpressionEvaluation
    {
        // Method to evaluate a prefix expression using an expression tree
        public string EvaluatePrefix(string expression)
        {
            // Build the expression tree
            ExpressionTreeNode root = PrefixBuildExpressionTree(expression);

            // Evaluate the expression tree and return the result
            return Evaluate(root, true);
        }

        // Method to evaluate a postfix expression using an expression tree
        public string EvaluatePostfix(string expression)
        {
            // Build the expression tree
            ExpressionTreeNode root = PostfixBuildExpressionTree(expression);

            // Evaluate the expression tree and return the result
            return Evaluate(root, false);
        }

        private string Evaluate(ExpressionTreeNode root, bool isPrefix)
        {
            if (isPrefix)
                return EvaluatePreExpressionTree(root).ToString();
            else
                return EvaluatePostExpressionTree(root).ToString();
        }


        // Method to build an expression tree from a prefix or postfix expression
        private ExpressionTreeNode PrefixBuildExpressionTree(string expression)
        {
            Stack<ExpressionTreeNode> stack = new Stack<ExpressionTreeNode>();

            // Scan all characters one by one
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                char c = expression[i];

                // If the scanned character is an operand (number here), create a leaf node and push it to the stack
                if (char.IsDigit(c))
                {
                    int multiplier = 1;
                    while (i >= 0 && char.IsDigit(expression[i]))
                    {
                        int n = 0;
                        n = n + ((int)(expression[i] - '0')) * multiplier;
                        i--;
                        ExpressionTreeNode leafNode = new ExpressionTreeNode(n);
                        stack.Push(leafNode);
                    }
                    i++;
                }

                // If the scanned character is an operator, create an internal node and pop two elements from the stack to set as children
                else
                {
                    ExpressionTreeNode internalNode = new ExpressionTreeNode(c);
                    internalNode.Left = stack.Pop();
                    internalNode.Right = stack.Pop();
                    stack.Push(internalNode);
                }
            }

            // The expression tree is complete when there's only one node left in the stack
            return stack.Pop();
        }

        // Method to build an expression tree from a prefix or postfix expression
        private ExpressionTreeNode PostfixBuildExpressionTree(string expression)
        {
            Stack<ExpressionTreeNode> stack = new Stack<ExpressionTreeNode>();

            // Scan all characters one by one
            for (int i = 0; i <= expression.Length - 1; i++)
            {
                char c = expression[i];

                // If the scanned character is an operand (number here), create a leaf node and push it to the stack
                if (char.IsDigit(c))
                {
                    string s = c.ToString();
                    double d = double.Parse(s);
                    ExpressionTreeNode leafNode = new ExpressionTreeNode(d);
                    stack.Push(leafNode);
                }

                // If the scanned character is an operator, create an internal node and pop two elements from the stack to set as children
                else
                {
                    ExpressionTreeNode internalNode = new ExpressionTreeNode(c);
                    internalNode.Left = stack.Pop();
                    internalNode.Right = stack.Pop();
                    stack.Push(internalNode);
                }
            }

            // The expression tree is complete when there's only one node left in the stack
            return stack.Pop();
        }

        // Method to evaluate an Postfix expression tree
        private double EvaluatePostExpressionTree(ExpressionTreeNode root)
        {
            if (root.IsLeafNode())
            {
                return root.Value;
            }
            else
            {
                double leftValue = EvaluatePostExpressionTree(root.Left);
                double rightValue = EvaluatePostExpressionTree(root.Right);
                switch (root.Operator)
                {
                    case '+':
                        return rightValue + leftValue;
                    case '-':
                        return rightValue - leftValue;
                    case '*':
                        return rightValue * leftValue;
                    case '/':
                        return rightValue / leftValue;
                    default:
                        throw new InvalidOperationException("Invalid operator");
                }
            }
        }

        // Method to evaluate an Prefix expression tree
        private double EvaluatePreExpressionTree(ExpressionTreeNode root)
        {
            if (root.IsLeafNode())
            {
                return root.Value;
            }
            else
            {
                double leftValue = EvaluatePreExpressionTree(root.Left);
                double rightValue = EvaluatePreExpressionTree(root.Right);
                switch (root.Operator)
                {
                    case '+':
                        return leftValue + rightValue;
                    case '-':
                        return leftValue - rightValue;
                    case '*':
                        return leftValue * rightValue;
                    case '/':
                        return leftValue / rightValue;
                    default:
                        throw new InvalidOperationException("Invalid operator");
                }
            }
        }
    }
}