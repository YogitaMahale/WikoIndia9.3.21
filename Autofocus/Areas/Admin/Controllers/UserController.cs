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
using Autofocus.Repository.IRepository;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

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
        private readonly IUserRegistrationAPIRepository _userRegistrationAPIRepository;
        private readonly IHttpClientFactory _clientFactory;
        public UserController(IHttpClientFactory clientFactory,IWebHostEnvironment hostingEnvironment, IUnitofWork unitofWork, ApplicationDbContext db, UserManager<IdentityUser> userManager, IUserRegistrationAPIRepository userRegistrationAPIRepository)
        {
            _unitofWork = unitofWork;         
            _hostingEnvironment = hostingEnvironment;
            _db = db;
            _userManager = userManager;
            _userRegistrationAPIRepository = userRegistrationAPIRepository;
            _clientFactory = clientFactory;

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


        //*******************************************************
        [HttpGet]
        public async Task<IActionResult> EditBasicInfo(string id)
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
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            var model = new EditBasicInformationModel
            {
                Id = objfromdb.Id,
                name = objfromdb.name,
                companyName  = objfromdb.companyName,
                businessYear = objfromdb.businessYear,
                //productDealin = objfromdb.productDealin,
                multipleproductDealin = objfromdb.productDealin,
                //ExportToCountries = objfromdb.ExportToCountries,
                multipleExportToCountries = objfromdb.ExportToCountries,
                userlatitude = objfromdb.userlatitude,
                userlongitude = objfromdb.userlongitude,
                packHouselatitude = objfromdb.packHouselatitude,
                packHouselongitude = objfromdb.packHouselongitude,
                packHouseAddress = objfromdb.packHouseAddress,
                deviceid = objfromdb.deviceid,
                //countryid = countryid,
                //stateid = stateid,
                //cityid = (int)objfromdb.cityid,
                logoName = objfromdb.logo,
                Email=objfromdb.Email,
                phonenumber=objfromdb.PhoneNumber,
                userType= roles[0].ToString()


            };

            if (objfromdb.cityid != null)
            {
                model.countryid = countryid;
                model.stateid = stateid;
                model.cityid = (int)objfromdb.cityid;

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
            }
         
            return View(model);

        }

 
        //EditBasicInformationModel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBasicInfo(EditBasicInformationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var obj = _db.ApplicationUser.FirstOrDefault(u => u.Id == model.Id);
                    if (obj == null)
                    {
                        return NotFound();
                    }
                   
                    BasicInformationDtos objBasicInformationDtos = new BasicInformationDtos();

                    objBasicInformationDtos.Id = model.Id;
                    objBasicInformationDtos.name = model.name;
                    objBasicInformationDtos.companyName = model.companyName;
                    objBasicInformationDtos.cityid = model.cityid;
                    objBasicInformationDtos.businessYear = model.businessYear;
                    objBasicInformationDtos.productDealin = model.multipleproductDealin;
                    objBasicInformationDtos.ExportToCountries = model.multipleExportToCountries;
                    objBasicInformationDtos.userlatitude = model.userlatitude;
                    objBasicInformationDtos.userlongitude = model.userlongitude;
                    objBasicInformationDtos.packHouselatitude = model.packHouselatitude;

                    objBasicInformationDtos.packHouselongitude = model.packHouselongitude;
                    objBasicInformationDtos.packHouseAddress = model.packHouseAddress;
                    objBasicInformationDtos.isBasicInfoFill = true ;
                    if (model.logo != null && model.logo.Length > 0)
                    {

                        using (var ms = new MemoryStream())
                        {
                            model.logo.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objBasicInformationDtos.logo = s;
                            ms.Close();
                            // act on the Base64 data
                        }
                    }
                    else
                    {
                        objBasicInformationDtos.logo = null;
                    }


                    string path1 = SD.APIBaseUrl + "user/UpdateBasicInformation";
                  //  var datalist = await _mainCategoryRepository.GetAllAsync(SD.APIBaseUrl + "Maincategory/GetMainCategory");
                    bool res = await _userRegistrationAPIRepository.UpdateAsync(path1, objBasicInformationDtos);

                    if(res)
                    {
                        TempData["success"] = "Record Update successfully";
                    }
                    else
                    {
                        TempData["error"] = "Record Not Update";

                    }

                    if(model.userType== "Admin")
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                         
                        return RedirectToAction("Profile", "UserLogin", new { area = "Admin", id = model.Id });
                    }
                    
                }
                catch { }
                return RedirectToAction(nameof(Index));
            }
            else
            {

                ViewBag.Countries = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).Select(x => new SelectListItem()
                {
                    Text = x.countryname,
                    Value = x.id.ToString()
                });                             
              
                int countryiddd = 0, stateid = 0, countryid = 0;
                if (model.cityid != null)
                {
                    int cityiddd = (int)model.cityid;
                    //  countryiddd = (int)objfromdb.cityid;
                    stateid = _unitofWork.city.Get(cityiddd).stateid;
                    countryid = _unitofWork.state.Get(stateid).countryid;
                }
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

        }
        //*******************************************************
        //---- Certification


       


        [HttpGet]
        public IActionResult EditUserCertification(string id)
        {


            var objfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);
            
            if (objfromdb == null)
            {
                return NotFound();
            }         

            var model = new CertificationInformationEditViewModel
            {
                Id = objfromdb.Id,
                Ceritication_IECImg = objfromdb.Ceritication_IEC,
                Ceritication_APEDAImg = objfromdb.Ceritication_APEDA,
                Ceritication_FIEOImg = objfromdb.Ceritication_FIEO,                
                Ceritication_GlobalGapImg = objfromdb.Ceritication_GlobalGap,                
                Ceritication_OthersImg = objfromdb.Ceritication_Others
           };
          
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserCertification(CertificationInformationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var obj = _db.ApplicationUser.FirstOrDefault(u => u.Id == model.Id);
                    if (obj == null)
                    {
                        return NotFound();
                    }

                    CertificationInformationDtos objCertificationInformationDtos = new CertificationInformationDtos();
                    objCertificationInformationDtos.Id = model.Id;
                    //Ceritication_IEC, Ceritication_APEDA, Ceritication_FIEO, Ceritication_GlobalGap, Ceritication_Others
                    objCertificationInformationDtos.isCertificationFill = true;
                    if (model.Ceritication_IEC != null && model.Ceritication_IEC.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritication_IEC.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritication_IEC = s;
                            ms.Close();                            
                        }
                    }
                    if (model.Ceritication_APEDA != null && model.Ceritication_APEDA.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritication_APEDA.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritication_APEDA = s;
                            ms.Close();
                        }
                    }

                    if (model.Ceritication_FIEO != null && model.Ceritication_FIEO.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritication_FIEO.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritication_FIEO = s;
                            ms.Close();
                        }
                    }
                    if (model.Ceritication_GlobalGap != null && model.Ceritication_GlobalGap.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritication_GlobalGap.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritication_GlobalGap = s;
                            ms.Close();
                        }
                    }

                    if (model.Ceritication_Others != null && model.Ceritication_Others.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritication_Others.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritication_Others = s;
                            ms.Close();
                        }
                    }

                    #region "API CALL"

                    string url = SD.APIBaseUrl + "user/UpdateCertificationInformation";
                    var request = new HttpRequestMessage(HttpMethod.Patch, url);
                    if (objCertificationInformationDtos != null)
                    {
                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(objCertificationInformationDtos), Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        //return false;
                    }

                    var client = _clientFactory.CreateClient();
                    //if (token != null && token.Length != 0)
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //}
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent|| response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["success"] = "Certification Doucument update successfully";
                        // return true;
                    }
                    else
                    {
                        TempData["error"] = "Record Not Update";
                        // return false;
                    }
                    #endregion
                    //    string path1 = SD.APIBaseUrl + "user/UpdateCertificationInformation";                   
                    //bool res = await _userRegistrationAPIRepository.UpdateAsync(path1, objCertificationInformationDtos);
                   
                }
                catch { }
                var user = await _userManager.FindByIdAsync(model.Id);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0].ToString() == "Admin")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    return RedirectToAction("Profile", "UserLogin", new { area = "Admin", id = model.Id });
                }

               // return RedirectToAction(nameof(Index));
            }
            else
            {

               

                return View(model);
            }

        }




        //****************EditUserDocumentation******************
        [HttpGet]
        public IActionResult EditUserDocumentation(string id)
        {
            //UserDocumentationEditViewModel
            var objfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == id);

            if (objfromdb == null)
            {
                return NotFound();
            }             

        var model = new UserDocumentationEditViewModel
            {
                Id = objfromdb.Id,
            CancellerdChequeImgName = objfromdb.CancellerdChequeImg,
            CeritificationImg = objfromdb.Ceritification,
            CompanyRegCeritificateImg = objfromdb.CompanyRegCeritificate,
            VisitingCardImgName = objfromdb.VisitingCardImg,
            aadharBackImgName = objfromdb.aadharBackImg,
             aadharFrontImgName = objfromdb.aadharFrontImg,
            pancardImgName = objfromdb.pancardImg,
             
        };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserDocumentation(UserDocumentationEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var obj = _db.ApplicationUser.FirstOrDefault(u => u.Id == model.Id);
                    if (obj == null)
                    {
                        return NotFound();
                    }

                    DocumentationDtos objCertificationInformationDtos = new DocumentationDtos();
                    objCertificationInformationDtos.Id = model.Id;
       
                    objCertificationInformationDtos.isDocumentationFill = true;
                    if (model.CancellerdChequeImg != null && model.CancellerdChequeImg.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.CancellerdChequeImg.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.CancellerdChequeImg = s;
                            ms.Close();
                        }
                    }
                    if (model.Ceritification != null && model.Ceritification.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.Ceritification.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.Ceritification = s;
                            ms.Close();
                        }
                    }

                    if (model.CompanyRegCeritificate != null && model.CompanyRegCeritificate.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.CompanyRegCeritificate.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.CompanyRegCeritificate = s;
                            ms.Close();
                        }
                    }
                    if (model.VisitingCardImg != null && model.VisitingCardImg.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.VisitingCardImg.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.VisitingCardImg = s;
                            ms.Close();
                        }
                    }

                    if (model.aadharBackImg != null && model.aadharBackImg.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.aadharBackImg.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.aadharBackImg = s;
                            ms.Close();
                        }
                    }


                    if (model.aadharFrontImg != null && model.aadharFrontImg.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.aadharFrontImg.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.aadharFrontImg = s;
                            ms.Close();
                        }
                    }


                    if (model.pancardImg != null && model.pancardImg.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            model.pancardImg.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            objCertificationInformationDtos.pancardImg = s;
                            ms.Close();
                        }
                    }

                    #region "API CALL"

                    string url = SD.APIBaseUrl + "user/UpdateUserDocumentation";
                    var request = new HttpRequestMessage(HttpMethod.Patch, url);
                    if (objCertificationInformationDtos != null)
                    {
                        request.Content = new StringContent(
                            JsonConvert.SerializeObject(objCertificationInformationDtos), Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        //return false;
                    }

                    var client = _clientFactory.CreateClient();
                    //if (token != null && token.Length != 0)
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //}
                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent || response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["success"] = "Record Update successfully";
                        // return true;
                    }
                    else
                    {
                        TempData["error"] = "Record Not Update";
                        // return false;
                    }
                    #endregion
                    //    string path1 = SD.APIBaseUrl + "user/UpdateCertificationInformation";                   
                    //bool res = await _userRegistrationAPIRepository.UpdateAsync(path1, objCertificationInformationDtos);

                }
                catch { }


                var user = await _userManager.FindByIdAsync(model.Id);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles[0].ToString() == "Admin")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    return RedirectToAction("Profile", "UserLogin", new { area = "Admin", id = model.Id });
                }
            }
            else
            {



                return View(model);
            }

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

        
        #endregion
    }
}