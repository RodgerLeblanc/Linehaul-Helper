using System.Collections.Generic;
using System.Threading.Tasks;
using Linehaul_Helper.Models;
using System;

namespace Linehaul_Helper.Interfaces
{
    public interface IDatabaseService
    {
        event EventHandler UnitInfoListDateTimeChanged;
        event EventHandler IsBusyChanged;

        DateTime UnitInfoListDateTime { get; }
        bool IsBusy { get; }

        Task<List<UnitInfo>> GetUnitInfos();
        Task<List<UnitInfo>> GetUnitInfosOnline();
        Task<bool> UpdateDocument(Document document);
    }
}