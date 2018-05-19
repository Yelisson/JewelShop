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
    [BuyerInterface("Интерфейс для работы со складами")]
    public interface IHangarService
    {
        [BuyerMethod("Метод получения списка складов")]
        List<HangarViewModel> GetList();
        [BuyerMethod("Метод получения склада по id")]
        HangarViewModel GetElement(int id);
        [BuyerMethod("Метод добавления склада")]
        void AddElement(HangarBindingModel model);
        [BuyerMethod("Метод изменения данных по складу")]
        void UpdElement(HangarBindingModel model);
        [BuyerMethod("Метод удаления склада")]
        void DelElement(int id);
    }
}
