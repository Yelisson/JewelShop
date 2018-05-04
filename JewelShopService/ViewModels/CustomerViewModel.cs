using System.Runtime.Serialization;

namespace JewelShopService.ViewModels
{
    [DataContract]
  public  class CustomerViewModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string customerFIO { get; set; }
    }
}
