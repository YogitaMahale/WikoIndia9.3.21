using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models.Dtos
{
   public  class ProductDetailsbyidViewModelDtos
    {     
        
      //  public int pid { get; set; }  
        public int packingSizeId { get; set; }
        public virtual packingSize packingSize { get; set; }
        public decimal amount { get; set; }

        
    }
}
