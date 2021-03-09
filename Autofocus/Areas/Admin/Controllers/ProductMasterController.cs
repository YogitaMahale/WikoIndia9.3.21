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
     
    public class ProductMasterController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
        public ProductMasterController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()        
        {
            ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.name,
                Value = x.id.ToString()
            });
            var obj = new productMasterViewModel();
            return View(obj);
        }
     
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x=>x.isdeleted==false).Select(x=>new SelectListItem() { 
                Text=x.name ,
                Value=x.id.ToString()
            });
            var model = new productMasterViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(productMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
               // id, subCategroyId, name, img, description, isdeleted, isactive
                var objcategory = new ProductMaster
                {
                    id = model.id
                    ,
                    name = model.name
                    ,
                    subCategroyId = model.subCategroyId
                    ,description=model.description
                   ,
                    isdeleted = false
                    ,
                    isactive = false

                };
                if (model.img != null && model.img.Length > 0)
                {
                    var uploadDir = @"uploads/ProductMaster";
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
                _unitofWork.productMaster.Add(objcategory);
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
            return View();
        }
        public IActionResult Edit(int id)
        {
            ViewBag.AllMainCategory = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.name,
                Value = x.id.ToString()
            });

            var objcategory = _unitofWork.productMaster.Get(id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new productMasterViewModel()
            {
                id = objcategory.id,
                mainCategroyId  = _unitofWork.subcategory.Get(objcategory.subCategroyId).mainCategroyId,
                subCategroyId = objcategory.subCategroyId,
                name  = objcategory.name ,
                imgname=objcategory.img,
                description=objcategory.description 

            };
            ViewBag.AllSubcategory = _unitofWork.subcategory.GetAll().Where(x => x.isdeleted == false&&x.mainCategroyId==model.mainCategroyId).Select(x => new SelectListItem()
            {
                Text = x.name,
                Value = x.id.ToString()
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(productMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.productMaster.Get(model.id);
                if (storeobj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                storeobj.id = model.id;
                storeobj.subCategroyId = model.subCategroyId;
                storeobj.name = model.name ;
                storeobj.description = model.description;
               
                if (model.img != null && model.img.Length > 0)
                {
                    var uploadDir = @"uploads/ProductMaster";
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

                _unitofWork.productMaster.Update(storeobj);
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
                var objcategory = _unitofWork.productMaster.Get(model.id);
                if (objcategory == null)
                {
                    return NotFound();
                }
                var model1 = new productMasterViewModel()
                {
                    id = objcategory.id,
                    mainCategroyId = _unitofWork.subcategory.Get(objcategory.subCategroyId).mainCategroyId,
                    subCategroyId = objcategory.subCategroyId,
                    name = objcategory.name,
                    imgname = objcategory.img,
                    description = objcategory.description

                };
                ViewBag.AllSubcategory = _unitofWork.subcategory.GetAll().Where(x => x.isdeleted == false && x.mainCategroyId == model.mainCategroyId).Select(x => new SelectListItem()
                {
                    Text = x.name,
                    Value = x.id.ToString()
                });
                return View(model1);
            }

        }



        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL(int subCategroyId)
        {

            var obj = _unitofWork.productMaster.GetAll(includeProperties: "Subcategory").Where(x => x.isdeleted == false&&x.subCategroyId==subCategroyId);
            return Json(new { data = obj });
           
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.productMaster.Get(id);
            if(obj==null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.productMaster.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true , message = "Delete Successfuly" });
        }
        #endregion
    }
}