using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models;
namespace Autofocus.Models.Dtos
{
    public class ApplicationUserViewModelDtos
    {
        public string OTPNo { get; set; }

        public string PhoneNumber { get; set; }

        
        public string name { get; set; }
        public string companyName { get; set; }


         
        public int? cityid { get; set; }      

        public string businessYear { get; set; }
       
        public string productDealin { get; set; }

        public string ExportToCountries { get; set; }


        public DateTime createddate { get; set; } = DateTime.Now;

      


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
       
        public Boolean isBasicInfoFill { get; set; }
      
        public Boolean isCertificationFill { get; set; }
       
        public Boolean isDocumentationFill { get; set; }
        //
    }
}

