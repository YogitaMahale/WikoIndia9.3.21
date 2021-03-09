using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models
{
   public  class ProductDetails
    {
        public int id { get; set; }

        public int pid { get; set; }
        [ForeignKey("pid")]
        public virtual Product Product { get; set; }

        public int packingSizeId { get; set; }
        [ForeignKey("packingSizeId")]
        public virtual packingSize packingSize { get; set; }

        //[Column(TypeName = "decimal(18, 2)")]
        //public decimal bagPrice { get; set; }

        //labourCost+bagPrice
        [Column(TypeName = "decimal(18, 2)")]
        public decimal amount { get; set; }

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }


    }
}
