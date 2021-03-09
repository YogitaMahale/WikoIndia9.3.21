using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.Dtos
{
    public class CertificationInformationDtos
    {
        public string Id { get; set; }
        public string Ceritication_IEC { get; set; }
        public string Ceritication_APEDA { get; set; }
        public string Ceritication_FIEO { get; set; }
        public string Ceritication_GlobalGap { get; set; }
        public string Ceritication_Others { get; set; }
        public Boolean isCertificationFill { get; set; } 



    }
}
