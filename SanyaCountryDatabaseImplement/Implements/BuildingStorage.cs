using SanyaCountryDatabaseImplement.Models;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaCountryDatabaseImplement.Implements
{
    public class BuildingStorage : IBuildingStorage
    {
        public void Delete(BuildingBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            var element = context.Buildings
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Buildings.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public BuildingViewModel GetElement(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SanyaCountryDatabase();
            var element = context.Buildings
                .FirstOrDefault(rec => rec.Name == model.Name 
                || rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<BuildingViewModel> GetFilteredList(BuildingBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SanyaCountryDatabase();
            return context.Buildings
                .Where(rec => rec.Name.Contains(model.Name))
                .Select(CreateModel)
                .ToList();
        }

        public List<BuildingViewModel> GetFullList()
        {
            using var context = new SanyaCountryDatabase();
            return context.Buildings
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(BuildingBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            context.Buildings.Add(CreateModel(model, new Building()));
            context.SaveChanges();
        }

        public void Update(BuildingBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            var element = context.Buildings
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        private static Building CreateModel(BuildingBindingModel model, 
            Building building)
        {
            building.Name = model.Name;
            building.Created = model.Created;
            building.Price = model.Price;
            building.Square = model.Square;
            return building;
        }

        private static BuildingViewModel CreateModel(Building building)
        {
            return new BuildingViewModel
            {
                Id = building.Id,
                Name = building.Name,
                Created = building.Created.HasValue ? building.Created.Value : null,
                Price = building.Price,
                Square = building.Square
            };
        }
    }
}
