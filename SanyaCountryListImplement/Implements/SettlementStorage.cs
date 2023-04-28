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
            for (int i = 0; i < source.Settlements.Count; ++i)
            {
                if (source.Settlements[i].Id == model.Id)
                {
                    source.Settlements.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public SettlementViewModel GetElement(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var s in source.Settlements)
            {
                if (s.Id == model.Id || s.Name ==
                model.Name)
                {
                    return CreateModel(s);
                }
            }
            return null;
        }

        public List<SettlementViewModel> GetFilteredList(SettlementBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<SettlementViewModel>();
            foreach (var s in source.Settlements)
            {
                if (s.Name.Contains(model.Name))
                {
                    result.Add(CreateModel(s));
                }
            }
            return result;
        }

        public List<SettlementViewModel> GetFullList()
        {
            var result = new List<SettlementViewModel>();
            foreach (var s in source.Settlements)
            {
                result.Add(CreateModel(s));
            }
            return result;
        }

        public void Insert(SettlementBindingModel model)
        {
            var tmp = new Settlement
            {
                Id = 1,
                Buildings = new Dictionary<int, int>()
            };

            foreach (var s in source.Settlements)
            {
                if (s.Id >= tmp.Id)
                {
                    tmp.Id = s.Id + 1;
                }
            }
            source.Settlements.Add(CreateModel(model, tmp));
        }

        public void Update(SettlementBindingModel model)
        {
            Settlement tmp = null;
            foreach (var s in source.Settlements)
            {
                if (s.Id == model.Id)
                {
                    tmp = s;
                    break;
                }
            }

            if (tmp == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tmp);
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
            // требуется дополнительно получить список строений для поселения с
            //названиями и их количество
            var buildings = new Dictionary<int, (string, int)>();
            foreach (var b in settlement.Buildings)
            {
                string bName = string.Empty;
                foreach (var building in source.Buildings)
                {
                    if (b.Key == building.Id)
                    {
                        bName = building.Name;
                        break;
                    }
                }
                buildings.Add(b.Key, (bName, b.Value));
            }

            return new SettlementViewModel
            {
                Id = settlement.Id,
                Name = settlement.Name,
                Type = settlement.Type.ToString(),
                Buildings = buildings
            };
        }
    }
}
