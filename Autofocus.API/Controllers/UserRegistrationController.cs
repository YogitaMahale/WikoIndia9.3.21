using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.DataAccess.Repository.IRepository;
using AutoMapper;
using Autofocus.API.Dtos;
using Autofocus.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net;
using Autofocus.Utility;
using Microsoft.AspNetCore.Identity;
using Autofocus.DataAccess.Data;

namespace Autofocus.API.Controllers
{

    [ApiController]
    [Route("api/v{version:apiversion}/UserRegistration")]
    [ApiVersion("1.0")]
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
        /// <summary>
        /// Get User Details by User ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns></returns>
        [HttpGet("[action]/{id}", Name = "GetUserDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetUserDetails(String  id)
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
                var Dtosobj = new ApplicationUserViewModelDtos();
                Dtosobj = _mapper.Map<ApplicationUserViewModelDtos>(obj);
                return Ok(Dtosobj);
            }
           
            return Ok(obj);
        }

        /// <summary>
        /// Get OTP No
        /// </summary>
        /// <param name="mobileNo">Mobile No</param>
        /// <returns></returns>

        [HttpGet("[action]/{mobileNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetOTPNo(string mobileNo)
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

                    var Dtosobj = new ApplicationUserViewModelDtos();
                    Dtosobj = _mapper.Map<ApplicationUserViewModelDtos>(obj);


                    return Ok(Dtosobj);
                }
                else
                {
                    #region "sms"
                    try
                    {

                        string Password = "Bingo@5151";
                        string Msg = "OTP :" + no + ".  Please Use this OTP.This is usable once and expire in 10 minutes"; ;
                        string userdetails = "ezacus";
                        string OPTINS = "MSGSAY";
                        string MobileNumber = mobileNo;
                        // string type = "3";
                        string template_id = "123";
                        string strUrl = "https://bulksms.co/sendmessage.php?&user=" + userdetails + "&password=" + Password + "&mobile=" + MobileNumber + "&message=" + Msg + "&sender=" + OPTINS + "&type=" + 3 + "&template_id=" + template_id;
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        System.Net.WebRequest request = System.Net.WebRequest.Create(strUrl);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream s = (Stream)response.GetResponseStream();
                        StreamReader readStream = new StreamReader(s);
                        string dataString = readStream.ReadToEnd();
                        s.Close();
                        readStream.Close();
                        response.Close();

                    }

                    catch
                    { }
                    #endregion
                    ApplicationUserViewModelDtos objApplicationUserViewModelDtos = new ApplicationUserViewModelDtos();
                    objApplicationUserViewModelDtos.OTPNo = no;
                    return Ok(objApplicationUserViewModelDtos);
                }

            }
            catch (Exception obj)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add New User      
        /// </summary>
        /// <param name="model"> usertype : Buyer / Seller  </param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApplicationUserSaveModelDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SaveUser([FromBody] ApplicationUserSaveModelDtos model)
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
                    UserName= model.phoneNumber,
                    PhoneNumber = model.phoneNumber,
                    Email = model.email,
                    createddate=DateTime.Now
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

                    //await _userManager.AddToRoleAsync(user, SD.Role_Admin);
                    if (model.usertype.ToUpper().Trim() == "Buyer".ToUpper().Trim())
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Buyer);
                    }
                    else if (model.usertype.ToUpper().Trim() == "Seller".ToUpper().Trim())
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Seller);
                    }

                    string s = HttpContext.GetRequestedApiVersion().ToString();

                    return CreatedAtRoute("GetUserDetails", new { Version = HttpContext.GetRequestedApiVersion().ToString(), id = user.Id },user);
                 //      return Ok(user);
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
        /// <summary>
        /// Update User Details by User Id
        /// </summary>
        /// <param name="Id">User ID</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("[action]/{Id}", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser(string Id, [FromBody] ApplicationUserUpdateModelDtos model)
        {
            if (model == null || Id != model.Id)
            {
                return BadRequest(ModelState);
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
                obj.Email = model.email;
                obj.NormalizedEmail = model.email;
                obj.Ceritification = model.Ceritification;
                 
                // obj.CompanyRegCeritificate = model.CompanyRegCeritificate;
                obj.ExportToCountries = model.ExportToCountries;
                //obj.VisitingCardImg = model.VisitingCardImg;
                //obj.aadharBackImg = model.aadharBackImg;
                //  obj.aadharFrontImg = model.aadharFrontImg;
                obj.businessYear = model.businessYear;
                obj.cityid = model.cityid;
                obj.deviceid = model.deviceid;
                obj.latitude = model.latitude;
                // obj.logo = model.logo;
                obj.longitude = model.longitude;
                // obj.pancardImg = model.pancardImg;
                // obj.CancellerdChequeImg = model.CancellerdChequeImg;
                obj.productDealin = model.productDealin;
            }
            var rootFolder = Directory.GetCurrentDirectory();
            var path = rootFolder.ToString().Replace(".API", "");
            var folderPath1 = path + @"\wwwroot";
            //var imagePath = folderPath1;
            var test = folderPath1.Replace("\\", "/");
            
            SaveImageinFolder t = new SaveImageinFolder();
            obj.CompanyRegCeritificate = t.uploadImage((test + obj.CompanyRegCeritificate).Replace("/", "\\"), @"\uploads\user\companyRegCeritificate", model.CompanyRegCeritificate);
            obj.logo = t.uploadImage((test + obj.logo).Replace("/", "\\"), @"\uploads\user\logo", model.logo);
            obj.VisitingCardImg = t.uploadImage((test + obj.VisitingCardImg).Replace("/", "\\"), @"\uploads\user\visitingCardImg", model.VisitingCardImg);
            obj.aadharBackImg = t.uploadImage((test + obj.aadharBackImg).Replace("/", "\\"), @"\uploads\user\aadhar", model.aadharBackImg);
            obj.aadharFrontImg = t.uploadImage((test + obj.aadharFrontImg).Replace("/", "\\"), @"\uploads\user\aadhar", model.aadharFrontImg);
            obj.pancardImg = t.uploadImage((test + obj.pancardImg).Replace("/", "\\"), @"\uploads\user\pancard", model.pancardImg);
            obj.CancellerdChequeImg = t.uploadImage((test + obj.CancellerdChequeImg).Replace("/", "\\"), @"\uploads\user\cancelChequeImg", model.CancellerdChequeImg);

            var result = await _userManager.UpdateAsync(obj);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            return Ok(obj);
           // return NoContent();
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
     //   [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApplicationUserViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UserLogin([FromBody]LoginViewModel model)
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
                    var Dtosobj = new ApplicationUserViewModelDtos();
                    Dtosobj = _mapper.Map<ApplicationUserViewModelDtos>(obj);
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
    }
}

