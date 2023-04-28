using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.BusinessLogicsContracts;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryBusinessLogic.BusinessLogic
{
    public class BuildingLogic : IBuildingLogic
    {
        private readonly IBuildingStorage _buildingStorage;

        public BuildingLogic(IBuildingStorage buildingStorage)
        {
            _buildingStorage = buildingStorage;
        }

        //Метод для создания и обновления данных в хранилище
        public void CreateOrUpdate(BuildingBindingModel model)
        {
            var element = _buildingStorage.GetElement(new BuildingBindingModel
            {
                Name = model.Name
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть строение с таким названием");
            }

            if (model.Id.HasValue)
            {
                _buildingStorage.Update(model);
            }
            else
            {
                model.Created = DateTime.Now;
                _buildingStorage.Insert(model);
            }
        }

        //Метод для удаления записи из хранилища
        public void Delete(BuildingBindingModel model)
        {
            var element = _buildingStorage.GetElement(new BuildingBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _buildingStorage.Delete(model);
        }

        //Метод получения данных из хранилища
        public List<BuildingViewModel> Read(BuildingBindingModel model)
        {
            if (model == null)
            {
                return _buildingStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<BuildingViewModel> { _buildingStorage.GetElement(model) };
            }

            return _buildingStorage.GetFilteredList(model);
        }
    }
}
