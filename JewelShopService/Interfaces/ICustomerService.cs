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
    [BuyerInterface("Интерфейс для работы с работниками")]
    public interface ICustomerService
    {
        [BuyerMethod("Метод получения списка работников")]
        List<CustomerViewModel> GetList();
        [BuyerMethod("Метод получения работника по id")]
        CustomerViewModel GetElement(int id);
        [BuyerMethod("Метод добавления работника")]
        void AddElement(CustomerBindingModel model);
        [BuyerMethod("Метод изменения данных по работнику")]
        void UpdElement(CustomerBindingModel model);
        [BuyerMethod("Метод удаления работника")]
        void DelElement(int id);
    }
}
