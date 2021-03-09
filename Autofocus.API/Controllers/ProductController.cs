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
    [Route("api/v{version:apiversion}/Product")]
    [ApiVersion("1.0")]
    
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Add New Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult saveProduct([FromBody] ProductViewModelDtos model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            var checkduplicate = _unitofWork.product.GetAll().Where(x => x.userId == model.userId && x.subCategoryId == model.subCategoryId && x.isdeleted == false).FirstOrDefault();
            if (checkduplicate!=null)
            {
                ModelState.AddModelError("", "Product Exists!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //DateTime dateFromString =model.rateTill;
            DateTime dt3 = new DateTime(2015, 12, 31, 5, 10, 20);
            var objProduct = _mapper.Map<Product>(model);
            //objProduct.rateTill = dt3;
          //  objProduct.isdeleted = false; 
            _unitofWork.product.Add(objProduct);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }

           

            IEnumerable<ProductDetailsViewModelDtos> objproductDetailsViewModelDtos = model.productDetailsViewModelDtos;
            foreach(var item in objproductDetailsViewModelDtos)
            {
                var objProductdetails = _mapper.Map<ProductDetails>(item);
                objProductdetails.pid = objProduct.id;
                _unitofWork.productDetails.Add(objProductdetails);
            }
         

            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            return Ok(model);
            // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

        /// <summary>
        /// update Product
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModelDtos))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult updateProduct(int pid, [FromBody] ProductViewModelDtos model)
        {

            if (model == null)
            {
                return BadRequest(ModelState);
            }
            if (pid == null || pid != model.id)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objProduct = _mapper.Map<Product>(model);
            //  objProduct.isdeleted = false; 
            _unitofWork.product.Update(objProduct);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            IEnumerable<ProductDetailsViewModelDtos> objproductDetailsViewModelDtos = model.productDetailsViewModelDtos;
            foreach (var item in objproductDetailsViewModelDtos)
            {
                //var objProductdetails = _mapper.Map<ProductDetails>(item);
                var obj = _unitofWork.productDetails.GetAll().Where(x => x.id == item.id).FirstOrDefault();
                obj.bagId = item.bagId;
                obj.bagPrice = item.bagPrice;
                obj.labourCost = item.labourCost;
                //bagId, bagPrice, labourCost
                 
                    _unitofWork.productDetails.Update(obj);
            }


            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }
            return Ok(model);
            // return CreatedAtRoute("GetMainCategory", new { Version = HttpContext.GetRequestedApiVersion().ToString(), maincategoryId = mainCategoryObj.id }, mainCategoryObj);
        }

    }
}
