using Linehaul_Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linehaul_Helper.Interfaces
{
    public interface IWarehouseDatabaseService
    {
        event EventHandler IsBusyChanged;

        bool IsBusy { get; }

        Task<List<WarehouseLocation>> GetWarehouseLocations();
        List<WarehouseLocation> GetDefaultWarehouseLocations();
    }
}
