using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Models.ViewModels
{
    public class productSizeUpsertViewModel
    {
        public int id { get; set; }
        [Required]
        public string size { get; set; }


        public Boolean isdeleted { get; set; }

        public Boolean isactive { get; set; }
    }
}
