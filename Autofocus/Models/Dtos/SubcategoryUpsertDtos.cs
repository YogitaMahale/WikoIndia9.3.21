using Autofocus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Autofocus.Models.Dtos
{
  public  class SubcategoryUpsertDtos
    {

        public int id { get; set; } 
        public int mainCategroyId { get; set; } 
        public string name { get; set; }
        public string img { get; set; }

     

    }
}
