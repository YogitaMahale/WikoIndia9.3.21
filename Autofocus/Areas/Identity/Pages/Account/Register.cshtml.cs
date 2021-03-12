using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Mvc;
using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Autofocus.Areas.Identity.Pages.Account
{
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitofWork _unitofWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
              RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IUnitofWork unitofWork,
            IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

       


        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
 
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Display(Name = "Name")]
            public string name { get; set; }
            [Required]
            [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
            [Display(Name = "Mobile No")]
            public string phonenumber { get; set; }
            //[Display(Name = "Cancel Cheque Photo")]
            //public IFormFile CancellerdChequeImg { get; set; }


            //[Display(Name = "Ceritification")]
            //public string Ceritification { get; set; }

            //[Required]
            //[Display(Name = "Select Country")]
            //public int countryid { get; set; } = 0;

            //[Required]
            //[Display(Name = "Select State")]
            //public int stateid { get; set; } = 0;


            //[Required]
            //[Display(Name = "Select City")]
            //public int cityid { get; set; } = 0;

            //[Display(Name = "Company Reg Ceritificate")]
            //public IFormFile CompanyRegCeritificate { get; set; }
            //public string ExportToCountries { get; set; }


            //[Display(Name = "Visiting Card Image")]
            //public IFormFile VisitingCardImg { get; set; }


            //[Display(Name = "Aadhar Back Image")]
            //public IFormFile aadharBackImg { get; set; }


            //[Display(Name = "Aadhar Front Image")]
            //public IFormFile aadharFrontImg { get; set; }



            //[Display(Name = "Business Year")]
            //public string  businessYear { get; set; }
            //[Display(Name = "Product Deal in")]
            //public string productDealin { get; set; }
            

            //[Display(Name = "Pancard Image")]
            //public IFormFile pancardImg { get; set; }

            //[Display(Name = "Logo Image")]
            //public IFormFile logo { get; set; }



            [Display(Name = "Role")]
            public string Role { get; set; }

            public IEnumerable<SelectListItem> roleList { get; set; }

        }
        public int SelectedTag { get; set; }
       // public SelectList Countries { get; set; }

        
        public async Task OnGetAsync(string returnUrl = null)
        {
           // Countries = new SelectList(_unitofWork.country.GetAll().Where(x => x.isdeleted == false), "id", "countryname");
                      
            ReturnUrl = returnUrl;
            Input = new InputModel()
            {

                roleList = _roleManager.Roles.Where(x => x.Name != SD.Role_Admin).Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
       
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //       
                //CancellerdChequeImg, , CompanyRegCeritificate, , VisitingCardImg, aadharBackImg, 
                //aadharFrontImg, ,  ,  , deviceid, latitude, logo, longitude,  , pancardImg, 

                var user = new ApplicationUser
                {
                    name = Input.name,
                    UserName = Input.phonenumber,
                    Email = Input.Email,
                    PhoneNumber = Input.phonenumber,
                    Role = Input.Role,
                    //Ceritification = Input.Ceritification ,
                    //ExportToCountries = Input.ExportToCountries,
                    //businessYear = Input.businessYear,
                    //cityid = (int)Input.cityid ,
                    //createddate = DateTime.Now,
                   // productDealin = Input.productDealin

                };

                var webRootPath = _hostingEnvironment.WebRootPath;
                #region "img"
                //if (Input.logo != null && Input.logo.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/logo";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.logo.FileName);
                //    var extesion = Path.GetExtension(Input.logo.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);                    
                //    await Input.logo.CopyToAsync(fs);
                //    fs.Close();                     
                //    user.logo = '/' + uploadDir + '/' + fileName;
                //}
                //if (Input.aadharFrontImg != null && Input.aadharFrontImg.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/aadhar";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.aadharFrontImg.FileName);
                //    var extesion = Path.GetExtension(Input.aadharFrontImg.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.aadharFrontImg.CopyToAsync(fs);
                //    fs.Close();
                //    user.aadharFrontImg = '/' + uploadDir + '/' + fileName;
                //}
                //if (Input.aadharBackImg != null && Input.aadharBackImg.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/aadhar";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.aadharBackImg.FileName);
                //    var extesion = Path.GetExtension(Input.aadharBackImg.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.aadharBackImg.CopyToAsync(fs);
                //    fs.Close();
                //    user.aadharBackImg = '/' + uploadDir + '/' + fileName;
                //}
                //if(Input.CancellerdChequeImg != null && Input.CancellerdChequeImg.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/cancelChequeImg";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.CancellerdChequeImg.FileName);
                //    var extesion = Path.GetExtension(Input.CancellerdChequeImg.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.CancellerdChequeImg.CopyToAsync(fs);
                //    fs.Close();
                //    user.CancellerdChequeImg = '/' + uploadDir + '/' + fileName;
                //}
                //if (Input.CompanyRegCeritificate != null && Input.CompanyRegCeritificate.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/companyRegCeritificate";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.CompanyRegCeritificate.FileName);
                //    var extesion = Path.GetExtension(Input.CompanyRegCeritificate.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.CompanyRegCeritificate.CopyToAsync(fs);
                //    fs.Close();
                //    user.CompanyRegCeritificate = '/' + uploadDir + '/' + fileName;
                //}
                //if (Input.pancardImg != null && Input.pancardImg.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/pancard";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.pancardImg.FileName);
                //    var extesion = Path.GetExtension(Input.pancardImg.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.pancardImg.CopyToAsync(fs);
                //    fs.Close();
                //    user.pancardImg = '/' + uploadDir + '/' + fileName;
                //}
                //if (Input.VisitingCardImg != null && Input.VisitingCardImg.Length > 0)
                //{
                //    var uploadDir = @"uploads/user/visitingCardImg";
                //    var fileName = Path.GetFileNameWithoutExtension(Input.VisitingCardImg.FileName);
                //    var extesion = Path.GetExtension(Input.VisitingCardImg.FileName);

                //    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extesion;
                //    var path = Path.Combine(webRootPath, uploadDir, fileName);

                //    FileStream fs = new FileStream(path, FileMode.Create);
                //    await Input.VisitingCardImg.CopyToAsync(fs);
                //    fs.Close();
                //    user.VisitingCardImg = '/' + uploadDir + '/' + fileName;
                //}


                #endregion
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    TempData["userName"] = user.name;
                    _logger.LogInformation("User created a new account with password.");
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                    }

                    if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Buyer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Buyer));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Seller))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Seller));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.Role_Employee))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                    }
                    //  await _userManager.AddToRoleAsync(user, SD.Role_Admin);


                    if (user.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Buyer);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }





                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (user.Role == null)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "User", new { Area = "Admin" });
                        }

                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            Input = new InputModel()
            {

                roleList = _roleManager.Roles.Where(x => x.Name != SD.Role_Admin).Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                })
            };
            // If we got this far, something failed, redisplay form
            return Page();
        }
        
    }
}
