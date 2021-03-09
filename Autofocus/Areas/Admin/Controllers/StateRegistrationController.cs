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
using Autofocus .Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autofocus.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles =SD.Role_Admin)]
     
    public class StateRegistrationController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
        public StateRegistrationController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
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
            ViewBag.Allcountry = _unitofWork.country.GetAll().Where(x=>x.isdeleted==false).Select(x=>new SelectListItem() { 
                Text=x.countryname,
                Value=x.id.ToString()
            });
            var model = new StateRegistrationCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StateRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var objcategory = new StateRegistration
                {
                    id = model.id
                    ,
                    countryid  = model.countryid
                    ,
                    StateName=model.StateName
                   
                   ,
                    isdeleted = false
                    ,
                    isactive = false 

                };
                 
                _unitofWork.state.Add(objcategory);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Save successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Allcountry = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
                {
                    Text = x.countryname,
                    Value = x.id.ToString()
                });
                return View(model);

            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Allcountry = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.countryname,
                Value = x.id.ToString()
            });
            var objcategory = _unitofWork.state.Get(id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new StateRegistrationCreateViewModel()
            {
                id = objcategory.id,
                countryid = objcategory.countryid,
                StateName = objcategory.StateName

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StateRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.state.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.countryid = model.countryid;
                storeobj.StateName = model.StateName;


                _unitofWork.state.Update(storeobj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Update successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Allcountry = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
                {
                    Text = x.countryname,
                    Value = x.id.ToString()
                });
                return View(model);
            }

        }



        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL()
        {
            var obj = _unitofWork.state.GetAll().Where(x=>x.isdeleted==false).Select(x=>new StateRegistrationIndexViewModel() { 
            id=x.id,
               countryName= _unitofWork.country.Get(x.countryid).countryname,
               stateName=x.StateName
            });
            return Json(new { data = obj });

 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.state.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.state.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}