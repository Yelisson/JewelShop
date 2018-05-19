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
    [BuyerInterface("Интерфейс для работы с изделиями")]
    public interface IAdornmentService
    {
        [BuyerMethod("Метод получения списка изделий")]
        List<AdornmentViewModel> GetList();
        [BuyerMethod("Метод получения изделия по id")]
        AdornmentViewModel GetElement(int id);
        [BuyerMethod("Метод добавления изделия")]
        void AddElement(AdornmentBindingModel model);
        [BuyerMethod("Метод изменения данных по изделию")]
        void UpdElement(AdornmentBindingModel model);
        [BuyerMethod("Метод удаления изделия")]
        void DelElement(int id);
    }
}
