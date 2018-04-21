using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopModel
{
    public class Element
    {
        public int id { set; get; }

        [Required]
        public string elementName { get; set; }

        [ForeignKey("elementId")]
        public virtual List<AdornmentElement> AdornmentElements { get; set; }

        [ForeignKey("elementtId")]
        public virtual List<HangarElement> HangarElements { get; set; }
    }
}
