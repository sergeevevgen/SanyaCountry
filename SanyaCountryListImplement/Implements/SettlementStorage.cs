using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SanyaCountryListImplement.Models;
using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;

namespace SanyaCountryListImplement.Implements
{
    public class SettlementStorage : ISettlementStorage
    {
        private readonly DataListSingleton source;

        public SettlementStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public void Delete(SettlementBindingModel model)
        {
            var element = source.Settlements
                .FirstOrDefault(x => x.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            source.Settlements.Remove(element);
        }

        public SettlementViewModel GetElement(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var element = source.Settlements
                .FirstOrDefault(x => x.Name == model.Name || x.Id == model.Id);
            return element != null ? CreateModel(element) : null;
        }

        public List<SettlementViewModel> GetFilteredList(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            
            return source.Settlements
                .Where(rec => rec.Name.Contains(model.Name))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<SettlementViewModel> GetFullList()
        {
            return source.Settlements
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public void Insert(SettlementBindingModel model)
        {
            int id = 1;
            if (source.Settlements.Count > 0)
            {
                id = source.Settlements.Max(x => x.Id) + 1;
            }
            var tmp = new Settlement
            {
                Id = id,
                Buildings = new Dictionary<int, int>()
            };
            source.Settlements.Add(CreateModel(model, tmp));
        }

        public void Update(SettlementBindingModel model)
        {
            var element = source.Settlements.FirstOrDefault(rec => rec.Id == model.Id);

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private static Settlement CreateModel(SettlementBindingModel model, Settlement settlement)
        {
            settlement.Name = model.Name;
            settlement.Type = model.Type;

            // удаляем убранные
            foreach (var key in settlement.Buildings.Keys.ToList())
            {
                if (!model.Buildings.ContainsKey(key))
                {
                    settlement.Buildings.Remove(key);
                }
            }

            // обновляем существующие и добавляем новые
            foreach (var b in model.Buildings)
            {
                if (settlement.Buildings.ContainsKey(b.Key))
                {
                    settlement.Buildings[b.Key] =
                    model.Buildings[b.Key].Item2;
                }
                else
                {
                    settlement.Buildings.Add(b.Key,
                    model.Buildings[b.Key].Item2);
                }
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
                Buildings = settlement.Buildings
                .ToDictionary(x => x.Key, 
                x => (source.Buildings.FirstOrDefault(xb => xb.Id == x.Key).Name, x.Value))
            };
        }
    }
}
