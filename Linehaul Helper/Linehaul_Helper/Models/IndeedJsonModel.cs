using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    public class IndeedJob
    {
        public string jobtitle { get; set; }
        public string company { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string formattedLocation { get; set; }
        public string source { get; set; }
        public string date { get; set; }
        public string snippet { get; set; }
        public string url { get; set; }
        public string onmousedown { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string jobkey { get; set; }
        public bool sponsored { get; set; }
        public bool expired { get; set; }
        public bool indeedApply { get; set; }
        public string formattedLocationFull { get; set; }
        public string formattedRelativeTime { get; set; }
        public string stations { get; set; }
    }

    public class IndeedJsonResponse
    {
        public int version { get; set; }
        public string query { get; set; }
        public string location { get; set; }
        public string paginationPayload { get; set; }
        public bool dupefilter { get; set; }
        public bool highlight { get; set; }
        public int totalResults { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int pageNumber { get; set; }
        public List<IndeedJob> results { get; set; }
    }
}
