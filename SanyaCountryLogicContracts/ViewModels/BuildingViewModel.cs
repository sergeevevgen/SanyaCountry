using SanyaCountryContracts.Attributes;
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
        [Column(title: "Номер", width: 50)]
        public int Id { get; set; }
        //Название вида дома
        [Column(title: "Название", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Name { get; set; }
        //Стоимость
        [Column(title: "Стоимость", width: 80)]
        public double Price { get; set; }
        //Дата постройки
        [Column(title: "Дата создания", gridViewAutoSize: GridViewAutoSize.Fill)]
        public DateTime? Created { get; set; }
        //Площадь здания
        [Column(title: "Площадь", width: 80)]
        public double Square { get; set; }
    }
}
