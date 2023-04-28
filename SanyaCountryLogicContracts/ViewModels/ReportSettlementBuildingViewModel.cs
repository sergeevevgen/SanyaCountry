using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryContracts.ViewModels
{
    public class ReportSettlementBuildingViewModel
    {
        public string SettlementName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Buildings { get; set; }
    }
}
