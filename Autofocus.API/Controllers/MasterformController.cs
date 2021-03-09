using Autofocus.API.Dtos;
using Autofocus.DataAccess.Data;
using Autofocus.DataAccess.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autofocus.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiversion}/Masterform")]
    [ApiVersion("1.0")]
    public class MasterformController : ControllerBase
    {
       // private readonly ApplicationDbContext _db;
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;    
        public MasterformController(IUnitofWork unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;            
        }
        /// <summary>
        /// Get All Country      
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
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

        /// <summary>
        /// Get All State by Country Id
        /// </summary>
        /// <param name="countryid">Country Id</param>
        /// <returns></returns>
        [HttpGet("[action]/{countryid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetStatebyCountryId(int countryid)
        {
            var obj = _unitofWork.state.GetAll().Where(x => x.isdeleted == false&&x.countryid==countryid).ToList();
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

        /// <summary>
        /// Get All City by State Id
        /// </summary>
        /// <param name="stateId">State Id</param>
        /// <returns></returns>
        [HttpGet("[action]/{stateId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CityRegistrationViewModelDtos))]
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


        /// <summary>
        /// Get All Grade
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
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

        /// <summary>
        /// Get All Packing Size
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
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
        /// <summary>
        /// Get All Packing Bag / Type
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
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

        /// <summary>
        /// Get Banner Images
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
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
    }
}
