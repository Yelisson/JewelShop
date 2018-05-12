using System.Runtime.Serialization;
using System.Collections.Generic;

namespace JewelShopService.ViewModels
{
    [DataContract]
  public  class AdornmentViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string adornmentName { get; set; }
        [DataMember]
        public decimal cost { get; set; }
        [DataMember]
        public List<AdornmentElementViewModel> ProductComponent { get; set; }
    }
}
