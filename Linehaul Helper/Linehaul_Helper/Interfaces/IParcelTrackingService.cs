using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Interfaces
{
    public interface IParcelTrackingService
    {
        event EventHandler IsBusyChanged;
        Task<ParcelTrackingModel> Track(string trackingNumber);
    }
}
