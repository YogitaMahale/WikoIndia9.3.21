using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.API.Controllers
{
    public class LoginViewModel
    {
        [Required]
        public string mobileNo { get; set; }
        [Required]
        public string password { get; set; }
    }
}
