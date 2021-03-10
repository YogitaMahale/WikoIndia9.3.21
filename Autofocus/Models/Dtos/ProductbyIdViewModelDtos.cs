using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models.Dtos
{
   public class ProductbyIdViewModelDtos
    {
        public int id { get; set; }
        public string userId { get; set; }        
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int productmasterId { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public string gradeId { get; set; }
        public int productSizeId { get; set; }       
        public virtual productSize productSize { get; set; }
        public decimal spotRate { get; set; }
       
        public decimal quantityAvailable { get; set; }

        public DateTime rateTill { get; set; }
        public int cityId { get; set; }      
        public virtual CityRegistration CityRegistration { get; set; }


        public int packingTypeId { get; set; }      
        public virtual packingType packingType { get; set; }

        public string tradeId { get; set; }

       
        public Boolean isNegotiable { get; set; }
  

        public IEnumerable<ProductDetailsViewModelDtos>  productDetailsViewModelDtos { get; set; }

    }
}
