using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryListImplement.Models
{
    public class Building
    {
        //Номер
        public int Id { get; set; }
        //Название вида дома
        public string Name { get; set; }
        //Стоимость
        public double Price { get; set; }
        //Дата постройки
        public DateTime? Created { get; set; }
        //Площадь здания
        public double Square { get; set; }
    }
}
