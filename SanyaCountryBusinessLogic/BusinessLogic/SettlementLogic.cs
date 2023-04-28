using SanyaCountryLogicContracts.BindingModels;
using SanyaCountryLogicContracts.BusinessLogicsContracts;
using SanyaCountryLogicContracts.StoragesContracts;
using SanyaCountryLogicContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SanyaCountryBusinessLogic.BusinessLogic
{
    public class SettlementLogic : ISettlementLogic
    {
        private readonly ISettlementStorage _settlementStorage;

        public SettlementLogic(ISettlementStorage settlementStorage)
        {
            _settlementStorage = settlementStorage;
        }

        public void CreateOrUpdate(SettlementBindingModel model)
        {
            var element = _settlementStorage.GetElement(new SettlementBindingModel
            {
                Name = model.Name,
                Type = model.Type,
                Buildings = model.Buildings
            });

            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть поселение с таким названием");
            }
            //Проверяет на то, чтобы название начиналось с заглавной буквы и имело хотя бы одну цифру
            if (!Regex.IsMatch(model.Name, @"^[A-Z]+\w*\d+\w*$"))
            {
                throw new Exception("Имя должно начинаться с заглавной буквы");
            }

            if (model.Id.HasValue)
            {
                _settlementStorage.Update(model);
            }
            else
            {
                _settlementStorage.Insert(model);
            }
        }

        public void Delete(SettlementBindingModel model)
        {
            var element = _settlementStorage.GetElement(new SettlementBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            _settlementStorage.Delete(model);
        }

        public List<SettlementViewModel> Read(SettlementBindingModel model)
        {
            if (model == null)
            {
                return _settlementStorage.GetFullList();
            }

            if (model.Id.HasValue)
            {
                return new List<SettlementViewModel> { _settlementStorage.GetElement(model) };
            }

            return _settlementStorage.GetFilteredList(model);
        }
    }
}
