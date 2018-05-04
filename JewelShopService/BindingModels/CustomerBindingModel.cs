using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
   public  class CustomerBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string customerFIO { get; set; }
    }
}
