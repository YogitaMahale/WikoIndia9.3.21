using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.DataAccess.Repository;
using Autofocus.Models;
//using Autofocus.Models.ViewModels;
using Autofocus.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using Autofocus.Models.ViewModels;
using Autofocus.Repository.IRepository;
using Autofocus.Models.Dtos;
using System.Net.Http;
using Autofocus.Models.Dtos;

namespace Autofocus.Areas.Admin.Controllers//CoreMoryatools.Areas.Admin.Controllers
{
    [Area("Admin")]
  //[Authorize(Roles = SD.Role_Admin)]

    public class MainCategoryController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;
        private readonly IMainCategoryAPIRepository _mainCategoryRepository;
        public MainCategoryController(IUnitofWork unitofWork,IMainCategoryAPIRepository mainCategoryRepository, IWebHostEnvironment hostingEnvironment)
        {
            _mainCategoryRepository = mainCategoryRepository;
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
                 bool res= await _mainCategoryRepository.CreateAsync(path, obj);

                //_unitofWork.mainCategory.Add(obj);
                //bool res = _unitofWork.Save();
                TempData["success"] = "Record Save successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();

            }
        }
       
        public async Task<IActionResult> Edit(int id)
        {
            //https://localhost:44368/Maincategory/GetMainCategorybyid?maincategoryId=1036
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
                    obj.img = null;
                }
                
                bool res= await  _mainCategoryRepository.UpdateAsync(SD.APIBaseUrl + "Maincategory/UpdateMainCategory", obj);                
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
        public async Task<IActionResult> GetALL()
        {
            string path = SD.APIBaseUrl + "Maincategory/GetMainCategory";
            return Json(new { data = await _mainCategoryRepository.GetAllAsync(path) });



       

            //return Json(new { data = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).ToList() }); ;
            //    return Json(new { data = await _mainCategoryRepository.GetAllAsync("http://wikoindiawebapi.onlineerp.org/api/v1/MainCategory") });
            //return Json(new { data = await _mainCategoryRepository.GetAllAsync(SD.MainCategoryAPIPath) });

        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
           
           string path = SD.APIBaseUrl + "Maincategory/DeleteMainCategory?maincategoryId=";
            bool res= await _mainCategoryRepository.DeleteAsync(path,id);
            if (res)
            {
                return Json(new { success = true, message = "Delete Successfuly" });
                
            }
            else
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }

            
        }
        #endregion
    }
}