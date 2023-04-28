using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanyaCountryContracts.BindingModels;
using SanyaCountryContracts.ViewModels;

namespace SanyaCountryContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        /// <summary>
        /// Получение списка блюд с указанием, какие ингредиенты в них используются
        /// </summary>
        /// <returns></returns>
        List<ReportSettlementBuildingViewModel> GetSettlementBuildings(ReportBindingModel model);


        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        void SaveSettlementBuildingsToPdfFile(ReportBindingModel model);
    }
}
