using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class BasicInformationDtos
    {
        public string Id { get; set; }
        public string name { get; set; }
       public string companyName { get; set; }        
       public int? cityid { get; set; }
       public string businessYear { get; set; }

        public string productDealin { get; set; }

        public string ExportToCountries { get; set; }

        public string userlatitude { get; set; }
        public string userlongitude { get; set; }
        public string packHouselatitude { get; set; }
        public string packHouselongitude { get; set; }
        public string packHouseAddress { get; set; }
        public string deviceid { get; set; }
        public bool  isBasicInfoFill { get; set; }


        public string logo { get; set; }
    }
}
