using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace RestCall
{
    class Program
    {
        static void Main(string[] args)
        {

            int result = GetCountries("un", 100090);
            Console.WriteLine(result);

            result = GetCountries("united", 200);
            Console.WriteLine(result);

            result = GetCountries("in", 1000000);
            Console.WriteLine(result);          
            
        }

        private static string FormatQueryString(string name, int pop)
        {
            return $"/api/countries/search?name={name}&page={pop}";
        }

        private static int GetCountriesAmount(dynamic items, string s, int p)
        {
            int count = 0;
            foreach (var item in items.data)
            {
                if (Convert.ToString(item["name"]).ToLower().Contains(s) && Convert.ToInt32(item["population"]) > p)
                    count++;
            }
            return count;
        }

        private static int GetCountries(string s, int p)
        {
            int countResult = 0;
            using (var restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
                restClient.Timeout = TimeSpan.FromSeconds(10);

                string query = FormatQueryString(s, 1);
                var request = new HttpRequestMessage(HttpMethod.Get, query);
                var response = restClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Request error...");
                    return 0;
                }
                else
                {

                    dynamic countries = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    countResult = GetCountriesAmount(countries, s, p);

                    var onlyCountryData = countries["data"];


                    for (int pageNumber = 2; pageNumber <= countries["total_pages"].Value; pageNumber++)
                    {
                        query = FormatQueryString(s, pageNumber);

                        request = new HttpRequestMessage(HttpMethod.Get, query);
                        response = restClient.SendAsync(request).Result;

                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Request error...");
                            break;
                        }
                        else
                        {
                            countries = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                            countResult += GetCountriesAmount(countries, s, p);

                        }
                    }
                }
            }

            return countResult;
        }

    }
}
