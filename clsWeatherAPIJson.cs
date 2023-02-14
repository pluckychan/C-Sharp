using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;       /* HttpClient for resquest URL content */
using System.Text.Json; /* for Json serialization / deserialization */
using System.Dynamic;

namespace WeatherJSON
{
    internal class clsWeatherAPIJson
    {
        public static string sJSON_str = "";
        /* ====================================================*/
        /* define classes for Weather API json attributes, BEGIN */
        public class WeatherCurrent
        {
            public WeatherLocation location { get; set; }
            public CurrentWeather current { get; set; }
        }

        public class WeatherLocation
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
        }

        public class CurrentWeather
        {
            public string last_updated { get; set; }
            public decimal temp_c { get; set; }
            public WeatherCondition condition { get; set; }
        }

        public class WeatherCondition
        {
            public string text { get; set; }
            public string icon { get; set; }
        }
        /* define classes for Weather API json attributes, END */
        /* ====================================================*/
        public clsWeatherAPIJson()
        {

        }

        public class clsData {
            public string @class { get; set; }
            public string value { get; set; }
        }
        public static async Task getWeather()
        {
            try
            {
                string sAPIKey = "2505fa5fa74e4df4866123238230602";
                string sCityName = "Wokingham";
                string sJsonURL = string.Format("http://api.weatherapi.com/v1/current.json?key={0}&q={1}", sAPIKey, sCityName);
                HttpClient oHttpClient = new HttpClient();
                HttpResponseMessage oResponse = await oHttpClient.GetAsync(sJsonURL); // await, wait for weatherapi response completed
                oResponse.EnsureSuccessStatusCode();
                string sjsonString = await oResponse.Content.ReadAsStringAsync();
                WeatherCurrent oweatherCurrent = JsonSerializer.Deserialize<WeatherCurrent>(sjsonString);
                sJSON_str = string.Format("Name: {0}\r\nRegion: {1}\r\nCountry: {2}\r\nTempature: {3}\r\nCondition: {4} ({5})",
                    oweatherCurrent.location.name, oweatherCurrent.location.region, oweatherCurrent.location.country,
                    oweatherCurrent.current.temp_c, oweatherCurrent.current.condition.text, oweatherCurrent.current.condition.icon);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
