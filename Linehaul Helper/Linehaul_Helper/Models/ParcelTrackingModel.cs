using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    public class ParcelTrackingModel
    {
        public string TrackingNumber { get; set; }
        public string Division { get; set; }
        public string Status { get; set; }
        public string LastUpdateString { get; set; }
        public string Notes { get; set; }
    }
}
