using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
  public  class HangarElementViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int hangarId { get; set; }
        [DataMember]
        public int elementId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public string elementName { get; set; }
    }
}
