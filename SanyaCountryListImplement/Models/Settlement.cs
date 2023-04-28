using SanyaCountryLogicContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryListImplement.Models
{
    public class Settlement
    {
        //Номер
        public int Id { get; set; }
        //Название
        public string Name { get; set; }
        //Тип поселения
        public SettlementType Type { get; set; }
        //Строения в поселении
        public Dictionary<int, int> Buildings { get; set; }
    }
}
