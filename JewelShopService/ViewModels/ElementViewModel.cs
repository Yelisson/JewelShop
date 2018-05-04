using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
   public class ElementViewModel
    {
        [DataMember]
        public int id { set; get; }
        [DataMember]
        public string elementName { get; set; }
    }
}
