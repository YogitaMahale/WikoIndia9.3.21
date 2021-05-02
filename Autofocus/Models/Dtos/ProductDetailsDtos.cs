using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class ProductDetailsDtos
    {
        public int id { get; set; }
        public int packingSizeId { get; set; }
        public string sizename { get; set; }
        public int packingeTypeId { get; set; }
        public string  packingeTypeName { get; set; }
        //public int packingeTypeId { get; set; }
        //public string packingeTypeName { get; set; }
        public decimal amount { get; set; }
    }
}
