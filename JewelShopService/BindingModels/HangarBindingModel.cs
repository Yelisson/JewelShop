using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
    public class HangarBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string hangarName { get; set; }
    }
}
