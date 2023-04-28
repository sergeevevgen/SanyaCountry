using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.BindingModels
{
    //Строение в поселении
    public class BuildingBindingModel
    {
        //Номер
        public int? Id { get; set; }
        //Улица
        public string Street { get; set; }
        //Дом
        public string House { get; set; }
        //Стоимость
        public double? Price { get; set; }
        //Дата постройки
        public DateTime? Created { get; set; }
        //Площадь здания
        public double? Square { get; set; }
    }
}
