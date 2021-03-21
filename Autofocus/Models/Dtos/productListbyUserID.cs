using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class productListbyUserID
    {

        public int id { get; set; }
        public string productname { get; set; }
        public string ProductImage { get; set; }
        public string gradeName { get; set; }
        public int gradeId { get; set; }
        public string productsize { get; set; }
        public string packingType { get; set; }
        public string cityname { get; set; }
        public decimal spotRate { get; set; }
        public decimal quantityAvailable { get; set; }
        public string rateTill { get; set; }
        public Boolean isNegotiable { get; set; }
        public string tradeId { get; set; }
       public IEnumerable<ProductDetailsDtos> productdetails { get; set; }
       // public IEnumerable<ProductDetails> productdetails { get; set; }
    }
}
