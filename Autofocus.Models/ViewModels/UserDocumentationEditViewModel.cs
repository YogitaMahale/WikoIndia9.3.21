using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.Models.ViewModels
{
   public class UserDocumentationEditViewModel
    {
        public string Id { get; set; }
        public IFormFile CancellerdChequeImg { get; set; }
        public string CancellerdChequeImgName { get; set; }
        public IFormFile Ceritification { get; set; }
        public string CeritificationImg { get; set; }
        public IFormFile CompanyRegCeritificate { get; set; }
        public string CompanyRegCeritificateImg { get; set; }
        public IFormFile VisitingCardImg { get; set; }
        public string VisitingCardImgName { get; set; }
        public IFormFile aadharBackImg { get; set; }
        public string aadharBackImgName { get; set; }


        public IFormFile aadharFrontImg { get; set; }


        public string aadharFrontImgName { get; set; }

        public IFormFile pancardImg { get; set; }
        public string pancardImgName { get; set; }
        public Boolean isDocumentationFill { get; set; }
    }
}
