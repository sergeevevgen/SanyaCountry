using Microsoft.EntityFrameworkCore;
using SanyaCountryDatabaseImplement.Models;

namespace SanyaCountryDatabaseImplement
{
    public class SanyaCountryDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SanyaCountryDatabaseDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Building> Buildings { set; get; }
        public virtual DbSet<Settlement> Settlements { set; get; }
        public virtual DbSet<SettlementBuilding> SettlementBuildings { set; get; }
    }
}