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
            for (int i = 0; i < source.Buildings.Count; ++i)
            {
                if (source.Buildings[i].Id == model.Id.Value)
                {
                    source.Buildings.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public BuildingViewModel GetElement(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            foreach (var b in source.Buildings)
            {
                if (b.Id == model.Id || b.Name ==
                    model.Name)
                {
                    return CreateModel(b);
                }
            }
            return null;
        }

        public List<BuildingViewModel> GetFilteredList(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var result = new List<BuildingViewModel>();
            foreach (var b in source.Buildings)
            {
                if (b.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(b));
                }
            }
            return result;
        }

        public List<BuildingViewModel> GetFullList()
        {
            var result = new List<BuildingViewModel>();
            foreach (var b in source.Buildings)
            {
                result.Add(CreateModel(b));
            }
            return result;
        }

        public void Insert(BuildingBindingModel model)
        {
            var tmp = new Building { Id = 1 };
            foreach (var b in source.Buildings)
            {
                if (b.Id >= tmp.Id)
                {
                    tmp.Id = b.Id + 1;
                }
            }
            source.Buildings.Add(CreateModel(model, tmp));
        }

        public void Update(BuildingBindingModel model)
        {
            Building tmp = null;
            foreach (var b in source.Buildings)
            {
                if (b.Id == model.Id)
                {
                    tmp = b;
                    break;
                }
            }

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
            building.Created = model.Created;
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
                Created = building.Created,
                Square = building.Square
            };
        }
    }
}
