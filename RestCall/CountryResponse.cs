using System.Collections.Generic;

namespace RestCall
{
    public class CountryResponse
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<CountryDataResponse> data { get; set; }
    }
}
