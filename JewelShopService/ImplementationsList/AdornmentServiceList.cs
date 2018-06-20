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
            List<AdornmentViewModel> result = source.Adornments
              .Select(rec => new AdornmentViewModel
              {
                  id = rec.id,
                  adornmentName = rec.adornmentName,
                  cost = rec.price,
                  ProductComponent = source.AdornmentElements
                          .Where(recPC => recPC.adornmentId == rec.id)
                          .Select(recPC => new AdornmentElementViewModel
                          {
                              id = recPC.id,
                              adornmentId = recPC.adornmentId,
                              elementId = recPC.elementId,
                              elementName = source.Elements
                                  .FirstOrDefault(recC => recC.id == recPC.elementId)?.elementName,
                              count = recPC.count
                          })
                          .ToList()
              })
              .ToList();
            return result;
        }

        public AdornmentViewModel GetElement(int id)
        {
            Adornment element = source.Adornments.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new AdornmentViewModel
                {
                    id = element.id,
                    adornmentName = element.adornmentName,
                    cost = element.price,
                    ProductComponent = source.AdornmentElements
                            .Where(recPC => recPC.adornmentId == element.id)
                            .Select(recPC => new AdornmentElementViewModel
                            {
                                id = recPC.id,
                                adornmentId = recPC.adornmentId,
                                elementId = recPC.elementId,
                                elementName = source.Elements
                                        .FirstOrDefault(recC => recC.id == recPC.elementId)?.elementName,
                                count = recPC.count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(AdornmentBindingModel model)
        {
            Adornment element = source.Adornments.FirstOrDefault(rec => rec.adornmentName == model.adornmentName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Adornments.Count > 0 ? source.Adornments.Max(rec => rec.id) : 0;
            source.Adornments.Add(new Adornment
            {
                id = maxId + 1,
                adornmentName = model.adornmentName,
                price = model.cost
            });
            int maxPCId = source.AdornmentElements.Count > 0 ?
                                    source.AdornmentElements.Max(rec => rec.id) : 0;
            var groupComponents = model.AdornmentComponent
                                        .GroupBy(rec => rec.elementId)
                                        .Select(rec => new
                                        {
                                            ComponentId = rec.Key,
                                            Count = rec.Sum(r => r.count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                source.AdornmentElements.Add(new AdornmentElement
                {
                    id = ++maxPCId,
                    adornmentId = maxId + 1,
                    elementId = groupComponent.ComponentId,
                    count = groupComponent.Count
                });
            }
        }

        public void UpdElement(AdornmentBindingModel model)
        {
            Adornment element = source.Adornments.FirstOrDefault(rec =>
                                       rec.adornmentName == model.adornmentName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Adornments.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.adornmentName = model.adornmentName;
            element.price = model.cost;

            int maxPCId = source.AdornmentElements.Count > 0 ? source.AdornmentElements.Max(rec => rec.id) : 0;
            var compIds = model.AdornmentComponent.Select(rec => rec.elementId).Distinct();
            var updateComponents = source.AdornmentElements
                                            .Where(rec => rec.adornmentId == model.id &&
                                           compIds.Contains(rec.elementId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.count = model.AdornmentComponent
                                                .FirstOrDefault(rec => rec.id == updateComponent.id).count;
            }
            source.AdornmentElements.RemoveAll(rec => rec.adornmentId == model.id &&
                                       !compIds.Contains(rec.elementId));
            var groupComponents = model.AdornmentComponent
                                        .Where(rec => rec.id == 0)
                                        .GroupBy(rec => rec.elementId)
                                        .Select(rec => new
                                        {
                                            ComponentId = rec.Key,
                                            Count = rec.Sum(r => r.count)
                                        });
            foreach (var groupComponent in groupComponents)
            {
                AdornmentElement elementPC = source.AdornmentElements
                                        .FirstOrDefault(rec => rec.adornmentId == model.id &&
                                                        rec.elementId == groupComponent.ComponentId);
                if (elementPC != null)
                {
                    elementPC.count += groupComponent.Count;
                }
                else
                {
                    source.AdornmentElements.Add(new AdornmentElement
                    {
                        id = ++maxPCId,
                        adornmentId = model.id,
                        elementId = groupComponent.ComponentId,
                        count = groupComponent.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Adornment element = source.Adornments.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.AdornmentElements.RemoveAll(rec => rec.adornmentId == id);
                source.Adornments.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
