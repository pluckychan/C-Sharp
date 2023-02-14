using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;       /* HttpClient for resquest URL content */

namespace WeatherJSON
{
    internal class clsNewtonJsonDeserialize
    {
        public static string sJSON_str = "";
        public clsNewtonJsonDeserialize()
        {

        }

        public static async Task getJsonObjectUrl()
        {
            string sAPIKey = "2505fa5fa74e4df4866123238230602";
            string sCityName = "Wokingham";
            string sSourceURL = string.Format("http://api.weatherapi.com/v1/current.json?key={0}&q={1}", sAPIKey, sCityName);
            HttpClient oHttpClient = new HttpClient();
            HttpResponseMessage oResponse = await oHttpClient.GetAsync(sSourceURL); // await, wait for weatherapi response completed
            oResponse.EnsureSuccessStatusCode();
            string sjsonString = await oResponse.Content.ReadAsStringAsync(); // Json content to deserialize

            dynamic objTemp = JArray.Parse("[" + sjsonString +"]");

            dynamic objJSON = objTemp[0];

            string title = objJSON.Title;

            sJSON_str = string.Format("Name: {0}\r\nRegion: {1}\r\nCountry: {2}\r\nTempature: {3}\r\nCondition: {4} ({5})",
                        objJSON.location.name, objJSON.location.region, objJSON.location.country,
                        objJSON.current.temp_c, objJSON.current.condition.text, objJSON.current.condition.icon);

        }
    }
}
