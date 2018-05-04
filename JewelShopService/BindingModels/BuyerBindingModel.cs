using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
    public class BuyerBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string buyerFIO { get; set; }
    }
}
