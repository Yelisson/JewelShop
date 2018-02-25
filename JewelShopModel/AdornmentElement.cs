using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopModel
{
   public  class AdornmentElement
    {
        public int id { get; set; }
        public int adornmentId { set; get; }
        public int elementId { get; set; }
        public int count { get; set; }
    }
}
