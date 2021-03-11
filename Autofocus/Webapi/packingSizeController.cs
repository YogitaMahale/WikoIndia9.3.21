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
    [Route("api/bagsize")]
    
    public class packingSizeController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public packingSizeController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("InsertBagSize")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult InsertBagSize([FromBody] packingSizeViewModel model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if (_unitofWork.packingSize.PackingSizeExists(model.name))
            {
                ModelState.AddModelError("", "This Bag Size already Exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Obj = _mapper.Map<packingSize>(model);
            Obj.isdeleted = false;
            Obj.isactive = false;
            _unitofWork.packingSize.Add(Obj);
         
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            var obj1 = _mapper.Map<packingSizeViewModel>(Obj);

            string output = JsonConvert.SerializeObject(obj1);
            string finalResult = "{\"success\" : 1, \"message\" : \"Record Saved Successfully\", \"data\" :" + output + "}";

            return Ok(finalResult);
            
        }
    }
}
