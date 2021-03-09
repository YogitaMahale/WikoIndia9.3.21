using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Autofocus.Models.Dtos
{
   public  class CountryRegistrationViewModel
    {
        public int id { get; set; }
         
        public string countryname { get; set; }

      
    }
}
