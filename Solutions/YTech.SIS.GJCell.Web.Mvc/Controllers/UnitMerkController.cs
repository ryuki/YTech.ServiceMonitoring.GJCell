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
    public partial class UnitMerkController : Controller
    {
        private readonly IMMerkTasks _merkTasks;
        public UnitMerkController(IMMerkTasks merkTasks)
        {
            this._merkTasks = merkTasks;
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

        public ActionResult Merks_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetMerks().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Merks_Create([DataSourceRequest] DataSourceRequest request, UnitMerkViewModel merkVM)
        {
            if (merkVM != null && ModelState.IsValid)
            {
                MMerk merk = new MMerk();
                merk.SetAssignedIdTo(merkVM.MerkID);

                ConvertToMerk(merkVM, merk);

                merk.CreatedDate = DateTime.Now;
                merk.CreatedBy = User.Identity.Name;
                merk.DataStatus = "New";

                _merkTasks.Insert(merk);
            }

            return Json(new[] { merkVM }.ToDataSourceResult(request, ModelState));
        }

        private static void ConvertToMerk(UnitMerkViewModel merkVM, MMerk merk)
        {
            merk.MerkName = merkVM.MerkName;
            merk.MerkStatus = merkVM.MerkStatus;
            merk.MerkDesc = merkVM.MerkDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Merks_Update([DataSourceRequest] DataSourceRequest request, UnitMerkViewModel merkVM)
        {
            if (merkVM != null && ModelState.IsValid)
            {
                var merk = _merkTasks.One(merkVM.MerkID);
                if (merk != null)
                {
                    ConvertToMerk(merkVM, merk);

                    merk.ModifiedDate = DateTime.Now;
                    merk.ModifiedBy = User.Identity.Name;
                    merk.DataStatus = "Updated";

                    _merkTasks.Update(merk);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Merks_Destroy([DataSourceRequest] DataSourceRequest request, UnitMerkViewModel merkVM)
        {
            if (merkVM != null)
            {
                var merk = _merkTasks.One(merkVM.MerkID);
                if (merk != null)
                {
                    //merk.ModifiedDate = DateTime.Now;
                    //merk.ModifiedBy = User.Identity.Name;
                    //merk.DataStatus = "Deleted";
                    _merkTasks.Delete(merk);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<UnitMerkViewModel> GetMerks()
        {
            var merkomers = this._merkTasks.GetListNotDeleted();

            return from merk in merkomers
                   select new UnitMerkViewModel
        {
            MerkID = merk.Id,
            MerkName = merk.MerkName,
            MerkDesc = merk.MerkDesc,
            MerkStatus = merk.MerkStatus
        };

        }

        public JsonResult PopulateMerks()
        {
            return Json(GetMerks(), JsonRequestBehavior.AllowGet);
        }
    }
}
