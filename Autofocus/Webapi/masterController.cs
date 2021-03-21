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
    [Route("master")]
     
    public class masterController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public masterController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCountry()
        {
            var obj = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<CountryRegistrationViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<CountryRegistrationViewModel>(item));
            }

            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("GetStatebyCountryId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStatebyCountryId(int countryid)
        {
            var obj = _unitofWork.state.GetAll().Where(x => x.isdeleted == false && x.countryid == countryid).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<StateRegistrationViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<StateRegistrationViewModel>(item));
            }

            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("GetCitybyStateId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MainCategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetCitybyStateId(int stateId)
        {
            var obj = _unitofWork.city.GetAll().Where(x => x.isdeleted == false && x.stateid == stateId).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<CityRegistrationViewModelDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<CityRegistrationViewModelDtos>(item));
            }

            return Ok(Dtosobj);
        }


        [HttpGet]
        [Route("GetGrade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GradeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetGrade()
        {
            var obj = _unitofWork.grade.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<GradeViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<GradeViewModel>(item));
            }

            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("GetPackingSize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GradeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetPackingSize()
        {
            var obj = _unitofWork.packingSize.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<packingSizeViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<packingSizeViewModel>(item));
            }

            return Ok(Dtosobj);
        }
        [HttpGet]
        [Route("GetPackingType")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(packingTypeViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetPackingType()
        {
            var obj = _unitofWork.packingType.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<packingTypeViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<packingTypeViewModel>(item));
            }

            return Ok(Dtosobj);
        }
        [HttpGet]
        [Route("GetSliderImages")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sliderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSliderImages()
        {
            var obj = _unitofWork.slider.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<sliderViewModel>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<sliderViewModel>(item));
            }

            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("GetProductSize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sliderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetProductSize()
        {
            var obj = _unitofWork.productSize.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<productSizeViewModelDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<productSizeViewModelDtos>(item));
            }

            return Ok(Dtosobj);
        }

        [HttpGet]
        [Route("getProductMasterbySubcategoryID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sliderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getProductMasterbySubcategoryID(int SubcategoryID)
        {
            var obj = _unitofWork.productMaster.GetAll().Where(x => x.subCategroyId == SubcategoryID && x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<ProductMasterDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<ProductMasterDtos>(item));
            }
            if (obj == null)
            {
                string finalResult = "{\"success\" : 0, \"message\" : \"No Data \", \"data\" : \"\"}";
                return Ok(finalResult);
            }
            else
            {
           
                string output = JsonConvert.SerializeObject(Dtosobj);
                string finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";
                return Ok(finalResult);
            }
           // return Ok(Dtosobj);
        }
        [HttpGet]
        [Route("getProductMasterbySubcategoryIDNew")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(sliderViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getProductMasterbySubcategoryIDNew(int SubcategoryID)
        {
            var obj = _unitofWork.productMaster.GetAll().Where(x => x.subCategroyId == SubcategoryID && x.isdeleted == false).ToList();
            if (obj == null)
            {
                return NotFound();
            }
            var Dtosobj = new List<ProductMasterDtos>();
            foreach (var item in obj)
            {
                Dtosobj.Add(_mapper.Map<ProductMasterDtos>(item));
            }
            if (obj == null||obj.Count==0)
            {
                string myJson = "{'message': 'No Data'}";
                return Ok(myJson);
                //string finalResult = "{\"success\" : 0, \"message\" : \"No Data \", \"data\" : \"\"}";
                //return Ok(finalResult);
            }
            else
            {

                string output = JsonConvert.SerializeObject(Dtosobj);
               // string finalResult = "{\"success\" : 1, \"message\" : \" Category All Data\", \"data\" :" + output + "}";
                return Ok(output);
            }
            // return Ok(Dtosobj);
        }
    }
}
