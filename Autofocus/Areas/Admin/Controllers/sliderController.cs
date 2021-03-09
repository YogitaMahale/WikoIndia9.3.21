using Autofocus.Entity;
using Autofocus.Models;
using Autofocus.DataAccess.Repository; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models.ViewModels;
using Autofocus.Utility;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Autofocus.Controllers
{
    [Area("Admin")]
   //  [Authorize(Roles = SD.Role_Admin )]
    public class sliderController : Controller
    {

        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public sliderController(IUnitofWork unitofWork
                               , IWebHostEnvironment hostingEnvironment
                                )
        {
            _hostingEnvironment = hostingEnvironment;
            _unitofWork = unitofWork;

        }
        public async Task<IActionResult> Index()
        {

            var listt = _unitofWork.slider.GetAll().Where(x => x.isdeleted == false).Select(x => new sliderIndexViewModel
            {
                id = x.id
                  ,
                name = x.name

            }).ToList();
            //  return View(storeList);


            return View(listt);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {



            var model = new sliderCreateViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(sliderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var obj = new slider
                {

                    id = model.id,

                    isdeleted = false
                    ,
                    isactive = false


                };
                if (model.name != null && model.name.Length > 0)
                {
                    var uploadDir = @"uploads/slider";
                    var fileName = Path.GetFileNameWithoutExtension(model.name.FileName);
                    var extesion = Path.GetExtension(model.name.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    FileStream fs = new FileStream(path, FileMode.Create);

                    await model.name.CopyToAsync(fs);
                    fs.Close();
                    obj.name = '/' + uploadDir + '/' + fileName;

                }
                  _unitofWork.slider.Add(obj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Saved successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();

            }
        }


        public async Task<IActionResult> Edit(int id)
        {



            var prod = _unitofWork.slider.Get(id);
            if (prod == null)
            {
                return NotFound();
            }
            var model = new sliderCreateViewModel()
            {
                id = prod.id,
                imgname = prod.name,


            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(sliderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var storeobj = _unitofWork.slider.Get(model.id);
                if (storeobj == null)
                {
                    return NotFound();
                }
                storeobj.id = model.id;


                if (model.name != null && model.name.Length > 0)
                {
                    var uploadDir = @"uploads/slider";
                    var fileName = Path.GetFileNameWithoutExtension(model.name.FileName);
                    var extesion = Path.GetExtension(model.name.FileName);
                    var webRootPath = _hostingEnvironment.WebRootPath;

                    if (storeobj.name != null)
                    {
                        var imagePath = webRootPath + storeobj.name.ToString().Replace("/", "\\");
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                    }
                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    FileStream fs = new FileStream(path, FileMode.Create);

                    await model.name.CopyToAsync(fs);
                    fs.Close();
                    storeobj.name = '/' + uploadDir + '/' + fileName;

                }

                  _unitofWork.slider.Update(storeobj);
                bool res = _unitofWork.Save();
                TempData["success"] = "Record Updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitofWork.slider.Get(id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
            obj.isdeleted = true;

            _unitofWork.slider.Update(obj);
            bool res = _unitofWork.Save();

            return Json(new { success = true, message = "Delete Successfuly" });
        }
    }
}
