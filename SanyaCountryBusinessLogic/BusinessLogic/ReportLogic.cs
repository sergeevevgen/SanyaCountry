using SanyaCountryBusinessLogic.OfficePackage;
using SanyaCountryBusinessLogic.OfficePackage.HelperModels;
using SanyaCountryContracts.BindingModels;
using SanyaCountryContracts.BusinessLogicsContracts;
using SanyaCountryContracts.ViewModels;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.StoragesContracts;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SanyaCountryBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly ISettlementStorage _settlementStorage;
        private readonly IBuildingStorage _buildingStorage;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ISettlementStorage settlementStorage, IBuildingStorage buildingStorage,
        AbstractSaveToPdf saveToPdf)
        {
            _settlementStorage = settlementStorage;
            _buildingStorage = buildingStorage;
            _saveToPdf = saveToPdf;
        }

        public List<ReportSettlementBuildingViewModel> GetSettlementBuildings(ReportBindingModel model)
        {
            var settlements = _settlementStorage.GetFullList();
            var list = new List<ReportSettlementBuildingViewModel>();

            foreach (var s in settlements)
            {
                var record = new ReportSettlementBuildingViewModel
                {
                    SettlementName = s.Name,
                    Buildings = new List<(string, int)>(),
                    TotalCount = 0
                };
                foreach (var building in s.Buildings)
                {
                    var el = _buildingStorage.GetElement(new BuildingBindingModel
                    {
                        Id = building.Key
                    });
                    if (el != null && el.Created >= model.DateFrom)
                    {
                        record.Buildings.Add((building.Value.Item1,
                        building.Value.Item2));
                        record.TotalCount += building.Value.Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public static void SaveData(string path, object data)
        {
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(data.GetType());
                serializer.Serialize(writer, data);
            }
        }

        public static T LoadData<T>(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        public void SaveOtchet(ReportBindingModel model)
        {
            var list = GetSettlementBuildings(model);
            SaveData(model.FileName, list);
        }

        public void SaveSettlementBuildingsToPdfFile(ReportBindingModel model)
        {
            var list = GetSettlementBuildings(model);
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                SettlementBuildings = list
            });
        }
    }
}
