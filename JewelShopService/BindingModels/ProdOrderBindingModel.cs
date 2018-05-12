using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
  public  class ProdOrderBindingModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int buyerId { get; set; }
        [DataMember]
        public int adornmentId { get; set; }
        [DataMember]
        public int? customerId { get; set; }
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public decimal sum { get; set; }
    }
}
