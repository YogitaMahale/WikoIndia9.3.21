using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.API.Dtos
{
  public  class SubcategoryDtos
    {


        public int id { get; set; }

        public int mainCategroyId { get; set; }
        public   MainCategory MainCategory { get; set; }

        //     public virtual MainCategory MainCategory { get; set; }


        [Required]
        public string name { get; set; }
        public string img { get; set; }

     

    }
}
