using JewelShopModel;
using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.ImplementationsList
{
    public class MainServiceList: IMainService
    {
        private DataListSingleton source;

        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ProdOrderViewModel> GetList()
        {
            List<ProdOrderViewModel> result = source.ProdOrders
                .Select(rec => new ProdOrderViewModel
                {
                    id = rec.id,
                    buyerId = rec.buyerId,
                    adornmentId = rec.adornmentId,
                    customerId = rec.customerId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateCustom = rec.DateCustom?.ToLongDateString(),
                    status = rec.status.ToString(),
                    count = rec.count,
                    sum = rec.sum,
                    buyerFIO = source.Buyers
                                    .FirstOrDefault(recC => recC.id == rec.buyerId)?.buyerFIO,
                    adornmentName = source.Adornments
                                    .FirstOrDefault(recP => recP.id == rec.adornmentId)?.adornmentName,
                    customerName = source.Customers
                                    .FirstOrDefault(recI => recI.id == rec.customerId)?.customerFIO
                })
                .ToList();
            return result;
        }
        public void CreateOrder(ProdOrderBindingModel model)
        {
            int maxId = source.ProdOrders.Count > 0 ? source.ProdOrders.Max(rec => rec.id) : 0;
            source.ProdOrders.Add(new ProdOrder
            {
                id = maxId + 1,
                buyerId = model.buyerId,
                adornmentId = model.adornmentId,
                DateCreate = DateTime.Now,
                count = model.count,
                sum = model.sum,
                status = ProdOrderStatus.Принят
            });
        }

        public void TakeOrderInWork(ProdOrderBindingModel model)
        {
            ProdOrder element = source.ProdOrders.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            var productComponents = source.AdornmentElements.Where(rec => rec.adornmentId == element.adornmentId);
            foreach (var productComponent in productComponents)
            {
                int countOnStocks = source.HangarElements
                                            .Where(rec => rec.elementtId == productComponent.elementId)
                                            .Sum(rec => rec.count);
                if (countOnStocks < productComponent.count * element.count)
                {
                    var componentName = source.Elements
                                    .FirstOrDefault(rec => rec.id == productComponent.elementId);
                    throw new Exception("Не достаточно компонента " + componentName?.elementName +
                        " требуется " + productComponent.count + ", в наличии " + countOnStocks);
                }
            }
            foreach (var productComponent in productComponents)
            {
                int countOnStocks = productComponent.count * element.count;
                var stockComponents = source.HangarElements
                                            .Where(rec => rec.elementtId == productComponent.elementId);
                foreach (var stockComponent in stockComponents)
                {
                    if (stockComponent.count >= countOnStocks)
                    {
                        stockComponent.count -= countOnStocks;
                        break;
                    }
                    else
                    {
                        countOnStocks -= stockComponent.count;
                        stockComponent.count = 0;
                    }
                }
            }
            element.customerId = model.customerId;
            element.DateCustom = DateTime.Now;
            element.status = ProdOrderStatus.Выполняетя;
        }

        public void FinishOrder(int id)
        {
            ProdOrder element = source.ProdOrders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = ProdOrderStatus.Готов;
        }

        public void PayOrder(int id)
        {
            ProdOrder element = source.ProdOrders.FirstOrDefault(rec => rec.id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.status = ProdOrderStatus.Оплачен;
        }

        public void PutComponentOnStock(HangarElementBindingModel model)
        {
            HangarElement element = source.HangarElements
                                               .FirstOrDefault(rec => rec.hangarId == model.hangarId &&
                                                                   rec.elementtId == model.elementId);
            if (element != null)
            {
                element.count += model.Count;
            }
            else
            {
                int maxId = source.HangarElements.Count > 0 ? source.HangarElements.Max(rec => rec.id) : 0;
                source.HangarElements.Add(new HangarElement
                {
                    id = ++maxId,
                    hangarId = model.hangarId,
                    elementtId = model.elementId,
                    count = model.Count
                });
            }
        }
    }
}
