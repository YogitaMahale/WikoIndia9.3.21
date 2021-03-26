using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models.Dtos
{
   public class ProductViewModelDtos
    {
        public int id { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public int productmasterId { get; set; }
        [Required]
        public string gradeId { get; set; }
        [Required]
        public int productSizeId { get; set; }
        [Required]
        public decimal spotRate { get; set; }
        [Required]
        public decimal quantityAvailable { get; set; }
        [Required]
        public string  rateTillTime { get; set; }
        [Required]
        public int cityId { get; set; }
        [Required]
        public int packingTypeId { get; set; }
        [Required]
        public Boolean isNegotiable { get; set; }
        public Boolean isAvailable { get; set; }
        public IEnumerable<ProductDetailsViewModelDtos>  productDetailsViewModelDtos { get; set; }

    }
}
