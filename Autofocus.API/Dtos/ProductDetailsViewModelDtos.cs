using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.API.Dtos
{
   public  class ProductDetailsViewModelDtos
    {
        public int id { get; set; }
       // public int pid { get; set; }

        [Required]
        public int bagId { get; set; }
        [Required]

        public decimal bagPrice { get; set; }
        [Required]

        public decimal labourCost { get; set; }
         


    }
}
