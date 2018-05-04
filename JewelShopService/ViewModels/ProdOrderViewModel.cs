using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
   public class ProdOrderViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int buyerId { get; set; }
        [DataMember]
        public string buyerFIO { get; set; }
        [DataMember]
        public int adornmentId { get; set; }
        [DataMember]
        public string adornmentName { get; set; }
        [DataMember]
        public int? customerId { get; set; }
        [DataMember]
        public string customerName { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public decimal sum { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string DateCustom { get; set; }
    }
}
