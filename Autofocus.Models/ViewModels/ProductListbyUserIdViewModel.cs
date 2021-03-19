using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.Models.ViewModels
{
   public  class ProductListbyUserIdViewModel
    {
            

        public int id { get; set; }
        public string ProductName { get; set; }
        public string gradeName { get; set; }
        public string productSize { get; set; }
        public decimal spotRate { get; set; }
        public decimal quantityAvailable { get; set; }
        public DateTime  rateTill { get; set; }
        public string cityName { get; set; }
        public string packingType { get; set; }
        public bool isNegotiable { get; set; }
        public string tradeId { get; set; }
        public bool isAvailable { get; set; }

    }
}
