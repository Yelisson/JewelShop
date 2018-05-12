using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
   public  class ElementBindingModel
    {
        [DataMember]
        public int id { set; get; }
        [DataMember]
        public string elementName { get; set; }
    }
}
