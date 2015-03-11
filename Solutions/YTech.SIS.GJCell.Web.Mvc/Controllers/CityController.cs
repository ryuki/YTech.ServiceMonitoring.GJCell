using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Web.Mvc;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels;
using System.Linq;
using YTech.SIS.GJCell.Domain;
using System;
using System.Web;
using YTech.SIS.GJCell.Enums;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class CityController : Controller
    {
        private readonly IMCityTasks _cityTasks;
        public CityController(IMCityTasks cityTasks)
        {
            this._cityTasks = cityTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        public ActionResult Citys_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCitys().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Citys_Create([DataSourceRequest] DataSourceRequest request, CityViewModel cityVM)
        {
            if (cityVM != null && ModelState.IsValid)
            {
                MCity city = new MCity();
                city.SetAssignedIdTo(cityVM.CityID);

                ConvertToCity(cityVM, city);

                city.CreatedDate = DateTime.Now;
                city.CreatedBy = User.Identity.Name;
                city.DataStatus = "New";

                _cityTasks.Insert(city);
            }

            return Json(new[] { cityVM }.ToDataSourceResult(request, ModelState));
        }

        private static void ConvertToCity(CityViewModel cityVM, MCity city)
        {
            city.CityName = cityVM.CityName;
            city.CityStatus = cityVM.CityStatus;
            city.CityDesc = cityVM.CityDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Citys_Update([DataSourceRequest] DataSourceRequest request, CityViewModel cityVM)
        {
            if (cityVM != null && ModelState.IsValid)
            {
                var city = _cityTasks.One(cityVM.CityID);
                if (city != null)
                {
                    ConvertToCity(cityVM, city);

                    city.ModifiedDate = DateTime.Now;
                    city.ModifiedBy = User.Identity.Name;
                    city.DataStatus = "Updated";

                    _cityTasks.Update(city);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Citys_Destroy([DataSourceRequest] DataSourceRequest request, CityViewModel cityVM)
        {
            if (cityVM != null)
            {
                var city = _cityTasks.One(cityVM.CityID);
                if (city != null)
                {
                    //city.ModifiedDate = DateTime.Now;
                    //city.ModifiedBy = User.Identity.Name;
                    //city.DataStatus = "Deleted";
                    _cityTasks.Delete(city);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<CityViewModel> GetCitys()
        {
            var cityomers = this._cityTasks.GetListNotDeleted();

            return from city in cityomers
                   select new CityViewModel
        {
            CityID = city.Id,
            CityName = city.CityName,
            CityDesc = city.CityDesc,
            CityStatus = city.CityStatus
        };

        }

        public JsonResult PopulateCitys()
        {
            IEnumerable<CityViewModel> citys = from city in _cityTasks.GetListNotDeleted()
                                                       select new CityViewModel
                                                       {
                                                           CityID = city.Id,
                                                           CityName = city.CityName
                                                       };
            ViewData["citys"] = citys;
            return Json(citys, JsonRequestBehavior.AllowGet);
        }
    }
}
