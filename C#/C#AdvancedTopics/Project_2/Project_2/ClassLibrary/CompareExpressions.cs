using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    // Define a class named "CompareExpressions" which implements the "IComparer" interface
    public class CompareExpressions : IComparer
    {
        // Define a method named "Compare" that takes two doubles as input and returns a boolean indicating if they are equal
        public bool Compare(double x, double y)
        {
            return x == y;
        }

        // Define a method named "Compare" that takes two objects as input and returns an integer indicating if they are equal
        public int Compare(object? x, object? y)
        {
            // Convert the objects to doubles
            double result1 = Convert.ToDouble(x);
            double result2 = Convert.ToDouble(y);

            // Call the "Compare" method that takes two doubles as input and return 1 if they are equal, 0 if they are not
            return Compare(result1, result2) ? 1 : 0;
        }
    }
}
