using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.ViewModels
{
    public class EditBasicInformationModel
    {
       
        public string Id { get; set; }
        public string userType { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Company Name")]
        public string companyName { get; set; }

        [Required]
        [Display(Name = "Select Country")]
        public int countryid { get; set; } = 0;

        [Required]
        [Display(Name = "Select State")]
        public int stateid { get; set; } = 0;


        [Required]
        [Display(Name = "Select City")]       
        public int? cityid { get; set; }
        [Display(Name = "Business Year")]
        public string businessYear { get; set; }
        [Display(Name = "Product Deal In")]
        public string productDealin { get; set; }
        public string multipleproductDealin { get; set; }
        [Display(Name = "Export To Countries")]
        public string ExportToCountries { get; set; }
        public string multipleExportToCountries { get; set; }
        [Display(Name = "Latitude")]
        public string userlatitude { get; set; }
        [Display(Name = "Longitude")]
        public string userlongitude { get; set; }
        [Display(Name = "Packhouse Latitude")]
        public string packHouselatitude { get; set; }
        [Display(Name = "Packhouse Longitude")]
        public string packHouselongitude { get; set; }
        [Display(Name = "Packhouse Address")]
        public string packHouseAddress { get; set; }

        public string deviceid { get; set; }
        public bool  isBasicInfoFill { get; set; }


        public IFormFile logo { get; set; }
        [Display(Name = "Logo")]
        public string  logoName { get; set; }
        [Display(Name = "Email Id")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string phonenumber { get; set; }
    }
}
