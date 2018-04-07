using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using JewelShopService.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelShopModel;

namespace JewelShopService.ImplementationsList
{
    public class BuyerServiceList : IBuyerService
    {
        private DataListSingleton source;
        public BuyerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<BuyerViewModel> GetList()
        {
            List<BuyerViewModel> result = source.Buyers
              .Select(rec => new BuyerViewModel
              {
                  id = rec.id,
                  buyerFIO = rec.buyerFIO
              })
              .ToList();
            return result;
        }

        public BuyerViewModel GetElement(int id)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new BuyerViewModel
                {
                    id = element.id,
                    buyerFIO = element.buyerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BuyerBindingModel model)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.buyerFIO == model.buyerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Buyers.Count > 0 ? source.Buyers.Max(rec => rec.id) : 0;
            source.Buyers.Add(new Buyer
            {
                id = maxId + 1,
                buyerFIO = model.buyerFIO
            });
        }


        public void UpdElement(BuyerBindingModel model)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec =>
                                   rec.buyerFIO == model.buyerFIO && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Buyers.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.buyerFIO = model.buyerFIO;
        }

        public void DelElement(int id)
        {
            Buyer element = source.Buyers.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Buyers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
