using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.ViewModels
{
    //Строение - ViewModel
    public class BuildingViewModel
    {
        //Номер
        public int Id { get; set; }
        //Название вида дома
        [DisplayName("Название")]
        public string Name { get; set; }
        ////Улица
        //public string Street { get; set; }
        ////Дом
        //public string House { get; set; }
        //Стоимость
        [DisplayName("Стоимость")]
        public double Price { get; set; }
        //Дата постройки
        [DisplayName("Дата создания")]
        public DateTime? Created { get; set; }
        //Площадь здания
        [DisplayName("Площадь")]
        public double Square { get; set; }
    }
}
