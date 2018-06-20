using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.ViewModels
{
   public class HangarsLoadViewModel
    {
        public string hangarName { get; set; }

        public int totalCount { get; set; }

        public IEnumerable<Tuple<string, int>> Elements { get; set; }
    }
}
