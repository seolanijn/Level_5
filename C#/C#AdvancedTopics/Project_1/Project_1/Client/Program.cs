/*
 * File name: Program.cs
 * Authors: Dhanashri Suresh Nayi, Seolan Jin, Junyeong Jo
 * Date: 2/20/2023
 * Description: This is a C# program that allows users to query data 
 *              from files containing information about Canadian cities. 
 */


using ClassLibrary;
using System.Globalization;

namespace Client
{
    internal class Program
    {
        const string JsonPath = "./Canadacities-JSON.json";
        const string CsvPath = "./Canadacities.csv";
        const string XmlPath = "./Canadacities-XML.xml";
        static async Task Main(string[] args)
        {
            string fileName = "";

            bool isExit = false;

            try
            {
                do
                {
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Program functionality");
                    Console.WriteLine("---------------------\n");

                    Console.WriteLine("- To exit program, enter 'exit' at any prompt");
                    Console.WriteLine("- To start program from the begining, enter 'restart' at any prompt");
                    Console.WriteLine("- You will be presented with a numbered list of options.");
                    Console.WriteLine("  Please enter a value, when prompted, to a corresponding file name, file type or data querying routine.\n");

                    // initializing a Statistics object to null
                    Statistics? statistics = null;

                    bool isValid = false;
                    bool isRestart = false;
                    string userInput;
                    do
                    {
                        Console.WriteLine("Fetching list of available file names to be processed and queried:");
                        Console.WriteLine("1) canadiancities-CSV");
                        Console.WriteLine("2) canadiancities-JSON");
                        Console.WriteLine("3) canadiancities-XML\n");

                        Console.Write("Select an option from the list above (e.g. 1, 2): ");
                        userInput = Console.ReadLine() ?? "";

                        // switch statement, which will execute the code block
                        // that matches the value of the userInput variable
                        switch (userInput)
                        {
                            case "1":
                                // setting the filename variable to CsvPath
                                fileName = CsvPath;
                                statistics = new Statistics(fileName, "CSV");
                                isValid = true;
                                break;
                            case "2":
                                // setting the filename variable to JsonPath
                                fileName = JsonPath;
                                statistics = new Statistics(fileName, "JSON");
                                isValid = true;
                                break;
                            case "3":
                                // setting the filename variable to XmlPath
                                fileName = XmlPath;
                                statistics = new Statistics(fileName, "XML");
                                isValid = true;
                                break;
                            case "exit":
                                isExit = true;
                                isValid = true;
                                break;
                            case "restart":
                                isRestart = true;
                                Console.WriteLine("\nRestart the program\n");
                                break;
                            default:
                                Console.WriteLine($"[{userInput}]: invalid input.");
                                break;
                        }
                    } while (!isValid); // continue looping while isValid is false
                    
                    // if isRestart is true, continue with the next iteration of the outer do-while loop
                    if (isRestart) continue;
                    // if isExit is true, break out of the outer do-while loop
                    if (isExit) break;

                    if (statistics == null)
                    {
                        throw new NullReferenceException("Statistics is null");
                    }

                    Console.WriteLine($"A city catalogue has now been populated from the {fileName.Substring(2)} file.\n");

                    Console.WriteLine($"Fetching list of available data querying routines that can be run on the {fileName.Substring(2)} file.\n");

                    do
                    {
                        // Display a menu of options for the user to choose from
                        Console.WriteLine(" 1) Display City Information");
                        Console.WriteLine(" 2) Display Province Cities");
                        Console.WriteLine(" 3) Calculate Province Population");
                        Console.WriteLine(" 4) Compare Cities Population");
                        Console.WriteLine(" 5) Distance Between Cities");
                        Console.WriteLine(" 6) Display Largest Population City By Province");
                        Console.WriteLine(" 7) Display Smallest Population City By Province");
                        Console.WriteLine(" 8) Rank Provinces By Population");
                        Console.WriteLine(" 9) Rank Provinces By Cities");
                        Console.WriteLine("10) Display Capital City By Province");
                        Console.WriteLine("11) Show City On Map");
                        Console.WriteLine("12) Restart Program And Choose Another File or File Type to Query\n");


                        // Get the user's choice of query from the menu
                        Console.Write($"Select a data query routine from the list above for the {fileName.Substring(2)} file (e.g. 1, 2): ");
                        userInput = Console.ReadLine() ?? "";

                        string name;

                        // Based on the user's choice of query,
                        // call the appropriate method on the Statistics object
                        // and display the results
                        switch (userInput)
                        {
                            case "1":
                                // Get city name and display information
                                Console.WriteLine("\n< Display City Information >\n");
                                Console.Write("Enter City Name (e.g. London): ");
                                name = Console.ReadLine() ?? "";
                                string output = statistics.DisplayCityInformation(CapitalizeString(name));
                                Console.WriteLine(output);
                                break;
                            case "2":
                                // Get province name and display city names in that province
                                Console.WriteLine("\n< Display Province Cities >\n");
                                Console.Write("Enter Province Name (e.g. Ontario): ");
                                name = Console.ReadLine() ?? "";
                                List<string> cityNames = statistics.DisplayProvinceCities(CapitalizeString(name));

                                // show results only if the city name exists
                                if (cityNames.Count > 0)
                                {
                                    Console.WriteLine($"\nFollowing cities are located in {CapitalizeString(name)}:\n");
                                    foreach (string cityName in cityNames)
                                    {
                                        Console.WriteLine(cityName);
                                    }
                                    Console.WriteLine();
                                }
                                break;
                            case "3":
                                // Get province name and display population
                                Console.WriteLine("\n< Calculate Province Population >\n");
                                Console.Write("Enter Province Name (e.g. Ontario): ");
                                name = Console.ReadLine() ?? "";
                                int population = statistics.DisplayProvincePopulation(CapitalizeString(name));
                                string formattedPopulation = population.ToString("N0", new CultureInfo("en-US")); // format the value
                                Console.WriteLine($"\nPopulation of {CapitalizeString(name)}: {formattedPopulation}\n");
                                break;
                            case "4":
                                // Get two city names and compare their populations
                                Console.WriteLine("\n< Match Cities Population >\n");
                                Console.Write("Enter First City Name (e.g. London): ");
                                string city1 = Console.ReadLine() ?? "";
                                Console.Write("Enter Second City Name (e.g. Toronto): ");
                                string city2 = Console.ReadLine() ?? "";
                                string? largerCity = statistics.CompareCitiesPopulation(CapitalizeString(city1), CapitalizeString(city2));

                                // show results only if the input is valid
                                if (largerCity != null)
                                    Console.WriteLine($"\n{largerCity}\n");

                                break;
                            case "5":
                                // Get two city names and calculate distance between them
                                Console.WriteLine("\n< Distance Between Cities >\n");
                                Console.Write("Enter First City Name (e.g. London): ");
                                string orgCity = Console.ReadLine() ?? "";
                                Console.Write("Enter Second City Name (e.g. Toronto): ");
                                string destCity = Console.ReadLine() ?? "";

                                double distance = await statistics.CalculateDistanceBetweenCities(CapitalizeString(orgCity), CapitalizeString(destCity));
                                Console.WriteLine($"\nThe distance between {CapitalizeString(orgCity)} and {CapitalizeString(destCity)} is {distance} Km\n");
                                break;
                            case "6":
                                // Get province name and display largest population city
                                Console.WriteLine("\n< Display Largest Population City By Province >\n");
                                Console.Write("Enter Province Name (e.g. Ontario): ");
                                name = Console.ReadLine() ?? "";

                                string largestCity = statistics.DisplayLargestPopulationCity(CapitalizeString(name));
                                Console.WriteLine($"\nThe largest population city in {CapitalizeString(name)} is {largestCity}\n");
                                break;

                            case "7":
                                // Get province name and display smallest population city
                                Console.WriteLine("\n< Display Smallest Population City By Province >\n");
                                Console.Write("Enter Province Name (e.g. Ontario):");
                                name = Console.ReadLine() ?? "";

                                string smallestCity = statistics.DisplaySmallestPopulationCity(CapitalizeString(name));
                                Console.WriteLine($"\nThe smallest population city in {CapitalizeString(name)} is {smallestCity}\n");
                                break;
                            case "8":
                                // Get ranks of the provinces in ascending order based on their population.
                                Console.WriteLine("\n< Rank Provinces By Population >\n");
                                Console.WriteLine("\nThe provinces ranked in ascending order by population:\n");

                                statistics.RankProvincesByPopulation();
                                Console.WriteLine();
                                break;
                            case "9":
                                // Get ranks of the provinces in ascending order based on the number of cities in each province.
                                Console.WriteLine("\n< Rank Provinces By Cities >\n");
                                Console.WriteLine("\nThe provinces ranked in ascending order by the number of cities:\n");

                                statistics.RankProvincesByCities();
                                Console.WriteLine();
                                break;
                            case "10":
                                // Get a province name and the program displays the capital city of that province.
                                Console.WriteLine("\n< Display Capital City By Province >\n");
                                Console.Write("Enter Province Name (e.g. Ontario): ");
                                name = Console.ReadLine() ?? "";

                                statistics.GetCapital(CapitalizeString(name));
                                break;
                            case "11":
                                // get a map with city that user input along with the province.
                                Console.WriteLine("\n< Show City On Map >\n");
                                Console.Write("Enter City Name (e.g. London): ");
                                name = Console.ReadLine() ?? "";
                                Console.Write("Enter Province Name (e.g. Ontario): ");
                                string provinceName = Console.ReadLine() ?? "";

                                Console.WriteLine("\nOpening the Google Maps...\n");
                                statistics.ShowCityOnMap(CapitalizeString(name), CapitalizeString(provinceName));
                                break;
                            case "12":
                                // The program restarts by setting the isRestart flag to true.
                                isRestart = true;
                                Console.WriteLine("\nRestart the program\n");
                                break;
                            case "exit":
                                // The program exits by setting the isExit flag to true.
                                isExit = true;
                                break;
                            case "restart":
                                // The program restarts by setting the isRestart flag to true.
                                isRestart = true;
                                Console.WriteLine("\nRestart the program\n");
                                break;
                            default:
                                Console.WriteLine($"\n[{userInput}]: invalid input\n");
                                break;
                        }
                    } while (!isExit && !isRestart);
                    if (isRestart) continue;
                } while (!isExit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Error: {ex.Message}");
            }

        }

        // returns a string with the first character with uppercase and the rest with lowercase
        private static string CapitalizeString(string val)
        {
            if (val.Length == 0) 
                return val;

            // handle the space of val
            string[] tempVals = val.Split(' ');
            string result = "";

            for (int i = 0; i < tempVals.Length; i++)
            {
                result += char.ToUpper(tempVals[i][0]) + tempVals[i].Substring(1).ToLower();
                if (i + 1 != tempVals.Length)
                {
                    result += " ";
                }
            }

            return result;
        }
    }
}