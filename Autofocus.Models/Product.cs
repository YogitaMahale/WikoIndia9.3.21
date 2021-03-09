using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models
{
   public class Product
    {
        public int id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        //[ForeignKey("ProductMaster")]
        public int productmasterId { get; set; }
      //  public ProductMaster ProductMaster { get; set; }


       // [ForeignKey("productSize")]
        public int productSizeId { get; set; }
      //  public productSize productSize { get; set; }
       // [ForeignKey("packingType")]
        public int packingTypeId { get; set; }
      //  public packingType packingType { get; set; }



        //public string  userId { get; set; }
        //[ForeignKey("userId")]
        //public virtual ApplicationUser ApplicationUser { get; set; }



        //public int productmasterId { get; set; }
        //[ForeignKey("productmasterId")]
        //public virtual ProductMaster ProductMaster { get; set; }

        public string gradeId { get; set; }

        //[ForeignKey("productSize")]
        //public int productSizeId { get; set; }       
        //public  productSize productSize { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal spotRate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal quantity { get; set; }

        public DateTime rateTill { get; set; }


        //[ForeignKey("packingType")]
        //public int packingTypeId { get; set; }      
        //public   packingType packingType { get; set; }

        public string cityId { get; set; }
      
 

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; } 
        

    }
}
