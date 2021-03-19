using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.ViewModels;
using Autofocus.Utility;
using Dapper;
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
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<IdentityUser> _userManager;
        public UserLoginController(UserManager<IdentityUser> userManager, IUnitofWork unitofWork)
        {
            _userManager = userManager;
            _unitofWork = unitofWork;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            IdentityUser usr = await GetCurrentUserAsync();
            var paramter = new DynamicParameters();
            paramter.Add("@Id", usr.Id);
            var objInfo = _unitofWork.sp_call.OneRecord<GetUserInformationbyIdModel>("GetUserDetailsbyId", paramter);
            //return usr?.Id;
            return View(objInfo);
        }
        [HttpGet]
        public async Task<IActionResult> EditBasicInfo(string id)
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return RedirectToAction("EditBasicInfo", "User", new { area = "Admin", id = usr.Id });
            
        }
        [HttpGet]
        public async Task<IActionResult> EditCertification(string id)
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return RedirectToAction("EditUserCertification", "User", new { area = "Admin", id = usr.Id });

        }
        [HttpGet]
        public async Task<IActionResult> EditDocumentation(string id)
        {
            IdentityUser usr = await GetCurrentUserAsync();
            return RedirectToAction("EditUserDocumentation", "User", new { area = "Admin", id = usr.Id });

        }
        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            //IdentityUser usr = await GetCurrentUserAsync();
            //var paramter = new DynamicParameters();
            //paramter.Add("@Id", usr.Id);
            //var objInfo = _unitofWork.sp_call.OneRecord<GetUserInformationbyIdModel>("getProductListbyuserId", paramter);
            ////return usr?.Id;
            return View();
        }
        #region "API CALL"
        [HttpGet]
        public async Task<IActionResult> GetALL(string id)
        {
            IdentityUser usr = await GetCurrentUserAsync();
           
            var paramter = new DynamicParameters();
            paramter.Add("@Id", usr.Id);
            var objInfo = _unitofWork.sp_call.List<ProductListbyUserIdViewModel>("getProductListbyUserId", paramter);

            return Json(new { data = objInfo.ToList() });

        }

        #endregion
    }
}
