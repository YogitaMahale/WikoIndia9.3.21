using Autofocus.API.Dtos;
using Autofocus.DataAccess.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Autofocus.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiversion}/SubCategory")]
    [ApiVersion("1.0")]
    [ApiController]

    //[ApiExplorerSettings(GroupName = "WikoIndiaAPISpesSubCategory")]
    public class SubCategoryController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SubCategoryController(IUnitofWork unitofWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Get All Main Category      
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubcategoryDtos))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategory()
        {
            var obj = _unitofWork.subcategory.GetAll().Where(x => x.isdeleted == false).ToList();
            if (obj == null || obj.Count == 0)
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

        /// <summary>
        /// Get Subcategory Details by MainCategory Id
        /// </summary>
        /// <param name="mainCategoryId">Maincategory Id</param>
        /// <returns></returns>

        [HttpGet("[action]/{mainCategoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryRegistrationViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetSubCategorybyMainCategoryId(int mainCategoryId)
        {
            var obj = _unitofWork.subcategory.GetAll().Where(x => x.isdeleted == false&&x.mainCategroyId==mainCategoryId).ToList();
            if (obj == null||obj.Count==0)
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
    }
}
