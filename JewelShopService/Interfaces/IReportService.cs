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
    [BuyerInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [BuyerMethod("Метод сохранения списка изделий в doc-файл")]
        void SaveAdornmentPrice(ReportBindingModel model);
        [BuyerMethod("Метод получения списка складов с количество компонент на них")]
        List<HangarsLoadViewModel> GetHangarsLoad();
        [BuyerMethod("Метод сохранения списка складов с количество компонент на них в xls-файл")]
        void SaveHangarsLoad(ReportBindingModel model);
        [BuyerMethod("Метод получения списка заказов клиентов")]
        List<BuyerOrdersModel> GetBuyerOrders(ReportBindingModel model);
        [BuyerMethod("Метод сохранения списка заказов клиентов в pdf-файл")]
        void SaveBuyerOrders(ReportBindingModel model);
    }
}
