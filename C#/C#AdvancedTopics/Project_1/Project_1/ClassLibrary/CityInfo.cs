/*
 * File name: CityInfo.cs
 * Authors: Junyeong Jo, Seolan Jin, Dhanashri Suresh Nayi
 * Date: 2/20/2023
 * Description: This class defines some properties about city information. 
 */

namespace ClassLibrary
{
    public class CityInfo
    {
        // properties
        private string CityName;
        private string CityAscii;
        private double Latitude, Longitude;
        private string Province;
        private string Capital;
        private int Population;
        private int CityID;
        
        // constructor
        public CityInfo(string cityName, string cityAscii, double latitude, double longitude, string province, string capital, int population, int cityID)
        {
            this.CityName = cityName;
            this.CityAscii = cityAscii;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Province = province;
            this.Capital = capital;
            this.Population = population;
            this.CityID = cityID;
        }

        // methods

        // returns the Province of the city
        internal string GetProvince() { return Province; }

        // returns the number of populations in the city
        internal int GetPopulation() { return Population; }

        // returns the latitude and longitude of a city
        internal KeyValuePair<double,double> GetLocation()
        {
            KeyValuePair<double, double> result = new KeyValuePair<double, double>(Latitude, Longitude);
            return result;
        }

        // returns the capital
        internal string GetCapital()
        {
            return Capital;
        }
        
        
    }
}