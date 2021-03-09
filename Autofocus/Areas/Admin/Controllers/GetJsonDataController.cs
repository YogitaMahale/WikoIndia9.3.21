using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofocus.Models;
using Microsoft.AspNetCore.Hosting;
using Autofocus.DataAccess.Repository.IRepository;

namespace Autofocus.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GetJsonDataController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUnitofWork _unitofWork;

        public GetJsonDataController(IUnitofWork unitofWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofWork = unitofWork;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult getCountry()
        {
            IList<CountryRegistration> obj = _unitofWork.country.GetAll().Where(x => x.isdeleted == false).ToList();
            //obj.Insert(0, new CountryRegistration { id = 0, countryname = "", isactive = false, isdeleted = false });
            return Json(new SelectList(obj, "id", "countryname"));
        }
        public JsonResult getCitybyStateid(int stateid)
        {

            IList<CityRegistration> obj = _unitofWork.city.GetAll().Where(x => x.stateid == stateid).ToList();
          //  obj.Insert(0, new CityRegistration { id = 0, cityName = "select", isactive = false, isdeleted = false });
            return Json(new SelectList(obj, "id", "cityName"));
        }

    }
}
