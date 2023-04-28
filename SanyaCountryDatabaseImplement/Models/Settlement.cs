using SanyaCountryLogicContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanyaCountryDatabaseImplement.Models
{
    public class Settlement
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public SettlementType Type { get; set; }

        //Внешний ключ связь один-ко-многим
        [ForeignKey("SettlementId")]
        public virtual List<SettlementBuilding> SettlementBuildings { get; set; }
    }
}
