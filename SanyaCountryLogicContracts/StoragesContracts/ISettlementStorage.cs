using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.ViewModels;

namespace SanyaCountryLogicContracts.StoragesContracts
{
    public interface ISettlementStorage
    {
        List<SettlementViewModel> GetFullList();
        List<SettlementViewModel> GetFilteredList(SettlementBindingModel model);

        SettlementViewModel GetElement(SettlementBindingModel model);

        void Insert(SettlementBindingModel model);

        void Update(SettlementBindingModel model);

        void Delete(SettlementBindingModel model);
    }
}
