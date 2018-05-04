using System;
using System.Runtime.Serialization;

namespace JewelShopService.BindingModels
{
    [DataContract]
   public  class ReportBindingModel
    {
        [DataMember]
        public string fileName { get; set; }

        [DataMember]
        public DateTime? dateFrom { get; set; }

        [DataMember]
        public DateTime? dateTo { get; set; }
    }
}
