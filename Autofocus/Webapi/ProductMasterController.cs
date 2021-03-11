using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using Autofocus.Utility;
using AutoMapper;
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
    [Route("api/productmaster")]
    public class ProductMasterController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductMasterController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("GetProductMaster")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductMasterDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProductMaster()
        {
            var obj = _unitofWork.productMaster.SelectAllProductMaster();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<ProductMasterDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<ProductMasterDtos>(item));
            }

            return Ok(Dtosobj);
        }


       


        [HttpGet]
        [Route("GetProductMasterbyid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProductMasterbyid(int id)
        {
            var obj = _unitofWork.productMaster.GetAll().Where(x => x.id == id).FirstOrDefault();
            if (obj == null)
            {
                string finalResult = "{\"success\" : 0, \"message\" : \"No Data \", \"data\" : \"\"}";                
                return Ok(finalResult);
            }
            else
            {
                var Dtosobj = new ProductMasterDtos();
                Dtosobj = _mapper.Map<ProductMasterDtos>(obj);
                string output = JsonConvert.SerializeObject(Dtosobj);
                string finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";
                return Ok(finalResult);
            }
           
        }


        [HttpPost]
        [Route("SaveProductMaster")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductMasterUpsertDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SaveProductMaster([FromBody] ProductMasterUpsertDtos model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if (_unitofWork.productMaster.ProductMasterExists(model.name))
            {
                ModelState.AddModelError("", "Product is  Exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Obj = _mapper.Map<ProductMaster>(model);
            if (model.img == null || model.img.Trim() == "")
            {
                Obj.img = "";
            }
            else
            {
                SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
                string iamgename = objsaveImageinFolder.uploadImage("", "\\uploads\\ProductMaster", model.img);
                Obj.img = iamgename;

            }

            Obj.name = model.name;
            Obj.description = model.description;
            Obj.isdeleted = false;
            Obj.isactive = false;

            _unitofWork.productMaster.Add(Obj);
            // bool res = _unitofWork.Save();
            // return Ok(mainCategoryObj);       


            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var obj = _mapper.Map<ProductMasterUpsertDtos>(Obj);

            return Ok(obj);
            // return CreatedAtRoute("GetMainCategory", new { maincategoryId = mainCategoryObj.id }, mainCategoryObj);
            // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        [HttpPatch]
        [Route("UpdateProductMaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // [HttpPatch("{maincategoryId:int}", Name = "UpdateMainCategory")]
        public ActionResult UpdateProductMaster([FromBody] ProductMasterUpsertDtos model)
        {
            if (model == null)// || maincategoryId != mainCategoryDtos.id
            {
                return BadRequest(ModelState);
            }

            // var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryDtos);
            var obj = _unitofWork.productMaster.Get(model.id);
            obj.name = model.name;
            obj.subCategroyId = model.subCategroyId;
            obj.description = model.description;
            if (model.img == null || model.img.Trim() == "")
            {
                obj.img = "";
            }
            else
            {
                SaveImageinFolder objsaveImageinFolder = new SaveImageinFolder();
                string iamgename = objsaveImageinFolder.uploadImage(obj.img, "\\uploads\\ProductMaster", model.img);
                obj.img = iamgename;

            }
            _unitofWork.productMaster.Update(obj);
            //_unitofWork.Save();


            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record{model.name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteProductMaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteProductMaster(int id)
        {
            var obj = _unitofWork.productMaster.Get(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.isdeleted = true;

            _unitofWork.productMaster.Update(obj);
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
