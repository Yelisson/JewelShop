using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.Attributies
{
    [AttributeUsage(AttributeTargets.Method)]
    class BuyerMethodAttribute:Attribute
    {
        public BuyerMethodAttribute(string descript)
        {
            Description = string.Format("Описание метода: ", descript);
        }
        public string Description { get; private set; }
    }
}
