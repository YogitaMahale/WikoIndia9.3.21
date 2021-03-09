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
using Autofocus.Utility;

namespace Autofocus.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiversion}/MainCategory")]
    [ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "WikoIndiaAPISpesMainCategory")]
    public class MainCategoryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MainCategoryController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Get All Main Category      
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMainCategory()
        {
            var obj = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).ToList();
            if(obj==null)
            {
                return NotFound();
            }
            var Dtosobj = new List<MainCategoryDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<MainCategoryDtos>(item));
            }

            return Ok(Dtosobj);
        }

        /// <summary>
        /// Get Main category by id        
        /// </summary>
        /// <param name="maincategoryId"> Maincategory id</param>
        /// <returns></returns>
        [HttpGet("{maincategoryId:int}",Name = "GetMainCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMainCategory(int maincategoryId)
        {
            var obj = _unitofWork.mainCategory.Get(maincategoryId);
           if(obj==null)
            {
                return NotFound();
            }
            var Dtosobj = new MainCategoryDtos();
            Dtosobj=_mapper.Map<MainCategoryDtos>(obj);

            return Ok(Dtosobj);
        }

        /// <summary>
        /// Get Maincategory Details by Maincategory Id
        /// </summary>
        /// <param name="maincategoryId"> Maincategory Id</param>
        /// <returns></returns>
        [HttpGet("[action]/{maincategoryId:int}" )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMainCategorybyid(int maincategoryId)
        {
            var obj = _unitofWork.mainCategory.GetAll().Where(x => x.id == maincategoryId).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            //var Dtosobj = new MainCategoryDtos();
            //Dtosobj = _mapper.Map<MainCategoryDtos>(obj);

            return Ok(obj);
        }

        /// <summary>
        /// Add new Main Category
        /// </summary>
        /// <param name="mainCategoryInsertDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateMainCategory([FromBody] MainCategoryDtos mainCategoryInsertDtos)
        {
            if(mainCategoryInsertDtos == null)
            {
                return BadRequest(ModelState);
            }
            if(_unitofWork.mainCategory.MainCategoryExists(mainCategoryInsertDtos.name))
            {
                ModelState.AddModelError("", "Main Category Exists!");
                return StatusCode(404, ModelState);
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryInsertDtos);
            if (mainCategoryInsertDtos.img == null || mainCategoryInsertDtos.img.Trim() == "")
            {
                mainCategoryObj.img  = "";
            }
            else
            {
                // var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);
                
                string fileName = Guid.NewGuid().ToString()+ ".jpg";
                var folderPath = SD.ImageFolderPath.Replace(@"/", @"\") + @"\wwwroot\uploads\MainCategory";
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(mainCategoryInsertDtos.img));
                mainCategoryObj.img = "/uploads/MainCategory/" + fileName;

               // var rootFolder = _hostingEnvironment.ContentRootPath;                
               //var path = rootFolder.ToString().Replace(".API", "");  
               // string fileName = Guid.NewGuid().ToString();              
               // fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ".jpg";
               // // var folderPath = path + @"\wwwroot\uploads\MainCategory";
               // var folderPath = SD.ImageFolderPath.Replace(@"/",@"\") + @"\wwwroot\uploads\MainCategory";
               // string s = Path.Combine(folderPath, fileName);
               // //if (!System.IO.Directory.Exists(folderPath))
               // //{
               // //    System.IO.Directory.CreateDirectory(folderPath);
               // //}
               // System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(mainCategoryInsertDtos.img));
               // mainCategoryObj.img = "/uploads/MainCategory/" + fileName;

            }


            mainCategoryObj.isdeleted = false;
            mainCategoryObj.isactive = false;

            _unitofWork.mainCategory.Add(mainCategoryObj);
           // bool res = _unitofWork.Save();
            // return Ok(mainCategoryObj);       


            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetMainCategory", new {   maincategoryId = mainCategoryObj.id }, mainCategoryObj);
           // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        /// <summary>
        /// Update Main Category Details
        /// </summary>
        /// <param name="maincategoryId"></param>
        /// <param name="mainCategoryDtos"></param>
        /// <returns></returns>
        [HttpPatch("{maincategoryId:int}", Name = "UpdateMainCategory")]
        public ActionResult UpdateMainCategory(int maincategoryId,[FromBody] MainCategoryDtos mainCategoryDtos)
        {
            if (mainCategoryDtos == null||maincategoryId != mainCategoryDtos.id)
            {
                return BadRequest(ModelState);
            }

            // var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryDtos);
            var mainCategoryObj = _unitofWork.mainCategory.Get(maincategoryId);
            mainCategoryObj.name = mainCategoryDtos.name;
            if (mainCategoryDtos.img == null || mainCategoryDtos.img.Trim() == "")
            {
               // mainCategoryObj.img = "";
            }
            else
            {
                // var obj = _unitofWork.mainCategory.Get(mainCategoryDtos.id);
               // var path = _hostingEnvironment.ContentRootPath;
                var rootFolder = Directory.GetCurrentDirectory();
               var path = rootFolder.ToString().Replace(".API", "");
                var folderPath1 = path + @"\wwwroot";

                if (mainCategoryObj.img != null)
                {
                    var imagePath = folderPath1;
                    var test = folderPath1.Replace("\\", "/");
                    var test2 = test + mainCategoryObj.img;
                    var imgdelete=test2.Replace("/","\\" );
                    //webRootPath + obj.img.ToString().Replace("/", "\\");
                    if (System.IO.File.Exists(imgdelete))
                    {
                        System.IO.File.Delete(imgdelete);
                    }
                }


                
                string fileName = Guid.NewGuid().ToString();
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ".jpg";
                var folderPath = path + @"\wwwroot\uploads\MainCategory";
                string s = Path.Combine(folderPath, fileName);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(mainCategoryDtos.img));
                mainCategoryObj.img = "/uploads/MainCategory/" + fileName;

            }
            _unitofWork.mainCategory.Update(mainCategoryObj);
            //_unitofWork.Save();
           

            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record{mainCategoryDtos.name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        
        /// <summary>
        /// Delete MainCategory
        /// </summary>
        /// <param name="maincategoryId">MainCategory Id</param>
        /// <returns></returns>
        [HttpDelete("{maincategoryId:int}", Name = "DeleteMainCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
    
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMainCategory(int maincategoryId)
        {
            var obj = _unitofWork.mainCategory.Get(maincategoryId);
            if(obj==null)
            {
                return NotFound();
            }
            obj.isdeleted = true;        

            _unitofWork.mainCategory.Update(obj);
           // _unitofWork.Save();
           

            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
