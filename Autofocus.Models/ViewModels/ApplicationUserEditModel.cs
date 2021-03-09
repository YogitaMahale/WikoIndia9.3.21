using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autofocus.Models.ViewModels
{
  public  class ApplicationUserEditModel
    {
        public string  Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Display(Name = "Mobile No")]
        public string phonenumber { get; set; }
        [Display(Name = "Cancel Cheque Photo")]
        public IFormFile CancellerdChequeImg { get; set; }
        public string CancellerdChequeImgName { get; set; }


        [Display(Name = "Ceritification")]
        public string Ceritification { get; set; }

        [Required]
        [Display(Name = "Select Country")]
        public int countryid { get; set; } = 0;

        [Required]
        [Display(Name = "Select State")]
        public int stateid { get; set; } = 0;


        [Required]
        [Display(Name = "Select City")]
        public int cityid { get; set; } = 0;

        [Display(Name = "Company Reg Ceritificate")]
        public IFormFile CompanyRegCeritificate { get; set; }
        public string CompanyRegCeritificateName { get; set; }
        public string ExportToCountries { get; set; }


        [Display(Name = "Visiting Card Image")]
        public IFormFile VisitingCardImg { get; set; }
        public string VisitingCardImgName { get; set; }


        [Display(Name = "Aadhar Back Image")]
        public IFormFile aadharBackImg { get; set; }
        public string aadharBackImgName { get; set; }


        [Display(Name = "Aadhar Front Image")]
        public IFormFile aadharFrontImg { get; set; }
        public string aadharFrontImgName { get; set; }



        [Display(Name = "Business Year")]
        public string businessYear { get; set; }
        [Display(Name = "Product Deal in")]
        public string productDealin { get; set; }
        //[BindProperty]
        //[Display(Name = "Logo")]
        //public IFormFile logo { get; set; } = null;


        [Display(Name = "Pancard Image")]
        public IFormFile pancardImg { get; set; }
        public string pancardImgName { get; set; }

        [Display(Name = "Logo Image")]
        public IFormFile logo { get; set; }
        public string logoName { get; set; }



        //[Display(Name = "Role")]
        //public string Role { get; set; }

       // public IEnumerable<SelectListItem> roleList { get; set; }
    }
}
