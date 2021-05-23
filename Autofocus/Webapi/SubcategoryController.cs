using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using Autofocus.Utility;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Webapi
{
    [Route("subcategory")]
    public class SubcategoryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SubcategoryController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("GetSubCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategory()
        {
            var obj = _unitofWork.subcategory.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<SubcategoryDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<SubcategoryDtos>(item));
            }

            return Ok(Dtosobj);
        }
        [HttpGet]
        [Route("GetSubCategorybyid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategorybyid(int id)
        {
            var obj = _unitofWork.subcategory.GetAll().Where(x => x.id == id&&x.isdeleted==false).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new SubcategoryDtos();
            Dtosobj = _mapper.Map<SubcategoryDtos>(obj);

            return Ok(Dtosobj);
        }


        [HttpPost]
        [Route("CreateSubCategory")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubcategoryUpsertDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateSubCategory([FromBody] SubcategoryUpsertDtos model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if (_unitofWork.mainCategory.MainCategoryExists(model.name))
            {
                ModelState.AddModelError("", "Sub Category Exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mainCategoryObj = _mapper.Map<Subcategory>(model);
            if (model.img == null || model.img.Trim() == "")
            {
                mainCategoryObj.img = "";
            }
            else
            {
                SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
                string iamgename = objsaveImageinFolder.uploadImage("", "\\uploads\\Subcategory", model.img);
                mainCategoryObj.img = iamgename;

            }

            mainCategoryObj.mainCategroyId = model.mainCategroyId;
            mainCategoryObj.isdeleted = false;
            mainCategoryObj.isactive = false;

            _unitofWork.subcategory.Add(mainCategoryObj);
            // bool res = _unitofWork.Save();
            // return Ok(mainCategoryObj);       


            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var obj = _mapper.Map<SubcategoryUpsertDtos>(mainCategoryObj);

            return Ok(obj);
            // return CreatedAtRoute("GetMainCategory", new { maincategoryId = mainCategoryObj.id }, mainCategoryObj);
            // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        [HttpPatch]
        [Route("UpdateSubCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [HttpPatch("{maincategoryId:int}", Name = "UpdateMainCategory")]
        public ActionResult UpdateSubCategory([FromBody] SubcategoryUpsertDtos model)
        {
            if (model == null)// || maincategoryId != mainCategoryDtos.id
            {
                return BadRequest(ModelState);
            }

            
            var  CategoryObj = _unitofWork.subcategory.Get(model.id);
            CategoryObj.name = model.name;
            CategoryObj.mainCategroyId = model.mainCategroyId;
            if (model.img == null || model.img.Trim() == "")
            {
                // mainCategoryObj.img = "";
            }
            else
            {
                var path = Directory.GetCurrentDirectory();
                var folderPath1 = path + @"\wwwroot";

                if (CategoryObj.img != null)
                {
                    var imagePath = folderPath1;
                    var test = folderPath1.Replace("\\", "/");
                    var test2 = test + CategoryObj.img;
                    var imgdelete = test2.Replace("/", "\\");
                    //webRootPath + obj.img.ToString().Replace("/", "\\");
                    if (System.IO.File.Exists(imgdelete))
                    {
                        System.IO.File.Delete(imgdelete);
                    }
                }

                string fileName = Guid.NewGuid().ToString();
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ".jpg";
                var folderPath = path + @"\wwwroot\uploads\Subcategory";
                string s = Path.Combine(folderPath, fileName);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(model.img));
                CategoryObj.img = "/uploads/Subcategory/" + fileName;

            }

            _unitofWork.subcategory.Update(CategoryObj);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record{model.name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpGet]
        [Route("GetSubCategorybyMaincategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategorybyMaincategoryId(int maincategoryId)
        {
            var obj = _unitofWork.subcategory.GetAll().Where(x => x.mainCategroyId == maincategoryId && x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
             
            var Dtosobj = new List<SubcategoryDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<SubcategoryDtos>(item));
            }
            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("GetSubCategorybyCityId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategorybyCityId(int cityId)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@cityId", cityId);
                var obj = _unitofWork.sp_call.List<SubcategoryDtos>("GetSubCategorybyCityId", parameter);
                if (obj == null)
                {
                    string finalResult = "{\"success\" : 0, \"message\" : \"No Data \", \"data\" : \"\"}";
                    return Ok(finalResult);
                }
                else
                {
                     
                    string output = JsonConvert.SerializeObject(obj);
                    string finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";
                    return Ok(finalResult);
                }
            }
            catch (Exception obj)
            {
                string myJson = "{\"Message\": " + "\"Bad Request\"" + "}";
                return BadRequest(myJson);

            }
        }

        //[HttpDelete]
        //[Route("DeleteMainCategory")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult DeleteMainCategory(int maincategoryId)
        //{
        //    var obj = _unitofWork.mainCategory.Get(maincategoryId);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    obj.isdeleted = true;

        //    _unitofWork.mainCategory.Update(obj);
        //    // _unitofWork.Save();


        //    if (!_unitofWork.Save())
        //    {
        //        ModelState.AddModelError("", $"Something went wrong saving record");
        //        return StatusCode(500, ModelState);
        //    }
        //    return NoContent();
        //}


        [HttpGet]
        [Route("GetSubCategorybyUserId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategorybyUserId(string  userId)
        {
            try
            {
                var parameter = new DynamicParameters();
                parameter.Add("@userId", userId);
                var obj = _unitofWork.sp_call.List<SubcategoryDtos>("GetSubCategorybyUserId", parameter);
                if (obj == null)
                {
                    string finalResult = "{\"success\" : 0, \"message\" : \"No Data \", \"data\" : \"\"}";
                    return Ok(finalResult);
                }
                else
                {

                    string output = JsonConvert.SerializeObject(obj);
                    string finalResult = "{\"success\" : 1, \"message\" : \" Category  Data\", \"data\" :" + output + "}";
                    return Ok(finalResult);
                }
            }
            catch (Exception obj)
            {
                string myJson = "{\"Message\": " + "\"Bad Request\"" + "}";
                return BadRequest(myJson);

            }
        }


    }
}
