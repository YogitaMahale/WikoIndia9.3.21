using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using Autofocus.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Autofocus.Webapi
{
    [Route("user")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserRegistrationController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;


        }
        [HttpPost]
        [Route("saveUSer")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApplicationUser))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> saveUSer([FromBody] ApplicationUserSaveModelDtos model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var checkduplicate = _unitofWork.applicationUser.GetAll().Where(x => x.PhoneNumber == model.phoneNumber.Trim()).FirstOrDefault();

            if (checkduplicate == null)
            {
                string rolename = string.Empty;
                if (model.usertype.ToUpper().Trim() == "Buyer".ToUpper().Trim())
                {
                    rolename = SD.Role_Buyer;
                }
                else if (model.usertype.ToUpper().Trim() == "Seller".ToUpper().Trim())
                {
                    rolename = SD.Role_Seller;

                }

                var user = new ApplicationUser
                {
                    name = model.name,
                    UserName = model.phoneNumber,
                    PhoneNumber = model.phoneNumber,
                    Email = model.email,
                    createddate = DateTime.Now,
                    companyName = model.companyName,
                    cityid=model.cityid
                };


                var result = await _userManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Buyer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Buyer));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Seller))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Seller));
                    }

                    // await _userManager.AddToRoleAsync(user, SD.Role_Admin);
                    if (model.usertype.ToUpper().Trim() == "Buyer".ToUpper().Trim())
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Buyer);
                    }
                    else if (model.usertype.ToUpper().Trim() == "Seller".ToUpper().Trim())
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Seller);
                    }


                    string output = JsonConvert.SerializeObject(user);
                    string finalResult = "{\"success\" : 1, \"message\" : \" User Saved Successfully\", \"data\" :" + output + "}";

                    return Ok(finalResult);
                }

                else
                {
                    ModelState.AddModelError("", $"Something went wrong saving record");
                    return StatusCode(500, ModelState);
                }

            }
            else
            {

                ModelState.AddModelError("", "This Mobile No Exists!");
                return StatusCode(404, ModelState);

            }


            
        }


        [HttpGet]
        [Route("GetOTPNo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetOTPNo(string  mobileNo)
        {
            try
            {

                String no = null;
                Random random = new Random();
                for (int i = 0; i < 1; i++)
                {
                    int n = random.Next(0, 999);
                    no += n.ToString("D4") + "";
                }
                // ApplicationUserViewModelDtos objApplicationUserViewModelDtos = new ApplicationUserViewModelDtos();                
                var obj = _unitofWork.applicationUser.GetAll().Where(x => x.PhoneNumber == mobileNo.Trim()).FirstOrDefault();

                if (obj != null)
                {

                    //var Dtosobj = new ApplicationUserViewModelDtos();
                    //Dtosobj = _mapper.Map<ApplicationUserViewModelDtos>(obj);
                    string myJson = "{\"message\": " + "\"This Mobile Number Allready Exist\"" + "}";

                    
                    return StatusCode(409, myJson);
                    //  return Ok(Dtosobj);
                }
                else
                {
                    string Msg = "OTP :" + no + ".  Please Use this OTP.This is usable once and expire in 10 minutes";
                    //SendSMS s = new SendSMS();
                    //bool flg = s.smsSent(mobileNo, Msg);

                    ApplicationUserViewModelDtos objApplicationUserViewModelDtos = new ApplicationUserViewModelDtos();
                    objApplicationUserViewModelDtos.OTPNo = no;
                    objApplicationUserViewModelDtos.PhoneNumber  = mobileNo;
                    return Ok(objApplicationUserViewModelDtos);
                   // return Ok(objApplicationUserViewModelDtos);
                }

            }
            catch (Exception obj)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("LoginWithMobileNoandPassword")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginWhiteMobilenoandPasswordDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> LoginWithMobileNoandPassword([FromBody] LoginViewModel model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.mobileNo, model.password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var obj = _unitofWork.applicationUser.GetAll().Where(x => x.PhoneNumber == model.mobileNo).FirstOrDefault();

                if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    var Dtosobj = new LoginWhiteMobilenoandPasswordDtos();
                    Dtosobj = _mapper.Map<LoginWhiteMobilenoandPasswordDtos>(obj);
                    return Ok(Dtosobj);
                }


            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "User account locked out.");
                    return StatusCode(404, ModelState);


                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return StatusCode(404, ModelState);


                }


            }
        }
        [HttpGet]
        [Route("LoginWithMobileNo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginWhiteMobilenoandPasswordDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> LoginWithMobileNo(string mobileNo)
        {
            if(!string.IsNullOrEmpty(mobileNo))
            {

                var obj = _unitofWork.applicationUser.GetAll().Where(x => x.PhoneNumber == mobileNo).FirstOrDefault();
                if (obj == null)
                {
                    ModelState.AddModelError("", "Mobile No Not Found");
                    return StatusCode(404, ModelState);
                }
                else
                {

                    String no = null;
                    Random random = new Random();
                    for (int i = 0; i < 1; i++)
                    {
                        int n = random.Next(0, 999);
                        no += n.ToString("D4") + "";
                    }
                    string Msg = "OTP :" + no + ".  Please Use this OTP.This is usable once and expire in 10 minutes";
                    SendSMS s = new SendSMS();
                    bool flg = s.smsSent(mobileNo, Msg);

                  

                    var Dtosobj = new LoginWhiteMobilenoandPasswordDtos();
                    Dtosobj = _mapper.Map<LoginWhiteMobilenoandPasswordDtos>(obj);
                    Dtosobj.OTP = no;
                    return Ok(Dtosobj);
                }

            }
            else
            {
                ModelState.AddModelError("", "Please Enter Mobile No");
                return StatusCode(404, ModelState);
            }            
        }

        [HttpPatch]
        [Route("UpdateBasicInformation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasicInformationDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBasicInformation([FromBody] BasicInformationDtos model)//[FromBody] BasicInformationDtos model,
        {

            if (model == null)
            {
                return BadRequest();
            }
            var obj = _unitofWork.applicationUser.GetAll().Where(x => x.Id == model.Id).FirstOrDefault();
            //  var obj = _db.ApplicationUser.Where(x => x.Id == model.Id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                obj.name = model.name;
                obj.companyName = model.companyName;
                obj.cityid = model.cityid;
                obj.businessYear = model.businessYear;
                obj.productDealin = model.productDealin;
                obj.ExportToCountries = model.ExportToCountries;
                obj.userlatitude = model.userlatitude;
                obj.userlongitude = model.userlongitude;
                obj.packHouselatitude = model.packHouselatitude;
                obj.packHouselongitude = model.packHouselongitude;
                obj.packHouseAddress = model.packHouseAddress;
                obj.deviceid = model.deviceid;
                obj.isBasicInfoFill = model.isBasicInfoFill;

            }
            if (model.logo == null || model.logo.Trim() == "")
            {
               // obj.logo = "";
            }
            else
            {
                SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
                string iamgename = objsaveImageinFolder.uploadImage(obj.logo, "\\uploads\\user\\documentation", model.logo);
                obj.logo = iamgename;

            }

            var result = await _userManager.UpdateAsync(obj);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var Dtosobj = new BasicInformationDtos();
            Dtosobj = _mapper.Map<BasicInformationDtos>(obj);
            return Ok(Dtosobj);

        }


        [HttpPatch]
        [Route("UpdateCertificationInformation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CertificationInformationDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCertificationInformation([FromBody] CertificationInformationDtos model)//[FromBody] BasicInformationDtos model,
        {
            SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
            if (model == null)
            {
                return BadRequest();
            }
            var obj = _unitofWork.applicationUser.GetAll().Where(x => x.Id == model.Id).FirstOrDefault();
            //  var obj = _db.ApplicationUser.Where(x => x.Id == model.Id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            else
            {

                if (model.Ceritication_IEC == null || model.Ceritication_IEC.Trim() == "")
                {
                    obj.Ceritication_IEC = "";
                }
                else
                {
                  
                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritication_IEC, "\\uploads\\user\\Certification", model.Ceritication_IEC);
                    obj.Ceritication_IEC = iamgename;               

                }
                if (model.Ceritication_APEDA == null || model.Ceritication_APEDA.Trim() == "")
                {
                    obj.Ceritication_APEDA = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritication_APEDA, "\\uploads\\user\\Certification", model.Ceritication_APEDA);
                    obj.Ceritication_APEDA = iamgename;

                }
                if (model.Ceritication_FIEO == null || model.Ceritication_FIEO.Trim() == "")
                {
                    obj.Ceritication_FIEO = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritication_FIEO, "\\uploads\\user\\Certification", model.Ceritication_FIEO);
                    obj.Ceritication_FIEO = iamgename;

                }
                if (model.Ceritication_GlobalGap == null || model.Ceritication_GlobalGap.Trim() == "")
                {
                    obj.Ceritication_GlobalGap = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritication_GlobalGap, "\\uploads\\user\\Certification", model.Ceritication_GlobalGap);
                    obj.Ceritication_GlobalGap = iamgename;

                }
                if (model.Ceritication_Others == null || model.Ceritication_Others.Trim() == "")
                {
                    obj.Ceritication_Others = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritication_Others, "\\uploads\\user\\Certification", model.Ceritication_Others);
                    obj.Ceritication_Others = iamgename;

                }
                
                obj.isCertificationFill = model.isCertificationFill;
                 
            }


          var result = await _userManager.UpdateAsync(obj);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var Dtosobj = new CertificationInformationDtos();
            Dtosobj = _mapper.Map<CertificationInformationDtos>(obj);
            return Ok(Dtosobj);

        }
        [HttpPatch]
        [Route("UpdateUserDocumentation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CertificationInformationDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUserDocumentation([FromBody] DocumentationDtos model)//[FromBody] BasicInformationDtos model,
        {
            SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
            if (model == null)
            {
                return BadRequest();
            }
            var obj = _unitofWork.applicationUser.GetAll().Where(x => x.Id == model.Id).FirstOrDefault();
            //  var obj = _db.ApplicationUser.Where(x => x.Id == model.Id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
  
                if (model.CancellerdChequeImg == null || model.CancellerdChequeImg.Trim() == "")
                {
                    obj.CancellerdChequeImg = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.CancellerdChequeImg, "\\uploads\\user\\documentation", model.CancellerdChequeImg);
                    obj.CancellerdChequeImg = iamgename;

                }
                if (model.Ceritification == null || model.Ceritification.Trim() == "")
                {
                    obj.Ceritification = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.Ceritification, "\\uploads\\user\\documentation", model.Ceritification);
                    obj.Ceritification = iamgename;

                }
                if (model.CompanyRegCeritificate == null || model.CompanyRegCeritificate.Trim() == "")
                {
                    obj.CompanyRegCeritificate = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.CompanyRegCeritificate, "\\uploads\\user\\documentation", model.CompanyRegCeritificate);
                    obj.CompanyRegCeritificate = iamgename;

                }
                if (model.VisitingCardImg == null || model.VisitingCardImg.Trim() == "")
                {
                    obj.VisitingCardImg = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.VisitingCardImg, "\\uploads\\user\\documentation", model.VisitingCardImg);
                    obj.VisitingCardImg = iamgename;

                }
                if (model.aadharBackImg == null || model.aadharBackImg.Trim() == "")
                {
                    obj.aadharBackImg = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.aadharBackImg, "\\uploads\\user\\documentation", model.aadharBackImg);
                    obj.aadharBackImg = iamgename;

                }
                if (model.aadharFrontImg == null || model.aadharFrontImg.Trim() == "")
                {
                    obj.aadharFrontImg = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.aadharFrontImg, "\\uploads\\user\\documentation", model.aadharFrontImg);
                    obj.aadharFrontImg = iamgename;

                }
               
                if (model.pancardImg == null || model.pancardImg.Trim() == "")
                {
                    obj.pancardImg = "";
                }
                else
                {

                    string iamgename = objsaveImageinFolder.uploadImage(obj.pancardImg, "\\uploads\\user\\documentation", model.pancardImg);
                    obj.pancardImg = iamgename;

                }

                obj.isDocumentationFill = model.isDocumentationFill;

            }


            var result = await _userManager.UpdateAsync(obj);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var Dtosobj = new DocumentationDtos();
            Dtosobj = _mapper.Map<DocumentationDtos>(obj);
            return Ok(Dtosobj);

        }


        [HttpGet]
        [Route("GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserInformationViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetUserDetails(String id)
        {
            // var affilatereg = _db.applicationUsers.FirstOrDefault(u => u.Id == model.id);
            var obj = _unitofWork.applicationUser.GetAll().Where(x => x.Id == id).FirstOrDefault();
            //var obj = _unitofWork.applicationUser.GetAll().Where(x => x.Id == id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var Dtosobj = new UserInformationViewModelDtos();
                Dtosobj = _mapper.Map<UserInformationViewModelDtos>(obj);
                return Ok(Dtosobj);
            }

             
        }
    }
}
