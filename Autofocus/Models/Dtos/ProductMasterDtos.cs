using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Autofocus.Models.Dtos
{
   public  class ProductMasterDtos
    {

        public int id { get; set; }

        public int subCategroyId { get; set; }
         
        public virtual Subcategory Subcategory { get; set; }


        
        public string name { get; set; }
        public string img { get; set; }
        public string description { get; set; }


        

    }
}
