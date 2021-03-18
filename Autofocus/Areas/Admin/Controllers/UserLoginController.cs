using Autofocus.Models;
using Autofocus.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Areas.Admin.Controllers
{
    [Area("Admin")]
     //[Authorize(Roles = SD.Role_Buyer)]
    [Authorize(Roles = SD.Role_Buyer + "," + SD.Role_Seller)]
    public class UserLoginController : Controller
    {
       
        private readonly UserManager<IdentityUser> _userManager;
        public UserLoginController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            //return usr?.Id;
            return View();
        }
    }
}
