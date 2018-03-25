using System;
using System.Collections.Generic;
using JewelShopModel;
using JewelShopService.BindingModels;
using JewelShopService.Interfaces;
using JewelShopService.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.ImplementationsList
{
   public  class HangarServiceList: IHangarService
    {
        private DataListSingleton source;

        public HangarServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<HangarViewModel> GetList()
        {
            List<HangarViewModel> result = source.Hangars
                .Select(rec => new HangarViewModel
                {
                    id = rec.id,
                    hangarName = rec.hangarName,
                    HangarElement = source.HangarElements
                            .Where(recPC => recPC.hangarId == rec.id)
                            .Select(recPC => new HangarElementViewModel
                            {
                                id = recPC.id,
                                hangarId = recPC.hangarId,
                                elementId = recPC.elementtId,
                                elementName = source.Elements
                                    .FirstOrDefault(recC => recC.id == recPC.elementtId)?.elementName,
                                Count = recPC.count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public HangarViewModel GetElement(int id)
        {
            Hangar element = source.Hangars.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new HangarViewModel
                {
                    id = element.id,
                    hangarName = element.hangarName,
                    HangarElement = source.HangarElements
                            .Where(recPC => recPC.hangarId == element.id)
                            .Select(recPC => new HangarElementViewModel
                            {
                                id = recPC.id,
                                hangarId = recPC.hangarId,
                                elementId = recPC.elementtId,
                                elementName = source.Elements
                                    .FirstOrDefault(recC => recC.id == recPC.elementtId)?.elementName,
                                Count = recPC.count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(HangarBindingModel model)
        {
            Hangar element = source.Hangars.FirstOrDefault(rec => rec.hangarName == model.hangarName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Hangars.Count > 0 ? source.Hangars.Max(rec => rec.id) : 0;
            source.Hangars.Add(new Hangar
            {
                id = maxId + 1,
                hangarName = model.hangarName
            });
        }

        public void UpdElement(HangarBindingModel model)
        {
            Hangar element = source.Hangars.FirstOrDefault(rec =>
                                        rec.hangarName == model.hangarName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Hangars.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.hangarName = model.hangarName;
        }

        public void DelElement(int id)
        {
            Hangar element = source.Hangars.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.HangarElements.RemoveAll(rec => rec.hangarId == id);
                source.Hangars.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
