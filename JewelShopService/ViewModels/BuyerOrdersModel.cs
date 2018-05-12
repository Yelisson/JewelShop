using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
    public class BuyerOrdersModel
    {
        [DataMember]
        public string buyerName { get; set; }
        [DataMember]
        public string dateCustom { get; set; }
        [DataMember]
        public string adornmentName { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public decimal sum { get; set; }
        [DataMember]
        public string status { get; set; }
    }
}
