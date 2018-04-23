using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.BindingModels
{
   public  class ReportBindingModel
    {
        public string fileName { get; set; }

        public DateTime? dateFrom { get; set; }

        public DateTime? dateTo { get; set; }
    }
}
