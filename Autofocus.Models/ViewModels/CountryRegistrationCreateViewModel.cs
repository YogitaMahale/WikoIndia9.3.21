using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class CountryRegistrationCreateViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Country Name")]
        public string countryname { get; set; }

        public string countrycode { get; set; }


        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }
    }
}
