using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Autofocus.API.Dtos
{
   public  class MainCategoryDtos
    {
         
       
       // [RegularExpression(@"[^0-9]", ErrorMessage = "Please Only Number")]
        public int id { get; set; } = 0;

        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Only character")]        
        public string name { get; set; }

        public string img { get; set; }


         
       
    }
}
