using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
   public class HangarsLoadViewModel
    {
        [DataMember]
        public string hangarName { get; set; }
        [DataMember]
        public int totalCount { get; set; }
        [DataMember]
         public List<HangarsElementLoadViewModel> Elements { get; set; }
    }

    [DataContract]
    public class HangarsElementLoadViewModel
    {
        [DataMember]
        public string ElementName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }

}
