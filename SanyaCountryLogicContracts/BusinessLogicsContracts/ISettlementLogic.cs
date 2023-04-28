using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryLogicContracts.BusinessLogicsContracts
{
    public interface ISettlementLogic
    {
        List<SettlementViewModel> Read(SettlementBindingModel model);

        void CreateOrUpdate(SettlementBindingModel model);

        void Delete(SettlementBindingModel model);
    }
}
