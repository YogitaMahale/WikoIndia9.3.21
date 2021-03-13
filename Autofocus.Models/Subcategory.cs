using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models
{
  public  class Subcategory
    {


        public int id { get; set; }

        public int mainCategroyId { get; set; }
        [ForeignKey("mainCategroyId")]

        public virtual MainCategory MainCategory { get; set; }
       
       
        [Required]
        public string name { get; set; }
        public string img { get; set; }
        public string cityIds { get; set; }
        public int stateid { get; set; }

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }
        
         

    }
}
