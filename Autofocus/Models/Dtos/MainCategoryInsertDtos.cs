using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Autofocus.Models.Dtos
{
   public  class MainCategoryInsertDtos
    {
      
        [Required]
        public string name { get; set; }

        public string img { get; set; }


         
       
    }
}
