using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
  public  class HangarViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string hangarName { get; set; }
        [DataMember]
        public List<HangarElementViewModel> HangarElement { get; set; }
    }
}
