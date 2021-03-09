using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Autofocus.Models.ViewModels
{
   public  class UserIndexViewModel
    {
        [Required]
        [Display(Name = "Select Type")]
        public string roleId { get; set; }

    }
}
