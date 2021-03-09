using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using Autofocus.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Webapi
{
    [Route("Maincategory")]
    //[ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
       
        public CategoryController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("GetMainCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMainCategory()
        {
            var obj = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
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
        [HttpGet]
        [Route("GetMainCategorybyid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetMainCategorybyid(int maincategoryId)
        {
            var obj = _unitofWork.mainCategory.GetAll().Where(x => x.id == maincategoryId).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new MainCategoryDtos();
            Dtosobj = _mapper.Map<MainCategoryDtos>(obj);

            return Ok(Dtosobj);
        }


        [HttpPost]
        [Route("CreateMainCategory")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateMainCategory([FromBody] MainCategoryDtos mainCategoryInsertDtos)
        {
            if (mainCategoryInsertDtos == null)
            {
                return BadRequest(ModelState);
            }
            if (_unitofWork.mainCategory.MainCategoryExists(mainCategoryInsertDtos.name))
            {
                ModelState.AddModelError("", "Main Category Exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryInsertDtos);
            if (mainCategoryInsertDtos.img == null || mainCategoryInsertDtos.img.Trim() == "")
            {
                mainCategoryObj.img = "";
            }
            else
            {
                   SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
              string iamgename = objsaveImageinFolder.uploadImage("", "\\uploads\\MainCategory", mainCategoryInsertDtos.img);
                mainCategoryObj.img = iamgename;
              
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
             var obj=_mapper.Map<MainCategoryDtos>(mainCategoryObj);

            return Ok(obj);
            // return CreatedAtRoute("GetMainCategory", new { maincategoryId = mainCategoryObj.id }, mainCategoryObj);
            // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        [HttpPatch]
        [Route("UpdateMainCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [HttpPatch("{maincategoryId:int}", Name = "UpdateMainCategory")]
        public ActionResult UpdateMainCategory([FromBody] MainCategoryDtos mainCategoryDtos)
        {
            if (mainCategoryDtos == null)// || maincategoryId != mainCategoryDtos.id
            {
                return BadRequest(ModelState);
            }

            // var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryDtos);
            var mainCategoryObj = _unitofWork.mainCategory.Get(mainCategoryDtos.id);
            mainCategoryObj.name = mainCategoryDtos.name;
            if (mainCategoryDtos.img == null || mainCategoryDtos.img.Trim() == "")
            {
                // mainCategoryObj.img = "";
            }
            else
            {
                var path = Directory.GetCurrentDirectory();
                var folderPath1 = path + @"\wwwroot";

                if (mainCategoryObj.img != null)
                {
                    var imagePath = folderPath1;
                    var test = folderPath1.Replace("\\", "/");
                    var test2 = test + mainCategoryObj.img;
                    var imgdelete = test2.Replace("/", "\\");
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

        [HttpDelete]
        [Route("DeleteMainCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMainCategory(int maincategoryId)
        {
            var obj = _unitofWork.mainCategory.Get(maincategoryId);
            if (obj == null)
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
