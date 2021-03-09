using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
//using Autofocus.Models.ViewModels;
using Autofocus.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
 
using Autofocus.Models.ViewModels;
 
//test
namespace Autofocus.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles =SD.Role_Admin)]
     
    public class CountryRegistrationController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
        public CountryRegistrationController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()        
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
          
            var model = new CountryRegistrationCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var objcategory = new CountryRegistration
                {
                    id = model.id
                    ,
                    countrycode  = model.countrycode
                    ,
                    countryname=model.countryname
                   
                   ,
                    isdeleted = false
                    ,
                    isactive = false 

                };
                 
                _unitofWork.country.Add(objcategory);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Save successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();

            }
        }
        public IActionResult Edit(int id)
        {
           
            var objcategory = _unitofWork.country .Get(id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new CountryRegistrationCreateViewModel()
            {
                id = objcategory.id,
                countrycode = objcategory.countrycode ,
                countryname=objcategory.countryname 

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CountryRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.country.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.countryname = model.countryname ;
                storeobj.countrycode = model.countrycode;
                
               
                _unitofWork.country.Update(storeobj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Update successfully";
               
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

         
 
        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL()
        {
            var obj = _unitofWork.country.GetAll().Where(x=>x.isdeleted==false);
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.country.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.country.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}