using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.StoragesContracts
{
    public interface IBuildingStorage
    {
        List<BuildingViewModel> GetFullList();
        List<BuildingViewModel> GetFilteredList(BuildingBindingModel model);

        BuildingViewModel GetElement(BuildingBindingModel model);

        void Insert(BuildingBindingModel model);

        void Update(BuildingBindingModel model);

        void Delete(BuildingBindingModel model);
    }
}
