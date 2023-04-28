using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SanyaCountryContracts.ViewModels
{
    public class ReportSettlementBuildingViewModel
    {
        //Этот атрибут указывает, какое поле не сериализуется
        [XmlIgnore]
        public string SettlementName { get; set; }
        
        public int TotalCount { get; set; }
        
        public List<(string, int)> Buildings { get; set; }

        public ReportSettlementBuildingViewModel()
        {
            Buildings = new List<(string, int)>();
        }
    }
}
