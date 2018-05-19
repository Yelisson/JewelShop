using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.Attributies
{
    [AttributeUsage(AttributeTargets.Interface)]
    class BuyerInterfaceAttribute: Attribute
    {      
        public BuyerInterfaceAttribute(string descript)
        {
            Description = string.Format("Описание интерфейса: ", descript);
        }
        public string Description { get; private set; }
    }
}
