using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models.Dtos
{
   public  class ProductDetailsViewModelDtos
    {     
        
      //  public int pid { get; set; }  
        public int packingSizeId { get; set; }
        public int packingeTypeId { get; set; }
        public decimal amount { get; set; }
        public bool isNegotiable { get; set; }

        
    }
}
