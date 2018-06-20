using JewelShopService.Interfaces;
using JewelShopService.BindingModels;
using JewelShopService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelShopModel;

namespace JewelShopService.ImplementationsList
{
    public class ElementServiceList:IElementService
    {
        private DataListSingleton source;

        public ElementServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ElementViewModel> GetList()
        {
            List<ElementViewModel> result = source.Elements
               .Select(rec => new ElementViewModel
               {
                   id = rec.id,
                   elementName = rec.elementName
               })
               .ToList();
            return result;
        }

        public ElementViewModel GetElement(int id)
        {
            Element element = source.Elements.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new ElementViewModel
                {
                    id = element.id,
                    elementName = element.elementName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(ElementBindingModel model)
        {
            Element element = source.Elements.FirstOrDefault(rec => rec.elementName == model.elementName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Elements.Count > 0 ? source.Elements.Max(rec => rec.id) : 0;
            source.Elements.Add(new Element
            {
                id = maxId + 1,
                elementName = model.elementName
            });
        }

        public void UpdElement(ElementBindingModel model)
        {
            Element element = source.Elements.FirstOrDefault(rec =>
                                        rec.elementName == model.elementName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Elements.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.elementName = model.elementName;
        }

        public void DelElement(int id)
        {
            Element element = source.Elements.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Elements.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
