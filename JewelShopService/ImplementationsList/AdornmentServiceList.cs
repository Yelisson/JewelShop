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
    public class AdornmentServiceList:IAdornmentService
    {
        private DataListSingleton source;

        public AdornmentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<AdornmentViewModel> GetList()
        {
            List<AdornmentViewModel> result = new List<AdornmentViewModel>();
            for (int i = 0; i < source.Adornments.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<AdornmentElementViewModel> productComponents = new List<AdornmentElementViewModel>();
                for (int j = 0; j < source.AdornmentElements.Count; ++j)
                {
                    if (source.AdornmentElements[j].adornmentId == source.Adornments[i].id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.AdornmentElements[j].elementId == source.Elements[k].id)
                            {
                                componentName = source.Elements[k].elementName;
                                break;
                            }
                        }
                        productComponents.Add(new AdornmentElementViewModel
                        {
                            id = source.AdornmentElements[j].id,
                            adornmentId = source.AdornmentElements[j].adornmentId,
                            elementId = source.AdornmentElements[j].elementId,
                           // elementName = elementName,
                            count = source.AdornmentElements[j].count
                        });
                    }
                }
                result.Add(new AdornmentViewModel
                {
                    id = source.Adornments[i].id,
                    adornmentName = source.Adornments[i].adornmentName,
                    cost = source.Adornments[i].price,
                    ProductComponent = productComponents
                });
            }
            return result;
        }

        public AdornmentViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Adornments.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их количество
                List<AdornmentElementViewModel> productComponents = new List<AdornmentElementViewModel>();
                for (int j = 0; j < source.AdornmentElements.Count; ++j)
                {
                    if (source.AdornmentElements[j].adornmentId == source.Adornments[i].id)
                    {
                        string componentName = string.Empty;
                        for (int k = 0; k < source.Elements.Count; ++k)
                        {
                            if (source.AdornmentElements[j].elementId == source.Elements[k].id)
                            {
                                componentName = source.Elements[k].elementName;
                                break;
                            }
                        }
                        productComponents.Add(new AdornmentElementViewModel
                        {
                            id = source.AdornmentElements[j].id,
                            adornmentId = source.AdornmentElements[j].adornmentId,
                            elementId = source.AdornmentElements[j].elementId,
                            //elementName = componentName,
                            count = source.AdornmentElements[j].count
                        });
                    }
                }
                if (source.Adornments[i].id == id)
                {
                    return new AdornmentViewModel
                    {
                        id = source.Adornments[i].id,
                        adornmentName = source.Adornments[i].adornmentName,
                        cost = source.Adornments[i].price,
                        ProductComponent = productComponents
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }
        public void AddElement(AdornmentBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Adornments.Count; ++i)
            {
                if (source.Adornments[i].id > maxId)
                {
                    maxId = source.Adornments[i].id;
                }
                if (source.Adornments[i].adornmentName == model.adornmentName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Adornments.Add(new Adornment
            {
                id = maxId + 1,
                adornmentName = model.adornmentName,
                price = model.cost
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.AdornmentElements.Count; ++i)
            {
                if (source.AdornmentElements[i].id > maxPCId)
                {
                    maxPCId = source.AdornmentElements[i].id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.AdornmentComponent.Count; ++i)
            {
                for (int j = 1; j < model.AdornmentComponent.Count; ++j)
                {
                    if (model.AdornmentComponent[i].elementId ==
                        model.AdornmentComponent[j].elementId)
                    {
                        model.AdornmentComponent[i].count +=
                            model.AdornmentComponent[j].count;
                        model.AdornmentComponent.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.AdornmentComponent.Count; ++i)
            {
                source.AdornmentElements.Add(new AdornmentElement
                {
                    id = ++maxPCId,
                    adornmentId = maxId + 1,
                    elementId = model.AdornmentComponent[i].elementId,
                    count = model.AdornmentComponent[i].count
                });
            }
        }

        public void UpdElement(AdornmentBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Adornments.Count; ++i)
            {
                if (source.Adornments[i].id == model.id)
                {
                    index = i;
                }
                if (source.Adornments[i].adornmentName == model.adornmentName &&
                    source.Adornments[i].id != model.id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Adornments[index].adornmentName = model.adornmentName;
            source.Adornments[index].price = model.cost;
            int maxPCId = 0;
            for (int i = 0; i < source.AdornmentElements.Count; ++i)
            {
                if (source.AdornmentElements[i].id > maxPCId)
                {
                    maxPCId = source.AdornmentElements[i].id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.AdornmentElements.Count; ++i)
            {
                if (source.AdornmentElements[i].adornmentId == model.id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.AdornmentComponent.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.AdornmentElements[i].id == model.AdornmentComponent[j].id)
                        {
                            source.AdornmentElements[i].count = model.AdornmentComponent[j].count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.AdornmentElements.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.AdornmentComponent.Count; ++i)
            {
                if (model.AdornmentComponent[i].id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.AdornmentElements.Count; ++j)
                    {
                        if (source.AdornmentElements[j].adornmentId == model.id &&
                            source.AdornmentElements[j].elementId == model.AdornmentComponent[i].elementId)
                        {
                            source.AdornmentElements[j].count += model.AdornmentComponent[i].count;
                            model.AdornmentComponent[i].id = source.AdornmentElements[j].id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.AdornmentComponent[i].id == 0)
                    {
                        source.AdornmentElements.Add(new AdornmentElement
                        {
                            id = ++maxPCId,
                            adornmentId = model.id,
                            elementId = model.AdornmentComponent[i].elementId,
                            count = model.AdornmentComponent[i].count
                        });
                    }
                }
            }

        }

        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.AdornmentElements.Count; ++i)
            {
                if (source.AdornmentElements[i].adornmentId == id)
                {
                    source.AdornmentElements.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Adornments.Count; ++i)
            {
                if (source.Adornments[i].id == id)
                {
                    source.Adornments.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
