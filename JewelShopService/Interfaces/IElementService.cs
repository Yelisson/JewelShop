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
    [BuyerInterface("Интерфейс для работы с компонентами")]
    public interface IElementService
    {
        [BuyerMethod("Метод получения списка компонент")]
        List<ElementViewModel> GetList();
        [BuyerMethod("Метод получения компонента по id")]
        ElementViewModel GetElement(int id);
        [BuyerMethod("Метод добавления компонента")]
        void AddElement(ElementBindingModel model);
        [BuyerMethod("Метод изменения данных по компоненту")]
        void UpdElement(ElementBindingModel model);
        [BuyerMethod("Метод удаления компонента")]
        void DelElement(int id);
    }
}
