using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class productListbyUserID
    {

        public string id { get; set; }
        public string productname { get; set; }
        public string grade { get; set; }
        public string productsize { get; set; }
        public string packingType { get; set; }
        public string cityname { get; set; }
        public decimal spotRate { get; set; }
        public decimal quantityAvailable { get; set; }
        public DateTime rateTill { get; set; }
        public Boolean isNegotiable { get; set; }
        public string tradeId { get; set; }
    }
}
