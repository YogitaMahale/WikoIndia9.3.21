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

namespace Autofocus.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiversion}/MainCategory")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "WikoIndiaAPISpesMainCategory")]
    public class MainCategoryV2Controller : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MainCategoryV2Controller(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
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
            var obj = _unitofWork.mainCategory.GetAll().Where(x => x.isdeleted == false&&x.id==3).ToList();
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateMainCategory([FromBody] MainCategoryInsertDtos mainCategoryInsertDtos)
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
                var rootFolder = Directory.GetCurrentDirectory();
                var path = rootFolder.ToString().Replace(".API", "");  
                string fileName = Guid.NewGuid().ToString();              
                fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + ".jpg";
                var folderPath = path + @"\wwwroot\uploads\MainCategory";
                string s = Path.Combine(folderPath, fileName);
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), Convert.FromBase64String(mainCategoryInsertDtos.img));
                mainCategoryObj.img = "/uploads/MainCategory/" + fileName;

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
           // return CreatedAtRoute("GetMainCategory", new {   maincategoryId = mainCategoryObj.id }, mainCategoryObj);
            return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        [HttpPatch("{maincategoryId:int}", Name = "UpdateMainCategory")]
        public ActionResult UpdateMainCategory(int maincategoryId,[FromBody] MainCategoryDtos mainCategoryDtos)
        {
            if (mainCategoryDtos == null||maincategoryId != mainCategoryDtos.id)
            {
                return BadRequest(ModelState);
            }
            
            var mainCategoryObj = _mapper.Map<MainCategory>(mainCategoryDtos);
            

            _unitofWork.mainCategory.Update(mainCategoryObj);
            //_unitofWork.Save();
           

            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record{mainCategoryDtos.name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
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
