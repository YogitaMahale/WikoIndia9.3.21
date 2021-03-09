using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models
{
  public  class ProductMaster
    {
        public int id { get; set; }

        public int subCategroyId { get; set; }
        [ForeignKey("subCategroyId")]

        public virtual Subcategory Subcategory { get; set; }


        [Required]
        public string name { get; set; }
        public string img { get; set; }
        public string description { get; set; }


        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }

    }
}
