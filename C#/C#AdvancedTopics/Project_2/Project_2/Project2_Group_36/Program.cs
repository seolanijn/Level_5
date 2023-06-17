/*
 * File name: Program.cs
 * Authors: Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date: 4/08/2023
 * Description: This is a C# program that can interpret human mathematical expressions and convert them to machine mathematical expressions, 
 *              which are in prefix and postfix notation. 
 *              This program will take the infix notation expression as input and use a set of functions
 *              to convert it to its corresponding prefix and postfix notations.
 */



using System.Xml;
using System.IO;
using ClassLibrary;
using System.Diagnostics;
using System.Text;

namespace Project_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CSVFile file = new CSVFile();

            //Deserialize the csv file into InFix List
            file.CSVDeserialize("./data/Project 2_INFO_5101.csv");

            ExpressionEvaluation evaluation = new ExpressionEvaluation();
            InfixToPrefix infixToPrefix = new InfixToPrefix();
            InfixToPostfix infixToPostfix = new InfixToPostfix();

            //Convert InFix into Prefix and Postfix
            infixToPrefix.Convert(file.InFix);
            infixToPostfix.Convert(file.InFix);

            List<string[]> prefixes = infixToPrefix.PreFix;
            List<string[]> postfixes = infixToPostfix.PostFix;
            CompareExpressions compareExpressions = new CompareExpressions();

            Console.WriteLine("====================================================================================================================");
            Console.WriteLine("+                                                Summary Report                                                    +");
            Console.WriteLine("====================================================================================================================");

            Console.WriteLine("|    Sno|                   Infix|                 PostFix|                   Prefix| Prefix Res|PostFix Res| Match|");
            Console.WriteLine("====================================================================================================================");

            for (int i = 0; i < file.InFix.Count; i++)
            {
                string sno = prefixes[i][0];
                string infix = file.InFix[i][1];
                string post = postfixes[i][1];
                string pre = prefixes[i][1];
                string preResult = evaluation.EvaluatePrefix(pre);
                string postResult = evaluation.EvaluatePostfix(post);
                string isMatch = compareExpressions.Compare(preResult, postResult) == 1 ? "True" : "False";

                Console.WriteLine($"|{sno.PadLeft(7)}|{infix.PadLeft(24)}|{post.PadLeft(24)}|{pre.PadLeft(25)}|{preResult.PadLeft(11)}|{postResult.PadLeft(11)}|{isMatch.PadLeft(6)}|");
            }
            Console.WriteLine("====================================================================================================================");


            //Takes user input whether they are going to generate and upload the xml file
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Do you want to create a XML file? y/n : ");
                char userInput = Console.ReadKey().KeyChar;
                if (userInput == 'y')
                {
                    isValid = true;
                    using (var stream = new StreamWriter("./data/output.xml"))
                    {
                        var settings = new XmlWriterSettings();
                        settings.Indent = true;
                        settings.IndentChars = "    ";
                        settings.NewLineChars = "\r\n";
                        settings.Encoding = Encoding.UTF8;
                        using (var writer = XmlWriter.Create(stream, settings))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartRootElement();

                            for (int i = 0; i < file.InFix.Count; i++)
                            {
                                writer.WriteStartElement();
                                string sno = prefixes[i][0];
                                string infix = file.InFix[i][1];
                                string post = postfixes[i][1];
                                string pre = prefixes[i][1];
                                string preResult = evaluation.EvaluatePrefix(pre);
                                string postResult = evaluation.EvaluatePostfix(post);
                                string isMatch = compareExpressions.Compare(preResult, postResult) == 1 ? "True" : "False";

                                writer.WriteAttribute("sno", sno);
                                writer.WriteAttribute("infix", infix);
                                writer.WriteAttribute("prefix", pre);
                                writer.WriteAttribute("postfix", post);
                                writer.WriteAttribute("evaluation", preResult);
                                writer.WriteAttribute("comparison", isMatch);
                                writer.WriteEndElement();

                            }
                            writer.WriteEndRootElement();
                        }

                    }

                    // launch the xml file
                    Process.Start("cmd", $"/C start {@"./data/output.xml"}");
                }
                else if (userInput == 'n')
                {
                    isValid = true;
                    Console.WriteLine("\nThe program has ended without generating/updating an XML file.");
                }
                else
                {
                    Console.WriteLine("\nError: invalid input. Try Again.");
                }
            }
        }
    }
}