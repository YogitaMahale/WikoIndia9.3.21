using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class packingSizeCreateViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string name { get; set; }

        

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }
    }
}
