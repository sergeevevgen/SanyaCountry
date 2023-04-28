using SanyaCountryListImplement.Models;

namespace SanyaCountryListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Building> Buildings { get; set; }
        public List<Settlement> Settlements { get; set; }

        private DataListSingleton()
        {
            Buildings = new List<Building>();
            Settlements = new List<Settlement>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}