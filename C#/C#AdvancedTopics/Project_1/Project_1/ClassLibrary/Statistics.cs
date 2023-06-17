/*
 * File name: Statistics.cs
 * Authors: Seolan Jin, Junyeong Jo, Dhanashri Suresh Nayi
 * Date: 2/20/2023
 * Description: This is a class that defines some useful methods to simulate the main program.
 */

using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace ClassLibrary
{
    public class Statistics
    {
        // property
        public Dictionary<string, CityInfo> CityCatalogue;

        // constructor
        public Statistics(string fileName, string fileType)
        {
            DataModeler dataModeler = new DataModeler();
            CityCatalogue = dataModeler.ParseFile(fileName, fileType);
        }

        // ============ city methods ============
        
        // this method gets a city name and
        // returns information about the city, including population, location, and the province it belongs to.
        public string DisplayCityInformation(string cityName)
        {
            string result = "";

            // Check if the given city name exists in the CityCatalogue dictionary.
            if (CityCatalogue.ContainsKey(cityName))
            {
                foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
                {
                    // If the current key matches the cityName parameter...
                    if (cityName == item.Key)
                    {
                        // Add information about the city to the result string.
                        result += $"\nCity: {cityName}\n";
                        result += $"Population: {FormatPopulation(item.Value.GetPopulation())}\n";
                        result += $"Location(latitude, longitude): ({item.Value.GetLocation().Key}, {item.Value.GetLocation().Value})\n";
                        result += $"Province: {item.Value.GetProvince()}\n";

                        // Break out of the loop since we found the city we were looking for.
                        break;
                    }
                }
            }
            // If the given city name wasn't found, check if a duplicate city exists.
            else if (CityCatalogue.ContainsKey($"{cityName}:1"))
            {
                // Call ConfirmDuplicateCity to get information about the duplicate city.
                CityInfo item = ConfirmDuplicateCity(cityName);

                // Add information about the duplicate city to the result string.
                result += $"\nCity: {cityName}\n";
                result += $"Population: {FormatPopulation(item.GetPopulation())}\n";
                result += $"Location(latitude, longitude): ({item.GetLocation().Key}, {item.GetLocation().Value})\n";
                result += $"Province: {item.GetProvince()}\n";
            }
            // If neither the given city name nor a duplicate was found, output an error message.
            else
            {
                Console.WriteLine("Error: The city name you provided doesn't seem to match any location.");
            }

            return result;
        }

        // this method gets a province name and
        // returns a city that has the largest population in the given province name
        public string DisplayLargestPopulationCity(string provinceName)
        {
            // get the first pair where the entered province name matches the province name of the current value from CityCatalogue
            KeyValuePair<string, CityInfo> currLargestPopulationCity = CityCatalogue.FirstOrDefault(item => provinceName == item.Value.GetProvince());

            if (currLargestPopulationCity.Key == null || currLargestPopulationCity.Value == null)
            {
                Console.WriteLine("\nError: The city name you provided doesn't seem to match any location.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return "(Not Found)";
            }

            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                // Iterate through all the cities in the given province
                if (provinceName == item.Value.GetProvince())
                {
                    if (currLargestPopulationCity.Value.GetPopulation() < item.Value.GetPopulation())
                        currLargestPopulationCity = item;
                }
            }
            return currLargestPopulationCity.Key;
        }

        // this method gets a province name and
        // returns a city that has the smallest population in the given province name
        public string DisplaySmallestPopulationCity(string provinceName)
        {
            // get the first pair where the entered province name matches the province name of the current value from CityCatalogue
            KeyValuePair<string, CityInfo> currSmallestPopulationCity = CityCatalogue.FirstOrDefault(item => provinceName == item.Value.GetProvince());
            
            if (currSmallestPopulationCity.Key == null || currSmallestPopulationCity.Value == null)
            {
                Console.WriteLine("\nError: The city name you provided doesn't seem to match any location.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return "(Not Found)";
            }

            // Iterate through all the cities and compare the populations to find the smallest population city
            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                if (provinceName == item.Value.GetProvince())
                {
                    if (currSmallestPopulationCity.Value.GetPopulation() > item.Value.GetPopulation())
                        currSmallestPopulationCity = item;
                }
            }

            return currSmallestPopulationCity.Key;
        }

        // this method gets two city names
        // returns a city that has the larger population among the cities
        public string? CompareCitiesPopulation(string city1, string city2)
        {
            string? result = null;
            CityInfo cityInfo1, cityInfo2;

            // Check if CityCatalogue contains city1
            if (CityCatalogue.ContainsKey(city1))
            {
                cityInfo1 = CityCatalogue[city1];
            }
            // If city1 not found but has a duplicate entry
            else if (CityCatalogue.ContainsKey($"{city1}:1"))
            {
                // Call ConfirmDuplicateCity to retrieve the exact city
                cityInfo1 = ConfirmDuplicateCity(city1);
            }
            // If city1 not found
            else
            {
                Console.WriteLine("\nError: The city name you provided doesn't seem to match any location.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return result;
            }

            // Check if CityCatalogue contains city2
            if (CityCatalogue.ContainsKey(city2))
            {
                cityInfo2 = CityCatalogue[city2];
            }
            // If city2 not found but has a duplicate entry
            else if (CityCatalogue.ContainsKey($"{city2}:1"))
            {
                // Call ConfirmDuplicateCity to retrieve the exact city
                cityInfo2 = ConfirmDuplicateCity(city2);
            }
            // If city2 not found
            else
            {
                Console.WriteLine("\nError: The city name you provided doesn't seem to match any location.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return result;
            }

            // Compare populations of the two cities and update result accordingly
            if (cityInfo1.GetPopulation() > cityInfo2.GetPopulation())
            {
                result = $"{city1} has a larger population than {city2} with a population of {FormatPopulation(cityInfo1.GetPopulation())}";
            }
            else if (cityInfo1.GetPopulation() < cityInfo2.GetPopulation())
            {
                result = $"{city2} has a larger population than {city1} with a population of {FormatPopulation(cityInfo2.GetPopulation())}";
            }
            else
            {
                result = $"Two cities have the same population with a population of {FormatPopulation(cityInfo1.GetPopulation())}";
            }

            return result;

        }

        // This method gets population and returns formatted integer as a string
        private string FormatPopulation(int population)
        {
            return population.ToString("N0", new CultureInfo("en-US"));
        }

        // This method gets a duplicate city name and asks the user to choose a city
        private CityInfo ConfirmDuplicateCity(string cityName)
        {
            // create a list to store the provinces of cities with the same name
            List<string> provinces = new List<string>();

            int occurrence = 1; // current number of city
            string currCityName = $"{cityName}:{occurrence}";

            // loop through the CityCatalogue to find all cities with the same name
            do
            {
                provinces.Add(CityCatalogue[currCityName].GetProvince());

                // increment the occurrence and update the currCityName string
                currCityName = $"{cityName}:{++occurrence}";
            } while (CityCatalogue.ContainsKey(currCityName));

            // create a flag to keep track of whether the user input is valid
            bool isValid = false;
            do
            {
                // display a list of cities with the same name and their provinces
                Console.WriteLine($"\nThere are {occurrence - 1} cities called {cityName} in Canada:");
                for (int i = 0; i < provinces.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {cityName} in {provinces[i]}");
                }
                // prompt the user to select a city from the list
                Console.Write($"\nSelect a city from the list above (e.g. 1, 2): ");
                string userInput = Console.ReadLine() ?? "";

                // check if the user input is valid
                for (int i = 0; i < provinces.Count; i++)
                {
                    if (userInput == (i + 1).ToString())
                    {
                        // if the user input is valid, return the CityInfo object of the selected city
                        return CityCatalogue[$"{cityName}:{i + 1}"];
                    }
                }

                // if we are still in the method, it means the user entered wrong input
                Console.WriteLine("Error: Invalid input. Select again.");

            } while (!isValid);

            return null;
        }

        // This method takes in a city name and province name and shows the location of the city on Google Maps.
        public void ShowCityOnMap(string cityName, string provinceName)
        {
            CityInfo cityInfo = null;

            // Check if the city name exists in the CityCatalogue dictionary
            if (CityCatalogue.ContainsKey(cityName))
            {
                // If it exists, retrieve the CityInfo object for the city
                cityInfo = CityCatalogue[cityName];
            }
            // If the city name does not exist in the CityCatalogue dictionary, check if a city name with a ':1' suffix exists
            else if (CityCatalogue.ContainsKey($"{cityName}:1"))
            {
                // If it does, iterate through all possible city names with the same prefix and a number suffix until a matching province is found
                int idx = 1;
                while (CityCatalogue.ContainsKey($"{cityName}:{idx}"))
                {
                    // Check if the province for the current city matches the provided province name
                    if (CityCatalogue[$"{cityName}:{idx}"].GetProvince() == provinceName)
                    {
                        // If it matches, retrieve the CityInfo object for the city
                        cityInfo = CityCatalogue[$"{cityName}:{idx}"];
                        break;
                    }
                    idx++;
                }
                // If no matching province is found, display an error message and return
                if (cityInfo == null)
                {
                    Console.WriteLine("Error: The province name you provided doesn't seem to match any location.");
                    Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                    return;
                }
            }
            // If neither the city name nor the city name with a ':1' suffix exist in the CityCatalogue dictionary, display an error message and return
            else
            {
                Console.WriteLine("\nError: The city name you provided doesn't seem to match any location.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return;
            }

            // Retrieve the latitude and longitude of the city from the CityInfo object
            double latitude = cityInfo.GetLocation().Key;
            double longitude = cityInfo.GetLocation().Value;

            // Create a URL for the Google Maps API using the latitude and longitude of the city
            string url = $"https://www.google.com/maps/search/?api=1&query={latitude},{longitude}";

            // Create a new ProcessStartInfo object with the URL as the filename and set UseShellExecute to true
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };

            // Start a new process with the ProcessStartInfo object, which will open the URL in the user's default web browser
            Process.Start(psi);
        }

        // This method gets two city names and returns calculated distance between the two cities
        public async Task<double> CalculateDistanceBetweenCities(string city1, string city2)
        {
            CityInfo cityInfo1, cityInfo2;

            // Check if city1 exists in the CityCatalogue
            if (CityCatalogue.ContainsKey(city1))
            {
                cityInfo1 = CityCatalogue[city1];
            }
            // Check if there are duplicate cities with the same name as city1
            else if (CityCatalogue.ContainsKey($"{city1}:1"))
            {
                // If there are duplicate cities, prompt the user to select one
                cityInfo1 = ConfirmDuplicateCity(city1);
            }
            // If city1 does not exist in the CityCatalogue, show an error message and return 0
            else
            {
                Console.WriteLine("\nError: As the city name provided does not exist, the distance will be shown as zero.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return 0;
            }

            // Repeat the same steps for city2
            if (CityCatalogue.ContainsKey(city2))
            {
                cityInfo2 = CityCatalogue[city2];
            }
            else if (CityCatalogue.ContainsKey($"{city2}:1"))
            {
                cityInfo2 = ConfirmDuplicateCity(city2);
            }
            else
            {
                Console.WriteLine("\nError: As the city name provided does not exist, the distance will be shown as zero.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
                return 0;
            }

            // Use the latitude and longitude of the cities to calculate the distance between them
            double lat1 = cityInfo1.GetLocation().Key;
            double lng1 = cityInfo1.GetLocation().Value;
            double lat2 = cityInfo2.GetLocation().Key;
            double lng2 = cityInfo2.GetLocation().Value;

            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins={lat1},{lng1}&destinations={lat2},{lng2}&key=AIzaSyB_WNpWf1tH0PbA9d8oZA6fJxxiS7mFU6A";
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                
                double distanceInMeters = (double)data["rows"][0]["elements"][0]["distance"]["value"];

                // Convert the distance from meters to kilometers and round to 2 decimal places
                return Math.Round(distanceInMeters / 1000, 2);
            }
        }

        // ============== province methods ==============

        // This method gets a province name and returns the sum of population of cities in the province
        public int DisplayProvincePopulation(string provinceName)
        {
            // variable to hold the sum of population of cities in the province
            int sumPopulation = 0;

            // loop through the CityCatalogue to find cities in the province
            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                // check if the current city belongs to the given province
                if (provinceName == item.Value.GetProvince())
                {
                    // add the population of the current city to the sum
                    sumPopulation += item.Value.GetPopulation();
                }
            }

            // if no cities were found in the province, print an error message
            if (sumPopulation == 0)
            {
                Console.WriteLine("\nError: As the province name provided does not exist, the population will be shown as zero.");
                Console.WriteLine(" Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
            }

            return sumPopulation;
        }

        // This method gets a province name and returns a list of cities in the province
        public List<string> DisplayProvinceCities(string provinceName)
        {
            // Create an empty list to store city names
            List<string> cityList = new List<string>();

            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                // Check if the city belongs to the specified province
                if (provinceName == item.Value.GetProvince())
                {
                    // If the city belongs to the province, add it to the cityList
                    cityList.Add(item.Key.Split(":").First());
                }
            }
            if (cityList.Count == 0)
            {
                Console.WriteLine("\n(It appears that there are no cities in the province you provided)");
                Console.WriteLine("Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
            }

            return cityList;
        }

        // This method shows the ranking of provinces by population
        public void RankProvincesByPopulation()
        {
            // Get all provinces in the catalogue
            SortedSet<string> provinces = GetAllProvinces();

            // Create a dictionary to store province population
            Dictionary<string, int> provincePopulations = new Dictionary<string, int>();
            foreach (string province in provinces)
            {
                // Add the province and its population to the dictionary
                provincePopulations.Add(province, DisplayProvincePopulation(province));
            }

            // Sort the dictionary by value (province population)
            Dictionary<string, int> sortedProvincePopulations = provincePopulations.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // Print the ranking of provinces by population
            int rank = 0;
            foreach (KeyValuePair<string, int> provincePopulation in sortedProvincePopulations)
            {
                Console.WriteLine($"[{++rank}] - {provincePopulation.Key} : {FormatPopulation(provincePopulation.Value)}");
            }
        }

        // This method shows the ranking of provinces by the number of cities in each province
        public void RankProvincesByCities()
        {
            SortedSet<string> provinces = GetAllProvinces();

            // Dictionary that is containing the province name as a key and the number of the cities as a value
            Dictionary<string, int> provinceCityPairs = new Dictionary<string, int>();

            // populate all provinces with value 0
            foreach (string province in provinces)
            {
                provinceCityPairs.Add(province, 0);
            }
            
            // increment where the key matches to the current item's province name
            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                provinceCityPairs[item.Value.GetProvince()]++;
            }

            // sort the provinceCityPairs by the number of the cities
            Dictionary<string, int> sortedProvinceCityPairs = provinceCityPairs.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            int rank = 0;

            // print the rank
            foreach (KeyValuePair<string, int> provinceCityPair in sortedProvinceCityPairs)
            {
                Console.WriteLine($"[{++rank}] - {provinceCityPair.Key} : {provinceCityPair.Value}");
            }
        }

        // This method returns a sorted set of all existing provinces from CityCatalogue
        private SortedSet<string> GetAllProvinces()
        {
            SortedSet<string> provinces = new SortedSet<string>();
            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                provinces.Add(item.Value.GetProvince());
            }
            return provinces;
        }

        // This method gets a province name and shows a capital of the province
        public void GetCapital(string provinceName)
        {
            foreach (KeyValuePair<string, CityInfo> item in CityCatalogue)
            {
                // checks if the city is in the specified province
                if (item.Value.GetProvince() == provinceName)
                {
                    // If it finds a city in the province with a capital flag,
                    // it prints the name of the city, its location, and terminates the method
                    if (item.Value.GetCapital() != "")
                    {
                        Console.WriteLine($"\nThe capital of {item.Value.GetProvince()} is {item.Key} with following information:");
                        Console.WriteLine($"    Latitude: {item.Value.GetLocation().Key}");
                        Console.WriteLine($"    Longitude: {item.Value.GetLocation().Value}\n");
                        return;
                    } 
                }
            }

            // If it does not find any city in the specified province or if no city is marked as a capital,
            // it prints an error message
            Console.WriteLine("\n(It appears that there is no capital in the province you provided)");
            Console.WriteLine("Note: All inputs must either be in all lowercase letters or only have the first letter capitalized.\n");
        }
    }
}
