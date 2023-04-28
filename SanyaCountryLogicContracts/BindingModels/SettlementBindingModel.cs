using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.BindingModels
{
    //Поселение (в нём находятся несколько строений)
    public class SettlementBindingModel
    {
        //Номер
        public int? Id { get; set; }
        //Название
        public string Name { get; set; }
        public string CoordinateX { get; set; }
        public string CoordinateY { get; set; }
    }
}
