using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autofocus.Models.ViewModels
{
  public  class CertificationInformationEditViewModel
    {
        public string Id { get; set; }
        public string loginType { get; set; }
        [Display(Name = "IEC")]
        public IFormFile Ceritication_IEC { get; set; }
        public string Ceritication_IECImg { get; set; }
        [Display(Name = "APEDA")]
        public IFormFile Ceritication_APEDA { get; set; }
        public string Ceritication_APEDAImg { get; set; }
        [Display(Name = "FIEO")]
        public IFormFile Ceritication_FIEO { get; set; }
        public string Ceritication_FIEOImg { get; set; }
        [Display(Name = "Global Gap")]
        public IFormFile Ceritication_GlobalGap { get; set; }
        public string Ceritication_GlobalGapImg { get; set; }
        [Display(Name = "Others")]
        public IFormFile Ceritication_Others { get; set; }
        public string Ceritication_OthersImg { get; set; }
        public Boolean isCertificationFill { get; set; }
    }
}
