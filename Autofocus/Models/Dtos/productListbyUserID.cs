using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class productListbyUserID
    {
 
     

        public int id { get; set; }
        public int maincategoryId { get; set; }
        public string maincategoryName { get; set; }
        public int subcategoryId { get; set; }
        public string  subcategoryname { get; set; }
        public int productmasterId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string gradeName { get; set; }
        public int gradeId { get; set; }
        public int productSizeId { get; set; }
        public string productsize { get; set; }
       // public string packingType { get; set; }
      //  public string cityname { get; set; }
        public decimal spotRate { get; set; }
        public decimal quantityAvailable { get; set; }
        public string rateTill { get; set; }
        public Boolean isNegotiable { get; set; }
        public string tradeId { get; set; }
        public int countryId { get; set; }
        public string  countryName { get; set; }
        public int stateId { get; set; }
        public string stateName { get; set; }
        
        public int cityId { get; set; }
        public string cityName { get; set; }
        public int packingTypeId { get; set; }
        public string packingType { get; set; }
        public bool  isAvailable { get; set; }

         
        public IEnumerable<ProductDetailsDtos> productdetails { get; set; }
       // public IEnumerable<ProductDetails> productdetails { get; set; }
    }
}
