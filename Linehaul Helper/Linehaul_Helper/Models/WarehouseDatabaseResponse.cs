using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    public class WarehouseDatabaseResponse
    {
        public class Value
        {
            public string rev { get; set; }
        }

        public class Position
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public class Doc
        {
            public string _id { get; set; }
            public string _rev { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public Position Position { get; set; }
            public string Address { get; set; }
        }

        public class Row
        {
            public string id { get; set; }
            public string key { get; set; }
            public Value value { get; set; }
            public WarehouseLocation doc { get; set; }
        }

        public class RootObject
        {
            public int total_rows { get; set; }
            public int offset { get; set; }
            public List<Row> rows { get; set; }
        }
    }
}
