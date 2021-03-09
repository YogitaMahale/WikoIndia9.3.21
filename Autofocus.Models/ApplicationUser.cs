
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models
{
  public class ApplicationUser : IdentityUser
    {
        [Required]
        public string name { get; set; }   
        public string companyName { get; set; }        
       

        [ForeignKey("CityRegistration")]
        public int? cityid { get; set; }
        public virtual CityRegistration CityRegistration { get; set; }

        public string businessYear { get; set; }
        // public string productDealin { get; set; }

        public string  productDealin { get; set; }
        
        public string ExportToCountries { get; set; }


        public DateTime createddate { get; set; } = DateTime.Now;         
       
        [NotMapped]
        public string Role { get; set; }



        //-*-----ceritication-----------------
        public string Ceritication_IEC { get; set; }
        public string Ceritication_APEDA { get; set; }
        public string Ceritication_FIEO { get; set; }
        public string Ceritication_GlobalGap { get; set; }
        public string Ceritication_Others { get; set; }

        //-*-----Docmentation----------------
        public string logo { get; set; }
        public string Ceritification { get; set; }
        public string pancardImg { get; set; }
        public string aadharBackImg { get; set; }
        public string aadharFrontImg { get; set; }
        public string CompanyRegCeritificate { get; set; }
        public string CancellerdChequeImg { get; set; }
        public string VisitingCardImg { get; set; }

        //-*-----Location----------------
        public string userlatitude { get; set; }
        public string userlongitude { get; set; }
        public string packHouselatitude { get; set; }
        public string packHouselongitude { get; set; }
        public string packHouseAddress { get; set; }
        public string deviceid { get; set; }
        //-*-----Status Flag----------------
        [DefaultValue("false")]
        public Boolean isBasicInfoFill { get; set; }
        [DefaultValue("false")]
        public Boolean isCertificationFill { get; set; }
        [DefaultValue("false")]
        public Boolean isDocumentationFill { get; set; } 

    }
}
