using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks; 
using Autofocus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Authorization;
using Autofocus.Utility;
using Autofocus.DataAccess; 
using Microsoft.AspNetCore.Identity; 
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dapper;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.DataAccess.Data;
using Autofocus.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Autofocus.Models.Dtos;

namespace Autofocus.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    
    //[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _db;
        public UserController(IWebHostEnvironment hostingEnvironment, IUnitofWork unitofWork, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;         
            _hostingEnvironment = hostingEnvironment;
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewBag.rolesList = _db.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var obj = new UserIndexViewModel();
            return View(obj);
        }

        public JsonResult getSubcategory(int cityid)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@cityId", cityid);
            var obj = _unitofWork.sp_call.List<SubcategoryDtos>("GetSubCategorybyCityId", parameter);
            
            return Json(new SelectList(obj, "id", "name"));
        }
        [HttpGet]
        public IActionResult EditBasicInfo(string id)
        {


            var objfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            ViewBag.Countries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
            {
                Text = x.countryname,
                Value = x.id.ToString()
            });

            int countryiddd = 0, stateid = 0, countryid = 0;

            if (objfromdb.cityid != null)
            {
                int cityiddd = (int)objfromdb.cityid;
                //  countryiddd = (int)objfromdb.cityid;
                stateid = _unitofWork.city.Get(cityiddd).stateid;
                countryid = _unitofWork.state.Get(stateid).countryid;
            }

            if (objfromdb == null)
            {
                return NotFound();
            }
        //    Id ,name ,         
        // companyName ,      
        // countryid ,       
        // stateid ,               
        // cityid ,
        //    businessYear ,       
        // productDealin ,         
        // ExportToCountries ,
        // userlatitude ,
        // userlongitude ,
        // packHouselatitude ,
        // packHouselongitude ,
        // packHouseAddress ,
        // deviceid ,
        //isBasicInfoFill ,
        //logo ,
        //logoName ,

            var model = new EditBasicInformationModel
            {
                Id = objfromdb.Id,
                name = objfromdb.name,
                companyName  = objfromdb.companyName,
                businessYear = objfromdb.businessYear,
                productDealin = objfromdb.productDealin,
                ExportToCountries = objfromdb.ExportToCountries,
                userlatitude = objfromdb.userlatitude,
                userlongitude = objfromdb.userlongitude,
                packHouselatitude = objfromdb.packHouselatitude,
                packHouselongitude = objfromdb.packHouselongitude,
                packHouseAddress = objfromdb.packHouseAddress,
                deviceid = objfromdb.deviceid,
                countryid = countryid,
                stateid = stateid,
                cityid = (int)objfromdb.cityid,
                logoName = objfromdb.logo,
                 
                 
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


        //[HttpGet]
        //public IActionResult Edit(string id)
        //{



        //    var objfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
        //    ViewBag.Countries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
        //    {
        //        Text = x.countryname,
        //        Value = x.id.ToString()
        //    });

        //    int countryiddd = 0, stateid = 0, countryid = 0;

        //    if (objfromdb.cityid != null)
        //    {
        //        int cityiddd = (int)objfromdb.cityid;
        //        //  countryiddd = (int)objfromdb.cityid;
        //        stateid = _unitofWork.city.Get(cityiddd).stateid;
        //        countryid = _unitofWork.state.Get(stateid).countryid;
        //    }

        //    if (objfromdb == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = new ApplicationUserEditModel
        //    {
        //        Id = objfromdb.Id,
        //        Email = objfromdb.Email,
        //        phonenumber = objfromdb.PhoneNumber,
        //        CancellerdChequeImgName = objfromdb.CancellerdChequeImg,
        //        Ceritification = objfromdb.Ceritification,
        //        CompanyRegCeritificateName = objfromdb.CompanyRegCeritificate,
        //        ExportToCountries = objfromdb.ExportToCountries,
        //        VisitingCardImgName = objfromdb.VisitingCardImg,
        //        aadharBackImgName = objfromdb.aadharBackImg,
        //        aadharFrontImgName = objfromdb.aadharFrontImg,
        //        businessYear = objfromdb.businessYear,
        //        countryid = countryid,
        //        stateid = stateid,
        //        cityid = (int)objfromdb.cityid,
        //        logoName = objfromdb.logo,
        //        name = objfromdb.name,
        //        pancardImgName = objfromdb.pancardImg,
        //        productDealin = objfromdb.productDealin
        //    };
        //    ViewBag.States = _unitofWork.state.GetAll().Where(x => x.isdeleted == false && x.countryid == model.countryid).Select(x => new SelectListItem()
        //    {
        //        Text = x.StateName,
        //        Value = x.id.ToString()
        //    });
        //    ViewBag.Cities = _unitofWork.city.GetAll().Where(x => x.isdeleted == false && x.stateid == model.stateid).Select(x => new SelectListItem()
        //    {
        //        Text = x.cityName,
        //        Value = x.id.ToString()
        //    });
        //    return View(model);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ApplicationUserEditModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            var obj = _db.ApplicationUser.FirstOrDefault(u => u.Id == model.Id);
        //            if (obj == null)
        //            {
        //                return NotFound();
        //            }
        //            obj.name = model.name;
        //            //  UserName = model.phonenumber,
        //            obj.Email = model.Email;
        //            obj.NormalizedEmail = model.Email;
        //            //PhoneNumber = model.phonenumber,
        //            // Role = model.Role,
        //            obj.Ceritification = model.Ceritification;
        //            obj.ExportToCountries = model.ExportToCountries;
        //            obj.businessYear = model.businessYear;
        //            obj.cityid = (int)model.cityid;
        //            obj.createddate = DateTime.Now;
        //            obj.productDealin = model.productDealin;

        //            var webRootPath = _hostingEnvironment.WebRootPath;
        //            if (model.logo != null && model.logo.Length > 0)
        //            {
        //                if (obj.logo != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.logo.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/logo";
        //                var fileName = Path.GetFileNameWithoutExtension(model.logo.FileName);
        //                var extesion = Path.GetExtension(model.logo.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.logo.CopyToAsync(fs);
        //                fs.Close();
        //                obj.logo = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.aadharFrontImg != null && model.aadharFrontImg.Length > 0)
        //            {
        //                if (obj.aadharFrontImg != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.aadharFrontImg.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/aadhar";
        //                var fileName = Path.GetFileNameWithoutExtension(model.aadharFrontImg.FileName);
        //                var extesion = Path.GetExtension(model.aadharFrontImg.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.aadharFrontImg.CopyToAsync(fs);
        //                fs.Close();
        //                obj.aadharFrontImg = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.aadharBackImg != null && model.aadharBackImg.Length > 0)
        //            {
        //                if (obj.aadharBackImg != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.aadharBackImg.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }


        //                var uploadDir = @"uploads/user/aadhar";
        //                var fileName = Path.GetFileNameWithoutExtension(model.aadharBackImg.FileName);
        //                var extesion = Path.GetExtension(model.aadharBackImg.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.aadharBackImg.CopyToAsync(fs);
        //                fs.Close();
        //                obj.aadharBackImg = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.CancellerdChequeImg != null && model.CancellerdChequeImg.Length > 0)
        //            {
        //                if (obj.CancellerdChequeImg != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.CancellerdChequeImg.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/cancelChequeImg";
        //                var fileName = Path.GetFileNameWithoutExtension(model.CancellerdChequeImg.FileName);
        //                var extesion = Path.GetExtension(model.CancellerdChequeImg.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.CancellerdChequeImg.CopyToAsync(fs);
        //                fs.Close();
        //                obj.CancellerdChequeImg = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.CompanyRegCeritificate != null && model.CompanyRegCeritificate.Length > 0)
        //            {
        //                if (obj.CompanyRegCeritificate != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.CompanyRegCeritificate.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/companyRegCeritificate";
        //                var fileName = Path.GetFileNameWithoutExtension(model.CompanyRegCeritificate.FileName);
        //                var extesion = Path.GetExtension(model.CompanyRegCeritificate.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.CompanyRegCeritificate.CopyToAsync(fs);
        //                fs.Close();
        //                obj.CompanyRegCeritificate = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.pancardImg != null && model.pancardImg.Length > 0)
        //            {
        //                if (obj.pancardImg != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.pancardImg.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/pancard";
        //                var fileName = Path.GetFileNameWithoutExtension(model.pancardImg.FileName);
        //                var extesion = Path.GetExtension(model.pancardImg.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.pancardImg.CopyToAsync(fs);
        //                fs.Close();
        //                obj.pancardImg = '/' + uploadDir + '/' + fileName;
        //            }
        //            if (model.VisitingCardImg != null && model.VisitingCardImg.Length > 0)
        //            {
        //                if (obj.VisitingCardImg != null)
        //                {
        //                    //  var imagePath = Path.Combine(webRootPath, storeobj.img.TrimStart('\\'));
        //                    var imagePath = webRootPath + obj.VisitingCardImg.ToString().Replace("/", "\\");
        //                    if (System.IO.File.Exists(imagePath))
        //                    {
        //                        System.IO.File.Delete(imagePath);
        //                    }

        //                }

        //                var uploadDir = @"uploads/user/visitingCardImg";
        //                var fileName = Path.GetFileNameWithoutExtension(model.VisitingCardImg.FileName);
        //                var extesion = Path.GetExtension(model.VisitingCardImg.FileName);

        //                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
        //                var path = Path.Combine(webRootPath, uploadDir, fileName);

        //                FileStream fs = new FileStream(path, FileMode.Create);
        //                await model.VisitingCardImg.CopyToAsync(fs);
        //                fs.Close();
        //                obj.VisitingCardImg = '/' + uploadDir + '/' + fileName;
        //            }


        //            var result = await _userManager.UpdateAsync(obj);

        //        }
        //        catch { }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        ViewBag.Countries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
        //        {
        //            Text = x.countryname,
        //            Value = x.id.ToString()
        //        });

        //        int countryiddd = 0, stateid = 0, countryid = 0;



        //        if (model.cityid != null)
        //        {
        //            int cityiddd = (int)model.cityid;
        //            //  countryiddd = (int)objfromdb.cityid;
        //            stateid = _unitofWork.city.Get(cityiddd).stateid;
        //            countryid = _unitofWork.state.Get(stateid).countryid;
        //        }
        //        ViewBag.States = _unitofWork.state.GetAll().Where(x => x.isdeleted == false && x.countryid == model.countryid).Select(x => new SelectListItem()
        //        {
        //            Text = x.StateName,
        //            Value = x.id.ToString()
        //        });
        //        ViewBag.Cities = _unitofWork.city.GetAll().Where(x => x.isdeleted == false && x.stateid == model.stateid).Select(x => new SelectListItem()
        //        {
        //            Text = x.cityName,
        //            Value = x.id.ToString()
        //        });
        //        return View(model);
        //    }

        //}

        #region "API CALL"
        [HttpGet]
        public IActionResult GetALL(string id)
        {
           
            List<string> userids = _db.UserRoles.Where(x => x.RoleId ==id).Select(b => b.UserId).Distinct().ToList();
            var userList = _db.ApplicationUser.Where(x => userids.Any(c => c == x.Id)).ToList();

            return Json(new { data = userList.ToList() });

        }





        [HttpPost]
        public IActionResult Lockunlock([FromBody] string id)
        {
            var objfromdb = _unitofWork.applicationUser.GetAll().Where(u => u.Id == id).FirstOrDefault();
            if (objfromdb == null)
            {
                return Json(new { success = false, message = "error while locking / unlocking" });
            }
            if (objfromdb.LockoutEnd != null && objfromdb.LockoutEnd > DateTime.Now)
            {
                objfromdb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objfromdb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation successful" });
        }


        //[HttpDelete]
        [HttpPost]
        public IActionResult Delete([FromBody] string id)
        {
            var obj = _unitofWork.applicationUser.GetAll().Where(x=>x.Id==id).FirstOrDefault();
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleteing" });
            }
           

            _unitofWork.applicationUser.Remove(obj);
            _unitofWork.Save();

            return Json(new { success = true, message = "Delete Successfuly" });
        }

        public IActionResult map()
        {
            return View();
        }

        #endregion
    }
}