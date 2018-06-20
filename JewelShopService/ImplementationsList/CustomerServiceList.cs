using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using JewelShopService.BindingModels;
using JewelShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.ImplementationsList
{
    public class CustomerServiceList:ICustomerService
    {

        private DataListSingleton source;

        public CustomerServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = source.Customers
                 .Select(rec => new CustomerViewModel
                 {
                     id = rec.id,
                     customerFIO = rec.customerFIO
                 })
                 .ToList();
            return result;
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    id = element.id,
                    customerFIO = element.customerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.customerFIO == model.customerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Customers.Count > 0 ? source.Customers.Max(rec => rec.id) : 0;
            source.Customers.Add(new Customer
            {
                id = maxId + 1,
                customerFIO = model.customerFIO
            });
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = source.Customers.FirstOrDefault(rec =>
                                     rec.customerFIO == model.customerFIO && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Customers.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.customerFIO = model.customerFIO;
        }

        public void DelElement(int id)
        {
            Customer element = source.Customers.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Customers.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
