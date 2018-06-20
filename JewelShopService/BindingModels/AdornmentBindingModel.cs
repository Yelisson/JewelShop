﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopService.BindingModels
{
    public class AdornmentBindingModel
    {
        public int id { get; set; }

        public string adornmentName { get; set; }

        public decimal cost { get; set; }

        public List<AdornmentElementBindingModel> AdornmentComponents { get; set; }
    }
}
