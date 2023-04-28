using SanyaCountryListImplement.Models;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryListImplement.Implements
{
    public class BuildingStorage : IBuildingStorage
    {
        private readonly DataListSingleton source;

        public BuildingStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(BuildingBindingModel model)
        {
            var building = source.Buildings.FirstOrDefault(x => x.Id == model.Id.Value);
            if (building == null)
            {
                throw new Exception("Элемент не найден");
            }
            source.Buildings.Remove(building);
        }

        public BuildingViewModel GetElement(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var building = source.Buildings
                .FirstOrDefault(x => x.Name == model.Name || x.Id == model.Id);
            return building != null ? CreateModel(building) : null;
        }

        public List<BuildingViewModel> GetFilteredList(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            return source.Buildings
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<BuildingViewModel> GetFullList()
        {
            return source.Buildings
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(BuildingBindingModel model)
        {
            int id = 1;
            if (source.Buildings.Count > 0)
            {
                id = source.Buildings.Max(x => x.Id) + 1;
            }
            
            var tmp = new Building { Id = id };
            source.Buildings.Add(CreateModel(model, tmp));
        }

        public void Update(BuildingBindingModel model)
        {
            var tmp = source.Buildings
                .FirstOrDefault(rec => rec.Id == model.Id);

            if (tmp == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, tmp);
        }

        private static Building CreateModel(BuildingBindingModel model, Building building)
        {
            building.Name = model.Name;
            building.Price = model.Price;
            building.Created = model.Created.Value;
            building.Square = model.Square;
            return building;
        }

        private static BuildingViewModel CreateModel(Building building)
        {
            return new BuildingViewModel
            {
                Id = building.Id,
                Name = building.Name,
                Price = building.Price,
                Created = building.Created.Value,
                Square = building.Square
            };
        }
    }
}
