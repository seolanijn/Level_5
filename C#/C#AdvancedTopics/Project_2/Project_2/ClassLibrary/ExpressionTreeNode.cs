using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ExpressionTreeNode
    {
        //Operator
        public char Operator { get; set; }
        //Value
        public double Value { get; set; }
        public ExpressionTreeNode Left { get; set; }
        public ExpressionTreeNode Right { get; set; }

        public ExpressionTreeNode(char op)
        {
            Operator = op;
        }

        public ExpressionTreeNode(double val)
        {
            Value = val;
        }

        //Checks if the node is a leaf or parent
        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }
    }
}
