using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.API.Dtos 
{
    public class ApplicationUserSaveModelDtos
    {
         

        public string Id { get; set; }
         
        [Required]
        [Display(Name = "User Type")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Only character")]
        public string usertype { get; set; }
        [Required]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Only character")]
        public string name { get; set; }
        //public string companyName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        [Required]       
       
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string phoneNumber { get; set; }
        [Required]
        public string password { get; set; }


        //public string Ceritification { get; set; }
        //public string CompanyRegCeritificate { get; set; }

        //public string ExportToCountries { get; set; }
        //public string VisitingCardImg { get; set; }
        //public string aadharBackImg { get; set; }
        //public string aadharFrontImg { get; set; }
        //public string businessYear { get; set; }
        //public int? cityid { get; set; }

        //public string deviceid { get; set; }
        //public string latitude { get; set; }
        //public string logo { get; set; }
        //public string longitude { get; set; }

        //public string pancardImg { get; set; }
        //public string productDealin { get; set; }
        //public string CancellerdChequeImg { get; set; }

    }
}
