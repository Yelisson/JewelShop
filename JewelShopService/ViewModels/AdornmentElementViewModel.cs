using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
    public class AdornmentElementViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int adornmentId { set; get; }
        [DataMember]
        public int elementId { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public string elementName { get; set; }
    }
}
