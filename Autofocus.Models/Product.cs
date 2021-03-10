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

        public string userId { get; set; }
        [ForeignKey("userId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        

        [ForeignKey("ProductMaster")]
        public int productmasterId { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public string gradeId { get; set; }


        public int productSizeId { get; set; }
        [ForeignKey("productSizeId")]
        public virtual productSize productSize { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal spotRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal quantityAvailable { get; set; }

        public DateTime rateTill { get; set; }
        public int cityId { get; set; }
        [ForeignKey("cityId")]
        public virtual CityRegistration CityRegistration { get; set; }


        public int packingTypeId { get; set; }
        [ForeignKey("packingTypeId")]
        public virtual packingType packingType { get; set; }

        public string tradeId { get; set; }

        [DefaultValue("false")]
        public Boolean isNegotiable { get; set; }
        [DefaultValue("false")]
        public Boolean isAvailable { get; set; }

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; } 
        

    }
}
