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
    [BuyerInterface("Интерфейс для работы с клиентами")]
    public interface IBuyerService
    {
        [BuyerMethod("Метод получения списка клиентов")]
        List<BuyerViewModel> GetList();
        [BuyerMethod("Метод получения клиента по id")]
        BuyerViewModel GetElement(int id);
        [BuyerMethod("Метод добавления клиента")]
        void AddElement(BuyerBindingModel model);
        [BuyerMethod("Метод изменения данных по клиенту")]
        void UpdElement(BuyerBindingModel model);
        [BuyerMethod("Метод удаления клиента")]
        void DelElement(int id);
    }
}
