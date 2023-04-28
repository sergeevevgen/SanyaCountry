using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SanyaCountryDatabaseImplement.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime? Created { get; set; }

        [Required]
        public double Square { get; set; }

        [ForeignKey("BuildingId")]
        public virtual List<SettlementBuilding> SettlementBuildings { get; set; }
    }
}
