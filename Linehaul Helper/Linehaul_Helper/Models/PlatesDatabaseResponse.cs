using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    public class PlatesDatabaseResponse
    {
        [JsonProperty("total_rows")]
        public int TotalRows { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        [JsonProperty("id")]
        public string RowId { get; set; }

        [JsonProperty("key")]
        public string RowKey { get; set; }

        [JsonProperty("value")]
        public Value RowValue { get; set; }

        [JsonProperty("doc")]
        public Document RowDoc { get; set; }
    }

    public class Value
    {
        [JsonProperty("rev")]
        public string ValueRev { get; set; }
    }

    public class Document
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_rev")]
        public string Rev { get; set; }

        [JsonProperty("UnitInfo")]
        public UnitInfo UnitInfo { get; set; }
    }
}
