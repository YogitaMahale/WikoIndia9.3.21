using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class StateRegistrationCreateViewModel
    {
        public int id { get; set; }


        [Required]
        [Display(Name = "Country Name")]
        public int countryid { get; set; }

        [Required]
        public string StateName { get; set; }


        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }


       
    }
}
