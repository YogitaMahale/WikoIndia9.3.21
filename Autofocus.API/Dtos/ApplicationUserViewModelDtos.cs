using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models;
namespace Autofocus.API.Dtos 
{
    public class ApplicationUserViewModelDtos
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string OTPNo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Ceritification { get; set; }
        public string CompanyRegCeritificate { get; set; }
        public string ExportToCountries { get; set; }
        public string VisitingCardImg { get; set; }
        public string aadharBackImg { get; set; }
        public string aadharFrontImg { get; set; }
        public string businessYear { get; set; }
        public string cityid { get; set; }
         //public CityRegistration CityRegistration { get; set; }
        public string createddate { get; set; }
        public string deviceid { get; set; }
        public string latitude { get; set; }
        public string logo { get; set; }
        public string longitude { get; set; }
        public string name { get; set; }
        public string pancardImg { get; set; }
        public string productDealin { get; set; }
    }
}

