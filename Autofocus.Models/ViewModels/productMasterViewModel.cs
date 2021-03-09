using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Autofocus.Models.ViewModels
{
  public class productMasterViewModel
    {
        public int id { get; set; }
        public int mainCategroyId { get; set; }
        public int subCategroyId { get; set; }
        public string name { get; set; }
        public IFormFile img { get; set; }
        public string imgname { get; set; }
        public string description { get; set; }      
        public Boolean isdeleted { get; set; }        
        public Boolean isactive { get; set; }

    }
}
