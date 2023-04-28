using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanyaCountryDatabaseImplement.Models;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SanyaCountryDatabaseImplement.Implements
{
    public class SettlementStorage : ISettlementStorage
    {
        public void Delete(SettlementBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            var element = context.Settlements
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Settlements.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public SettlementViewModel GetElement(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SanyaCountryDatabase();
            var element = context.Settlements
            .Include(rec => rec.SettlementBuildings)
            .ThenInclude(rec => rec.Building)
            .FirstOrDefault(rec => rec.Name == model.Name ||
            rec.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<SettlementViewModel> GetFilteredList(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new SanyaCountryDatabase();
            return context.Settlements
            .Include(rec => rec.SettlementBuildings)
            .ThenInclude(rec => rec.Building)
            .Where(rec => rec.Name.Contains(model.Name))
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public List<SettlementViewModel> GetFullList()
        {
            using var context = new SanyaCountryDatabase();
            return context.Settlements
            .Include(rec => rec.SettlementBuildings)
            .ThenInclude(rec => rec.Building)
            .ToList()
            .Select(CreateModel)
            .ToList();
        }

        public void Insert(SettlementBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                //Сначала надо создать значение в таблице Settlement,
                //а уже потом добавлять внешние ключи в таблицу SettlementBuilding
                var element = new Settlement()
                {
                    Name = model.Name,
                    Type = model.Type
                };
                context.Settlements.Add(element);
                context.SaveChanges();
                CreateModel(model, element, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(SettlementBindingModel model)
        {
            using var context = new SanyaCountryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Settlements
                    .FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static Settlement CreateModel(SettlementBindingModel model, Settlement settlement,
            SanyaCountryDatabase context)
        {
            settlement.Name = model.Name;
            settlement.Type = model.Type;

            if (model.Id.HasValue)
            {
                var settlementBuildings = context.SettlementBuildings
                    .Where(rec => rec.SettlementId == model.Id.Value)
                    .ToList();
                // удалили те, которых нет в модели
                context.SettlementBuildings
                    .RemoveRange(settlementBuildings
                    .Where(rec => !model.Buildings.ContainsKey(rec.BuildingId))
                    .ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateBuilding in settlementBuildings)
                {
                    updateBuilding.Count =
                    model.Buildings[updateBuilding.BuildingId].Item2;
                    model.Buildings.Remove(updateBuilding.BuildingId);
                }
                context.SaveChanges();
            }

            // добавили новые значения в таблицу SettlementBuildings
            foreach (var sb in model.Buildings)
            {
                context.SettlementBuildings.Add(new SettlementBuilding
                {
                    SettlementId = settlement.Id,
                    BuildingId = sb.Key,
                    Count = sb.Value.Item2
                });
                context.SaveChanges();
            }
            return settlement;
        }

        private SettlementViewModel CreateModel(Settlement settlement)
        {
            return new SettlementViewModel
            {
                Id = settlement.Id,
                Name = settlement.Name,
                Type = settlement.Type.ToString(),
                Buildings = settlement.SettlementBuildings
                .ToDictionary(x => x.BuildingId, x => (x.Building?.Name, x.Count))
            };
        }
    }
}
