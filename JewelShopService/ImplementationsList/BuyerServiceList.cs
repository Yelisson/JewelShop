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
            List<BuyerViewModel> result = new List<BuyerViewModel>();
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                result.Add(new BuyerViewModel
                {
                    id = source.Buyers[i].id,
                    buyerFIO = source.Buyers[i].buyerFIO
                });
            }

            return result;
        }

        public BuyerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].id == id)
                {
                    return new BuyerViewModel
                    {
                        id = source.Buyers[i].id,
                        buyerFIO = source.Buyers[i].buyerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(BuyerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].id > maxId)
                {
                    maxId = source.Buyers[i].id;
                }
                if (source.Buyers[i].buyerFIO == model.buyerFIO)
                {
                    throw new Exception("Клиент с таким ФИО уже есть");
                }
            }
            source.Buyers.Add(new Buyer
            {
                id = maxId + 1,
                buyerFIO = model.buyerFIO
            });
        }


        public void UpdElement(BuyerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].id == model.id)
                {
                    index = i;
                }
                if (source.Buyers[i].buyerFIO == model.buyerFIO &&
                    source.Buyers[i].id != model.id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Buyers[index].buyerFIO = model.buyerFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Buyers.Count; ++i)
            {
                if (source.Buyers[i].id == id)
                {
                    source.Buyers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
