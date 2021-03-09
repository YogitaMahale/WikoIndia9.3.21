using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class GradeCreateViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Type")]
        public string type { get; set; }
 

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }
    }
}
