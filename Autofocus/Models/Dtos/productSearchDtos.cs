using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class productSearchDtos
    {
 

        public int id { get; set; }
      
        public string userId { get; set; }
       
        public int productmasterId { get; set; }
      
        public string gradeId { get; set; }
       
        public int productSizeId { get; set; }
       
        public decimal spotRate { get; set; }
        
        public decimal quantityAvailable { get; set; }
      
        public DateTime rateTill { get; set; }
        //cityId, packingTypeId, isNegotiable[tradeId],[isAvailable]

        public int cityId { get; set; }
       
        public int packingTypeId { get; set; }
       
        public Boolean isNegotiable { get; set; }
        public Boolean isAvailable { get; set; }
        public string tradeId { get; set; }
        public string productname { get; set; }
    }
}
