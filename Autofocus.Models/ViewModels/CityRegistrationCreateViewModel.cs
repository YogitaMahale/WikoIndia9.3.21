using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class CityRegistrationCreateViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Select Country")]
        public int countryid { get; set; }

        [Required]
        [Display(Name = "Select State")]
        public int stateid { get; set; }
      
        [Required]
        public string cityName { get; set; }


        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }




    }
}
