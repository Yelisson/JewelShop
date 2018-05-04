using System.Collections.Generic;
using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
    public class AdornmentBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string adornmentName { get; set; }
        [DataMember]
        public decimal cost { get; set; }
        [DataMember]
        public List<AdornmentElementBindingModel> AdornmentComponent { get; set; }
    }
}
