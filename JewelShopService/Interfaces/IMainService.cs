using JewelShopService.Attributies;
using JewelShopService.BindingModels;
using JewelShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.Interfaces
{
    [BuyerInterface("Интерфейс для работы с заказами")]
    public interface IMainService
    {
        [BuyerMethod("Метод получения списка заказов")]
        List<ProdOrderViewModel> GetList();
        [BuyerMethod("Метод создания заказа")]
        void CreateOrder(ProdOrderBindingModel model);
        [BuyerMethod("Метод передачи заказа в работу")]
        void TakeOrderInWork(ProdOrderBindingModel model);
        [BuyerMethod("Метод передачи заказа на оплату")]
        void FinishOrder(int id);
        [BuyerMethod("Метод фиксирования оплаты по заказу")]
        void PayOrder(int id);
        [BuyerMethod("Метод пополнения компонент на складе")]
        void PutComponentOnStock(HangarElementBindingModel model);
    }
}
