using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autofocus.Models.ViewModels
{
    public   class MainCategoryCreateViewModel
    {
        public int id { get; set; }


        [Required]
        [Display(Name = "Name")]       
        public string name { get; set; }

        [Display(Name = "Image")]
        public IFormFile img { get; set; }
        public string imgName { get; set; }

        [DefaultValue("false")]
        public Boolean isdeleted { get; set; }
        [DefaultValue("false")]
        public Boolean isactive { get; set; }


       
    }
}
