using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryDatabaseImplement.Models
{
    public class SettlementBuilding
    {
        public int Id { get; set; }
        public int SettlementId { get; set; }
        public int BuildingId { get; set; }

        [Required]
        public int Count { get; set; }
        public virtual Settlement Settlement { get; set; }
        public virtual Building Building { get; set; }
    }
}
