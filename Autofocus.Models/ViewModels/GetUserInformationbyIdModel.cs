using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.Models.ViewModels
{
 public class GetUserInformationbyIdModel
    {
        

        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string cityName { get; set; }
        public string businessYear { get; set; }
        public string Ceritication_IEC { get; set; }
        public string Ceritication_APEDA { get; set; }
        public string Ceritication_FIEO { get; set; }
        public string Ceritication_GlobalGap { get; set; }                   
      
        public string Ceritication_Others { get; set; }
        public string logo { get; set; }
        public string Ceritification { get; set; }
        public string pancardImg { get; set; }
        public string aadharBackImg { get; set; }
        public string aadharFrontImg { get; set; }
        public string CompanyRegCeritificate { get; set; }
        public string CancellerdChequeImg { get; set; }
        public string VisitingCardImg { get; set; }
        public string userlatitude { get; set; }
 
        public string userlongitude { get; set; }
        public string packHouselatitude { get; set; }
        public string packHouselongitude { get; set; }
        public string packHouseAddress { get; set; }
        public bool isBasicInfoFill { get; set; }
        public bool isCertificationFill { get; set; }
        public bool isDocumentationFill { get; set; }
        public string productDealin { get; set; }
        public string countryname { get; set; }
        
    }
}
