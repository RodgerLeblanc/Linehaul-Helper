using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    class WarehouseLocation
    {
        public string Name;
        public string Description;
        public string Address;
        public Position Position;
    }

    class Position
    {
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public readonly double Latitude;
        public readonly double Longitude;
    }
}
