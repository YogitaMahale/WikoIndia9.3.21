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
 
using Microsoft.AspNetCore.Mvc.Rendering;
using Autofocus.DataAccess.Data;

namespace Autofocus.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class CityRegistrationController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
       
        public CityRegistrationController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
            
        }
        public IActionResult Index()        
        {
            return View();
        }
        public JsonResult getstatebyid(int id)
        {

            IList<StateRegistration> obj = _unitofWork.state.GetAll().Where(x => x.countryid == id &&x.isdeleted==false).ToList();
         //   obj.Insert(0, new StateRegistration { id = 0, StateName = "select", isactive = false, isdeleted = false });
            return Json(new SelectList(obj, "id", "StateName"));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Allcountry = _unitofWork.country.GetAll().Where(x=>x.isdeleted==false).Select(x=>new SelectListItem() { 
                Text=x.countryname,
                Value=x.id.ToString()
            });
            var model = new CityRegistrationCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var objcategory = new CityRegistration
                {
                    id = model.id
                    ,
                    stateid  = model.stateid
                    ,
                    cityName=model.cityName

                   ,
                    isdeleted = false
                    ,
                    isactive = false 

                };
                 
                _unitofWork.city.Add(objcategory);
               bool res= _unitofWork.Save();
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
            var objcategory = _unitofWork.city.Get(id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new CityRegistrationCreateViewModel()
            {
                id = objcategory.id,
                countryid = _unitofWork.state.Get(objcategory.stateid).countryid,
                stateid = objcategory.stateid,
                cityName = objcategory.cityName

            };
            ViewBag.States = _unitofWork.state.GetAll().Where(x=>x.isdeleted==false&&x.countryid==model.countryid).Select(x => new SelectListItem()
            {
                Text = x.StateName,
                Value = x.id.ToString()
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CityRegistrationCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.city.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.stateid = model.stateid;
                storeobj.cityName = model.cityName;


                _unitofWork.city.Update(storeobj);
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
            var obj = _unitofWork.city.GetAll().Where(x=>x.isdeleted==false).Select(x=>new CityRegistrationIndexViewModel() { 
            id=x.id,
               countryName= _unitofWork.country.Get(_unitofWork.state.Get(x.stateid).countryid).countryname,
               stateName= _unitofWork.state.Get(x.stateid).StateName,
               cityName=x.cityName
            });
            return Json(new { data = obj });

 
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.city.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.city.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}