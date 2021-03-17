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
using System.Net.Http;

namespace Autofocus.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles =SD.Role_Admin)]
     
    public class SubCategoryController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
       

        public SubCategoryController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
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
            ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x=>x.isdeleted==false).Select(x=>new SelectListItem() { 
                Text=x.name ,
                Value=x.id.ToString()
            });
            ViewBag.AllCountries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.countryname,
                Value = x.id.ToString()
            });
            var model = new SubcategoryCreateViewModel();
            return View(model);
        }
        public JsonResult getcitybyState(int state)
        {

            IList<CityRegistration> obj = _unitofWork.city.GetAll().Where(x => x.stateid == state).ToList();

            return Json(new SelectList(obj, "id", "cityName"));
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubcategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var objcategory = new Subcategory
                {
                    id = model.id
                    ,
                    name  = model.name
                    ,
                    mainCategroyId=model.mainCategroyId
                    ,cityIds=model.multipleCityIds
                    ,stateid=model.stateid
                   ,
                    isdeleted = false
                    ,
                    isactive = false 

                };                
                if (model.img != null && model.img.Length > 0)
                {
                    var uploadDir = @"uploads/Subcategory";
                    var fileName = Path.GetFileNameWithoutExtension(model.img.FileName);
                    var extesion = Path.GetExtension(model.img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    FileStream fs = new FileStream(path, FileMode.Create);

                    await model.img.CopyToAsync(fs);
                    fs.Close();
                    objcategory.img = '/' + uploadDir + '/' + fileName;

                }
                _unitofWork.subcategory.Add(objcategory);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Save successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
                {
                    Text = x.name,
                    Value = x.id.ToString()
                });
                return View(model);

            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.name,
                Value = x.id.ToString()
            });
            ViewBag.AllCountries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.countryname,
                Value = x.id.ToString()
            });
            var objcategory = _unitofWork.subcategory.Get(id);
            int countryiddd = 0, stateid = 0, countryid = 0;

            if (objcategory.cityIds != null)
            {
                string  cityiddd = objcategory.cityIds;
                //  countryiddd = (int)objfromdb.cityid;
                stateid = objcategory.stateid;
                countryid = _unitofWork.state.Get(stateid).countryid;
            }

          
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new SubcategoryCreateViewModel()
            {
                id = objcategory.id,
                mainCategroyId  = objcategory.mainCategroyId ,
                name  = objcategory.name ,
                imgName=objcategory.img,
                  countryid = countryid,
                stateid = stateid,
             //   cityIds = (int)objfromdb.cityid,
                multipleCityIds = objcategory.cityIds
            };
            ViewBag.States = _unitofWork.state.GetAll().Where(x => x.isdeleted == false && x.countryid == model.countryid).Select(x => new SelectListItem()
            {
                Text = x.StateName,
                Value = x.id.ToString()
            });
            ViewBag.Cities = _unitofWork.city.GetAll().Where(x => x.isdeleted == false && x.stateid == model.stateid).Select(x => new SelectListItem()
            {
                Text = x.cityName,
                Value = x.id.ToString() 
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SubcategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.subcategory.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.mainCategroyId = model.mainCategroyId;
                storeobj.name = model.name ;
                storeobj.cityIds = model.multipleCityIds;
                if (model.img != null && model.img.Length > 0)
                {
                    var uploadDir = @"uploads/Subcategory";
                    var fileName = Path.GetFileNameWithoutExtension(model.img.FileName);
                    var extesion = Path.GetExtension(model.img.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    if (storeobj.img != null)
                    {
                        var imagePath = webRootPath + storeobj.img.ToString().Replace("/", "\\");
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    FileStream fs = new FileStream(path, FileMode.Create);

                    await model.img.CopyToAsync(fs);
                    fs.Close();
                    storeobj.img = '/' + uploadDir + '/' + fileName;

                }

                _unitofWork.subcategory.Update(storeobj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Update successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
                {
                    Text = x.name,
                    Value = x.id.ToString()
                });
                return View(model);
            }

        }



        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL()
        {

            var obj = _unitofWork.subcategory.GetAll(includeProperties: "MainCategory").Where(x => x.isdeleted == false);
            return Json(new { data = obj });
           
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.subcategory.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.subcategory.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}