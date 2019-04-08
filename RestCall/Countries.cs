using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace RestCall
{
    public static class Countries
    {

        private static string FormatQueryString(string name , int pop)
        {
            return $"/api/countries/search?name={name}&page={pop}";
        }

        public static int GetCountries(string s , int p)
        {
            List<CountryDataResponse> resultList = new List<CountryDataResponse>();            
            using (var restClient = new HttpClient())
            {
                restClient.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
                restClient.Timeout = TimeSpan.FromSeconds(10);

                string query = FormatQueryString(s, 1);
                var request = new HttpRequestMessage(HttpMethod.Get, query);
                var response = restClient.SendAsync(request).Result;

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error...");
                    return 0;
                }
                else
                {
                    var res = JsonConvert.DeserializeObject<CountryResponse>(response.Content.ReadAsStringAsync().Result);

                    var myList = JsonConvert.DeserializeObject<CountryResponse>(response.Content.ReadAsStringAsync().Result);

                    resultList.AddRange(myList.data);

                    for (int pageNumber = 2; pageNumber <= res.total_pages; pageNumber++ )
                    {
                        query = FormatQueryString(s, pageNumber);

                        request = new HttpRequestMessage(HttpMethod.Get, query);
                        response = restClient.SendAsync(request).Result;

                        if (!response.IsSuccessStatusCode)
                        {
                            break;
                        }
                        else
                        {
                            myList = JsonConvert.DeserializeObject<CountryResponse>(response.Content.ReadAsStringAsync().Result);

                            resultList.AddRange(myList.data);

                        }
                    }                  
                }             
            }

            return resultList.Where(x => x.population > p).Count();
        }
    }
}
