using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
   public class BuyerViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string buyerFIO { get; set; }
        [DataMember]
        public string mail { get; set; }
        [DataMember]
        public List<MessageInfoViewModel> messages { get; set; }
    }
}
