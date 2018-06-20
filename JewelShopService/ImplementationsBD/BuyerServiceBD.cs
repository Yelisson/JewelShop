using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelShopModel;
using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;

namespace JewelShopService.ImplementationsBD
{
   public class BuyerServiceBD:IBuyerService
    {
        private AbstractDataBaseContext context;

        public BuyerServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<BuyerViewModel> GetList()
        {
            List<BuyerViewModel> result = context.Buyers
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
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.id == id);
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
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.buyerFIO == model.buyerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Buyers.Add(new Buyer
            {
                buyerFIO = model.buyerFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(BuyerBindingModel model)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec =>
                                    rec.buyerFIO == model.buyerFIO && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Buyers.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.buyerFIO = model.buyerFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Buyer element = context.Buyers.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                context.Buyers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
