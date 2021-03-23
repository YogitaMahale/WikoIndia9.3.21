using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autofocus.Models.ViewModels
{
   public class UserDocumentationEditViewModel
    {
        public string Id { get; set; }
        public string loginType { get; set; }
        [Display(Name = "Cancelled Cheque")]
        public IFormFile CancellerdChequeImg { get; set; }
        public string CancellerdChequeImgName { get; set; }
        [Display(Name = "Certification")]
        public IFormFile Ceritification { get; set; }
        public string CeritificationImg { get; set; }
        [Display(Name = "Company Reg. Ceritificate")]
        public IFormFile CompanyRegCeritificate { get; set; }
        public string CompanyRegCeritificateImg { get; set; }
        [Display(Name = "Visiting Card")]
        public IFormFile VisitingCardImg { get; set; }
        public string VisitingCardImgName { get; set; }
        [Display(Name = "Aadhar Back")]
        public IFormFile aadharBackImg { get; set; }
        public string aadharBackImgName { get; set; }

        [Display(Name = "Aadhar Front")]
        public IFormFile aadharFrontImg { get; set; }


        public string aadharFrontImgName { get; set; }
        [Display(Name = "Pan card")]
        public IFormFile pancardImg { get; set; }
        public string pancardImgName { get; set; }
        public Boolean isDocumentationFill { get; set; }
    }
}
