using Autofocus.DataAccess.Repository.IRepository;
using Autofocus.Models;
using Autofocus.Models.Dtos;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.Webapi
{
    [Route("api/product")]

    public class ProductController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        // private readonly ISP_CALL _spcall;

        public ProductController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            //  _spcall = spcall;, ISP_CALL spcall
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult saveProduct([FromBody] ProductViewModelDtos model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }
            //var checkduplicate = _unitofWork.product.GetAll().Where(x => x.userId == model.userId && x.productmasterId == model.productmasterId && x.isdeleted == false).FirstOrDefault();
            //if (checkduplicate != null)
            //{
            //    ModelState.AddModelError("", "Product Exists!");
            //    return StatusCode(404, ModelState);
            //}
            String traderId = null;
            Random random = new Random();
            for (int i = 0; i < 1; i++)
            {
                int n = random.Next(0, 999999);
                traderId += n.ToString("D4") + "";
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //DateTime dateFromString =model.rateTill;
            DateTime dt3 = new DateTime(2015, 12, 31, 5, 10, 20);
            var objProduct = _mapper.Map<Product>(model);
            objProduct.tradeId = traderId;
            //  objProduct.isdeleted = false; 
            _unitofWork.product.Add(objProduct);
            if (!_unitofWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong saving record");
                return StatusCode(500, ModelState);
            }

            IEnumerable<ProductDetailsViewModelDtos> objproductDetailsViewModelDtos = model.productDetailsViewModelDtos;
            foreach (var item in objproductDetailsViewModelDtos)
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
            // return Ok(model);


            string output = JsonConvert.SerializeObject(model);
            string finalResult = "{\"success\" : 1, \"message\" : \" Record Saved Successfully\", \"data\" :" + output + "}";
            return Ok(finalResult);

        }


        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProuductbyUserId(string userId)
        {
            //ProductViewModelDtos
            //var obj = _unitofWork.product.GetAll(includeProperties: "ApplicationUser,ProductMaster,productSize,CityRegistration,packingType").Where(x => x.isdeleted == false).ToList();
            // if (obj == null)
            // {
            //     return NotFound();
            // }
            var paramter = new DynamicParameters();
            //  paramter.Add("@Latitude", Latitude);
            paramter.Add("@userId", userId);
            //  paramter.Add("@distance", 5);
            //storedetailsListViewmodel
            var storeList = _unitofWork.sp_call.List<productListbyUserID>("getProductListbyUserId", paramter);
            if (storeList != null)
            {
                return Ok(storeList);

            }
            else
            {
                return NotFound();
            }


            //   return Ok(obj);
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProductbyProductId(int productId)
        {
            var obj = _unitofWork.product.GetAll(includeProperties: "ApplicationUser,ProductMaster,productSize,CityRegistration,packingType").Where(x => x.isdeleted == false && x.id == productId).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new ProductbyIdViewModelDtos();
            Dtosobj = _mapper.Map<ProductbyIdViewModelDtos>(obj);
            IEnumerable<ProductDetails> pdetails = _unitofWork.productDetails.GetAll(includeProperties: "packingSize").Where(x => x.pid == productId && x.isdeleted == false).ToList();
            // Dtosobj.productDetailsViewModelDtos = pdetails;
            ////var Dtosobj = new List<MainCategoryDtos>();
            //foreach (var item in pdetails)
            //{
            //    Dtosobj.productDetailsViewModelDtos.Add(_mapper.Map<ProductDetailsbyidViewModelDtos>(item));
            //}
            // obj.ApplicationUser = obj.ApplicationUser.wher.Include(p => p.Category);

            return Ok(obj);
        }
    }
}
