using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class DocumentationDtos
    {
        public string Id { get; set; }
        public string CancellerdChequeImg { get; set; }
        public string Ceritification { get; set; }
        public string CompanyRegCeritificate { get; set; }
        public string VisitingCardImg { get; set; }
        public string aadharBackImg { get; set; }


        public string aadharFrontImg { get; set; }
       
        public string pancardImg { get; set; }        
        public Boolean isDocumentationFill { get; set; } 
        
    }
}
