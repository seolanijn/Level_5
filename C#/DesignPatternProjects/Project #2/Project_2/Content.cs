/*
 * File Name: Content.cs
 * Author: Seolan Jin
 * Date: March 10, 2023
 * Description: This class is an abstract class that defines a template method and abstract Getters functions.
 */
using System;

namespace Assi2
{
    abstract class Content
    {
        // Template Method
        public void Print()
        {
            // Declares a string to format the output properly
            string[] lines = GetPrintableBody().Split('\n');
            int numChar = lines[lines.Length - 1].Length - 4 > 0 ? lines[lines.Length - 1].Length - 4 : 0;
            string temp = new string('-', numChar);

            // Print Title and Body
            Console.WriteLine(GetPrintableTitle());
            Console.WriteLine($"+-{temp}-+");
            Console.WriteLine(GetPrintableBody());
        }

        // Abstract functions
        public abstract string GetPrintableTitle();
        public abstract string GetPrintableBody();
    }
}
