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
        public int Id { get; set; }
        //Название
        [DisplayName("Название")]
        public string Name { get; set; }
        //Тип поселения
        [DisplayName("Тип поселения")]
        public string Type { get; set; }
        //Строения в поселении
        public Dictionary<int, (string, int)> Buildings { get; set; }
    }
}
