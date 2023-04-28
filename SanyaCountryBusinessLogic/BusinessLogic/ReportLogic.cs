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
                    Buildings = new List<Tuple<string, int>>(),
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
                        record.Buildings.Add(new Tuple<string, int>(building.Value.Item1,
                        building.Value.Item2));
                        record.TotalCount += building.Value.Item2;
                    }
                }
                list.Add(record);
            }
            return list;
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

        public void SaveReportData(ReportBindingModel model)
        {
            var list = GetSettlementBuildings(model);
            if (list == null)
            {
                return;
            }
            try
            {
                var dirInfo = new DirectoryInfo(model.FolderName);
                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                string fileName = $"{model.FolderName}.zip";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                var xmlSerializer = new XmlSerializer(typeof(ReportSettlementBuildingViewModel));
                var obj = new ReportSettlementBuildingViewModel();
                using var fs = new FileStream(string.Format("{0}/{1}.xml",
                model.FolderName, obj.GetType().Name), FileMode.OpenOrCreate);
                xmlSerializer.Serialize(fs, list);
                ZipFile.CreateFromDirectory(model.FolderName, fileName);
                dirInfo.Delete(true);
            }
            catch (Exception)
            {
                // делаем проброс
                throw;
            }
        }
    }
}
