using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
   public class AdornmentElementBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int adornmentId { set; get; }
        [DataMember]
        public int elementId { get; set; }
        [DataMember]
        public int count { get; set; }
    }
}
