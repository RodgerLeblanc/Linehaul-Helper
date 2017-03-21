using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Models
{
    public class UnitInfo
    {
        public int UnitNumber { get; set; }
        public String PlateNumber { get; set; }
        public bool RevisionNeeded { get; set; }
        public String RevisedPlateNumber { get; set; }
    }
}
