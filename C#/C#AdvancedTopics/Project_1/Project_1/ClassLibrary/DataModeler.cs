/*
 * File name: Statistics.cs
 * Authors: Seolan Jin, Junyeong Jo, Dhanashri Suresh Nayi
 * Date: 2/20/2023
 * Description: This class has some methods to parse a file.
 */

using Microsoft.VisualBasic.FileIO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClassLibrary
{
    public class DataModeler
    {
        // property
        private Dictionary<string, CityInfo> ParsedCitiesPairs = new Dictionary<string, CityInfo>();
        
        //This method parses an XML file with the specified file name
        //and stores the parsed information in the ParsedCitiesPairs dictionary.
        public void ParseXML(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            Dictionary<string, List<CityInfo>> cities = new Dictionary<string, List<CityInfo>>();

            // Get the root element
            XmlElement root = xmlDoc.DocumentElement;

            // Iterate over child nodes
            foreach (XmlNode node in root.ChildNodes)
            {
                // Check if the node is an element
                if (node.NodeType == XmlNodeType.Element && node.Name == "CanadaCity")
                {
                    string cityName = "";
                    string cityAscii = "";
                    double latitude = 0, longitude = 0;
                    string province = "";
                    string capital = "";
                    int population = 0;
                    int cityID = 0;

                    // Iterate over the child nodes of the CanadaCity element
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        // Check the name of the child node and store the data in the CityInfo object
                        switch (childNode.Name)
                        {
                            case "city":
                                cityName = childNode.InnerText;
                                break;
                            case "city_ascii":
                                cityAscii = childNode.InnerText;
                                break;
                            case "lat":
                                latitude = double.Parse(childNode.InnerText);
                                break;
                            case "lng":
                                longitude = double.Parse(childNode.InnerText);
                                break;
                            case "admin_name":
                                province = childNode.InnerText;
                                break;
                            case "capital":
                                capital = childNode.InnerText;
                                break;
                            case "population":
                                population = int.Parse(childNode.InnerText);
                                break;
                            case "id":
                                cityID = int.Parse(childNode.InnerText);
                                break;
                            default:
                                // Ignore unknown elements
                                break;
                        }
                    }
                    //  Creates a new CityInfo object with the data
                    CityInfo cityInfo = new CityInfo(cityName, cityAscii, latitude, longitude, province, capital, population, cityID);

                    // Checks if the city name is already in the dictionary.
                    if (cities.ContainsKey(cityName))
                    {
                        cities[cityName].Add(cityInfo);
                    }
                    else
                    {
                        // create a new list of CityInfo and add current cityinfo object
                        List<CityInfo> list = new List<CityInfo>();
                        list.Add(cityInfo);
                        cities.Add(cityName, list);
                    }
                }
            }
            // Call the FinalizeDictionary method to organize the parsed information into the desired format
            FinalizeDictionary(cities);
        }

        // This method parses a JSON file with the specified file name
        // and stores the parsed information in the ParsedCitiesPairs dictionary.
        public void ParseJSON(string fileName)
        {
            // Read the contents of the JSON file into a string
            string json = File.ReadAllText(fileName);

            // Deserialize the JSON string into a list of objects
            List<object> fields = JsonConvert.DeserializeObject<List<object>>(json);

            // Create a dictionary to hold the parsed city information
            Dictionary<string, List<CityInfo>> cities = new Dictionary<string, List<CityInfo>>();
           
            foreach (object field in fields)
            {
                // Convert the object to a JObject so that we can easily access its properties
                JObject cityObject = (JObject)field;

                // If the city_ascii property is null, break out of the loop
                if (cityObject["city_ascii"] == null)
                {
                    break;
                }

                // Get the values of the properties we're interested in
                string cityName = (string)cityObject["city"];
                string cityAscii = (string)cityObject["city_ascii"];
                double latitude = (double)cityObject["lat"];
                double longitude = (double)cityObject["lng"];
                string province = (string)cityObject["admin_name"];
                string capital = (string)cityObject["capital"];
                int population = (int)cityObject["population"];
                int cityID = (int)cityObject["id"];

                // Create a new CityInfo object using the values we just retrieved
                CityInfo cityInfo = new CityInfo(cityName, cityAscii, latitude, longitude, province, capital, population, cityID);
                
                // Check if the city name already exists in the dictionary
                if (cities.ContainsKey(cityName))
                {
                    // If it does, add the CityInfo object to the existing list
                    cities[cityName].Add(cityInfo);
                }
                else
                {
                    // If it doesn't, create a new list and add the CityInfo object to it, then add the list to the dictionary
                    List<CityInfo> list = new List<CityInfo>();
                    list.Add(cityInfo);
                    cities.Add(cityName, list);
                }
            }
            // Call the FinalizeDictionary method to organize the parsed information into the desired format
            FinalizeDictionary(cities);
        }

        //This method parses a CSV file with the specified file name
        //and stores the parsed information in the ParsedCitiesPairs dictionary.
        public void ParseCSV(string fileName)
        {
            // Create a TextFieldParser object to parse the CSV file
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                // set the delimiter for the CSV file
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                Dictionary<string, List<CityInfo>> cities = new Dictionary<string, List<CityInfo>>();

                // read the CSV file one line at a time
                while (!parser.EndOfData)
                {
                    // get the fields in the current line
                    string[] fields = parser.ReadFields() ?? new string[0];

                    // go to the next loop if the fields are the first row (category)
                    if (fields[0] == "city")
                    {
                        continue;
                    }

                    // Extract the fields for the current city
                    string cityName = fields[0];
                    string cityAscii = fields[1];
                    double latitude = double.Parse(fields[2]);
                    double longitude = double.Parse(fields[3]);
                    string province = fields[5];
                    string capital = fields[6];
                    int population = int.Parse(fields[7]);
                    int cityID = int.Parse(fields[8]);


                    // Create a new CityInfo object with the extracted fields
                    CityInfo cityInfo = new CityInfo(cityName, cityAscii, latitude, longitude, province, capital, population, cityID);

                    // Add the CityInfo object to the dictionary, grouping by city name
                    if (cities.ContainsKey(cityName))
                    {
                        cities[cityName].Add(cityInfo);
                    }
                    else
                    {
                        // create a new list and add the CityInfo object to it, then add the list to the dictionary
                        List<CityInfo> list = new List<CityInfo>();
                        list.Add(cityInfo);
                        cities.Add(cityName, list);
                    }
                }

                // Call the FinalizeDictionary method to organize the parsed information into the desired format
                FinalizeDictionary(cities);
            }
        }

        // This method takes the parsed information and stores it in the ParsedCitiesPairs dictionary
        private void FinalizeDictionary(Dictionary<string, List<CityInfo>> cities)
        {
            // Loop through each city in the dictionary
            foreach (KeyValuePair<string, List<CityInfo>> city in cities)
            {
                // if the city occures more than once
                if (city.Value.Count > 1)
                {
                    // Declare a counter variable to keep track of how many times the city has occurred
                    int occurrence = 1;
                    // Loop through each duplicate city and add it to the ParsedCitiesPairs dictionary with a unique key
                    foreach (CityInfo duplicateCity in city.Value)
                    {
                        ParsedCitiesPairs.Add($"{city.Key}:{occurrence++}", duplicateCity);
                    }
                }
                else
                {
                    // If the city only occurs once, add it to the ParsedCitiesPairs dictionary with its original key
                    ParsedCitiesPairs.Add(city.Key, city.Value.First());
                }
            }
        }

        // declare a delegate
        public delegate void DataModelerDelegate(string fileName);

        // This methiod gets a file name and a type of the file and returns a Dictionary that contains CityInfo
        public Dictionary<string,CityInfo> ParseFile(string fileName, string type)
        {
            // Declare a delegate that will be used to hold a reference to one of the Parse methods
            DataModelerDelegate dataModelerDelegate;

            // Depending on the file type specified, set the delegate to reference the appropriate Parse method
            if (type == "XML")
            {
                dataModelerDelegate = new DataModelerDelegate(ParseXML);
            }
            else if (type == "JSON")
            {
                dataModelerDelegate = new DataModelerDelegate(ParseJSON);
            }
            else if (type == "CSV")
            {
                dataModelerDelegate = new DataModelerDelegate(ParseCSV);
            }
            else
            {
                // If an invalid file type is specified, print an error message and set the delegate to null
                Console.WriteLine("Error: Invalid input");
                dataModelerDelegate = null;
            }

            // Call the appropriate Parse method using the delegate, passing in the fileName argument
            dataModelerDelegate(fileName);

            // Return the parsed city information as a dictionary
            return ParsedCitiesPairs;
        }
    }
}
