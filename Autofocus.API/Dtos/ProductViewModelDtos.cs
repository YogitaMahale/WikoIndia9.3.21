using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.API.Dtos
{
   public class ProductViewModelDtos
    {
        public int id { get; set; }

        [Required]
        public string  userId { get; set; }
        [Required]
        public int subCategoryId { get; set; }
        [Required]
        public string gradeId { get; set; }
        [Required]
        public int packingSizeId { get; set; }
        [Required]

        public decimal quantity { get; set; }
        [Required]

        public decimal  spotRate { get; set; }
        [Required]
        public string cityId { get; set; }
        [Required]
       public DateTime  rateTill { get; set; }
        

        public IEnumerable<ProductDetailsViewModelDtos>  productDetailsViewModelDtos { get; set; }

    }
}
