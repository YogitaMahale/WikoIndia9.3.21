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
 

namespace Autofocus.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
  //  [Authorize(Roles =SD.Role_Admin)]
     
    public class packingSizeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
        public packingSizeController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
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
          
            var model = new packingSizeCreateViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(packingSizeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var objcategory = new packingSize
                {
                    id = model.id
                    ,
                    name   = model.name
                     
                   ,
                    isdeleted = false
                    ,
                    isactive = false 

                };
                 
                _unitofWork.packingSize.Add(objcategory);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Save successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);

            }
        }
        public IActionResult Edit(int id)
        {
           
            var objcategory = _unitofWork.packingSize.Get(id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new packingSizeCreateViewModel()
            {
                id = objcategory.id,
                name  = objcategory.name  

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(packingSizeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.packingSize.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.name   = model.name  ;
              
               
                _unitofWork.packingSize.Update(storeobj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Update successfully";
               
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }

        }

         
 
        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL()
        {
            var obj = _unitofWork.packingSize.GetAll().Where(x=>x.isdeleted==false);
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.packingSize.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.packingSize.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}