using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Utility;
using Autofocus.Models.Dtos;
using Autofocus.Models.ViewModels;
using System.IO;

namespace Autofocus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;
     //   private readonly IUnitofWork _unitofWork;
        private readonly IMainCategoryAPIRepository _mainCategoryRepository;
        public CategoryController( IMainCategoryAPIRepository mainCategoryRepository, IWebHostEnvironment hostingEnvironment)
        {
            _mainCategoryRepository = mainCategoryRepository;
            //   _unitofWork = unitofWork;IUnitofWork unitofWork,
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {

            var model = new MainCategoryCreateViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MainCategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                MainCategoryDtos obj = new MainCategoryDtos
                {
                    name = model.name
                };
                if (model.img != null && model.img.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        model.img.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        obj.img = s;
                        ms.Close();
                        // act on the Base64 data
                    }
                }
                string path = SD.APIBaseUrl + "Maincategory/CreateMainCategory";
                bool res = await _mainCategoryRepository.CreateAsync(path, obj);
                //_unitofWork.mainCategory.Add(obj);
                //bool res = _unitofWork.Save();
                if(res)
                {
                    TempData["success"] = "Record Save successfully";
                }
                else
                {
                    TempData["error"] = "Record Not Save";
                }
              
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();

            }
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(MainCategoryCreateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var obj = new MainCategory
        //        {
        //            id = model.id
        //            ,
        //            name   = model.name 

        //           ,
        //            isdeleted = false
        //            ,
        //            isactive = false 

        //        };
        //        if (model.img != null && model.img.Length > 0)
        //        {
        //            var uploadDir = @"uploads/MainCategory";
        //            var fileName = Path.GetFileNameWithoutExtension(model.img.FileName);
        //            var extesion = Path.GetExtension(model.img.FileName);
        //            var webRootPath = _hostingEnvironment.WebRootPath;
        //            fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //            var path = Path.Combine(webRootPath, uploadDir, fileName);
        //            FileStream fs = new FileStream(path, FileMode.Create);

        //            await model.img.CopyToAsync(fs);
        //            fs.Close();
        //            obj.img = '/' + uploadDir + '/' + fileName;

        //        }

        //        _unitofWork.mainCategory.Add(obj);
        //        bool res = _unitofWork.Save();
        //        TempData["success"] = "Record Save successfully";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        return View();

        //    }
        //}
        public async Task<IActionResult> Edit(int id)
        {
 
            string path = SD.APIBaseUrl + "Maincategory/GetMainCategorybyid?maincategoryId=";

            MainCategoryDtos objcategory = await _mainCategoryRepository.GetAsync(path, id);
            if (objcategory == null)
            {
                return NotFound();
            }
            var model = new MainCategoryCreateViewModel()
            {
                id = objcategory.id,
                name = objcategory.name,
                imgName = objcategory.img

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MainCategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string path = SD.APIBaseUrl + "Maincategory/GetMainCategorybyid?maincategoryId=";
                MainCategoryDtos obj = await _mainCategoryRepository.GetAsync(path, model.id);

                if (obj == null)
                {
                    TempData["error"] = "Record Not Found";
                    return NotFound();
                }
                obj.id = model.id;
                obj.name = model.name;
                if (model.img != null && model.img.Length > 0)
                {

                    using (var ms = new MemoryStream())
                    {
                        model.img.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        obj.img = s;
                        ms.Close();
                        // act on the Base64 data
                    }
                }
                else
                {
                    obj.img = "";
                }

               string  path1 = SD.APIBaseUrl + "Maincategory/UpdateMainCategory";
                bool res = await _mainCategoryRepository.UpdateAsync(path1, obj);
                if (res)
                {
                    TempData["success"] = "Record Update successfully";
                }
                else
                {
                    TempData["error"] = "Record Not Update";
                }
               
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }





        [HttpGet]
        public async Task<IActionResult> GetALL()
        {
            //var datalist = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).ToList();
            //var datalist = await _mainCategoryRepository.GetAllAsync("http://wikoindiawebapi.onlineerp.org/api/v1/MainCategory");
            var datalist = await _mainCategoryRepository.GetAllAsync(SD.APIBaseUrl + "Maincategory/GetMainCategory");
            return Json(new { data = datalist }) ; 
            //    return Json(new { data = await _mainCategoryRepository.GetAllAsync("http://wikoindiawebapi.onlineerp.org/api/v1/MainCategory") });
            //return Json(new { data = await _mainCategoryRepository.GetAllAsync(SD.MainCategoryAPIPath) });

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            bool res = await _mainCategoryRepository.DeleteAsync(SD.APIBaseUrl+ "Maincategory/DeleteMainCategory?maincategoryId=", id);
            if (res)
            {
                return Json(new { success = true, message = "Delete Successfuly" });

            }
            else
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }


        }
    }
}
