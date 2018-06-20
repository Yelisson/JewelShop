using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopModel
{
   public  class Customer
    {
        public int id { get; set; }

        [Required]
        public string customerFIO { get; set; }

        [ForeignKey("customerId")]
        public virtual List<ProdOrder> ProdOrders { get; set; }
    }
}
