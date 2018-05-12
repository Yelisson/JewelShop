using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
   public  class HangarElementBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int hangarId { get; set; }
        [DataMember]
        public int elementId { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
