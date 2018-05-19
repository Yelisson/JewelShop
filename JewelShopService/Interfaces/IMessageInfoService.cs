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
    [BuyerInterface("Интерфейс для работы с письмами")]
    public interface IMessageInfoService
    {
        [BuyerMethod("Метод получения списка писем")]
        List<MessageInfoViewModel> GetList();
        [BuyerMethod("Метод получения письма по id")]
        MessageInfoViewModel GetElement(int id);
        [BuyerMethod("Метод добавления письма")]
        void AddElement(MessageInfoBindingModel model);
    }
}
