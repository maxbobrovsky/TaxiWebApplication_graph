using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaxiWebApplication.Services
{
    public class GraphRoadService
    {
        public async Task<double> GraphDistance(double first_lat, double first_long, double second_lat, double second_long)
        {
            using(var httpClient = new HttpClient())
            {
                string api_url = "https://graphhopper.com/api/1/route?vehicle=car&locale=uk&key=LijBPDQGfu7Iiq80w3HzwB4RUDJbMbhs6BU0dEnn&elevation=false&instructions=true&turn_costs=true&point=" + first_lat.ToString(CultureInfo.InvariantCulture) + "," + first_long.ToString(CultureInfo.InvariantCulture) + "&point=" + second_lat.ToString(CultureInfo.InvariantCulture) + "," + second_long.ToString(CultureInfo.InvariantCulture);

                HttpResponseMessage response = await httpClient.GetAsync(api_url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var myJObject = JObject.Parse(responseBody);

                var arr = myJObject.SelectToken("paths").ToString();

                string s = "\"distance\": ";

                int startInd = arr.IndexOf(s);
                StringBuilder sb = new StringBuilder();

                for (int i = startInd + s.Length; arr[i] != ','; i++) // Adding till we are in the number
                {
                    sb.Append(arr[i]);
                }

                IFormatProvider provider = CultureInfo.InvariantCulture;

                if (Double.TryParse(sb.ToString(), NumberStyles.Any, provider, out double distance))
                {
                    return distance / 1000;
                }

                return 0;


            }
        }
    }
}
