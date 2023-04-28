using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.BusinessLogicsContracts
{
    public interface IBuildingLogic
    {
        List<BuildingViewModel> Read(BuildingBindingModel model);

        void CreateOrUpdate(BuildingBindingModel model);

        void Delete(BuildingBindingModel model);
    }
}
