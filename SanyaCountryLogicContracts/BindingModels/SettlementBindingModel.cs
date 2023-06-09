﻿using SanyaCountryLogicContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.BindingModels
{
    //Поселение (в нём находятся несколько строений разных типов)
    public class SettlementBindingModel
    {
        //Номер
        public int? Id { get; set; }
        //Название
        public string Name { get; set; }
        //Тип поселения
        public SettlementType Type { get; set; }
        //Строения в поселении
        public Dictionary<int, (string, int)> Buildings { get; set; }
    }
}
