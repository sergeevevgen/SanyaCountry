using SanyaCountryContracts.Attributes;
using SanyaCountryLogicContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.ViewModels
{
    //Поселение (в нём находятся несколько строений разных типов)
    public class SettlementViewModel
    {
        //Номер
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        //Название
        [Column(title: "Название поселения", gridViewAutoSize: GridViewAutoSize.DisplayedCells)]
        public string Name { get; set; }
        //Тип поселения
        [Column(title: "Тип поселения", gridViewAutoSize: GridViewAutoSize.DisplayedCells)]
        public string Type { get; set; }
        //Строения в поселении
        public Dictionary<int, (string, int)> Buildings { get; set; }
    }
}
