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

namespace YTech.SIS.GJCell.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public partial class SPartController : Controller
    {
        private readonly IMSPartTasks spartTasks;
        private readonly IMMerkTasks _merkTasks;
        public SPartController(IMSPartTasks spartTasks, IMMerkTasks merkTasks)
        {
            this.spartTasks = spartTasks;
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

        public ActionResult SParts_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetSParts().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SParts_Create([DataSourceRequest] DataSourceRequest request, SPartViewModel spartVM)
        {
            if (spartVM != null && ModelState.IsValid)
            {
                MSPart spart = new MSPart();
                spart.SetAssignedIdTo(spartVM.SPartID);

                ConvertToSPart(spartVM, spart);

                spart.CreatedDate = DateTime.Now;
                spart.CreatedBy = User.Identity.Name;
                spart.DataStatus = "New";

                spartTasks.Insert(spart);
            }

            return Json(new[] { spartVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToSPart(SPartViewModel spartVM, MSPart spart)
        {
            spart.SPartName = spartVM.SPartName;
            spart.SPartPurchasePrice = spartVM.SPartPurchasePrice;
            spart.SPartServicePrice1 = spartVM.SPartServicePrice1;
            spart.SPartDesc = spartVM.SPartDesc;
            spart.MerkId = string.IsNullOrEmpty(spartVM.MerkId) ? null : _merkTasks.One(spartVM.MerkId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SParts_Update([DataSourceRequest] DataSourceRequest request, SPartViewModel spartVM)
        {
            if (spartVM != null && ModelState.IsValid)
            {
                var spart = spartTasks.One(spartVM.SPartID);
                if (spart != null)
                {
                    ConvertToSPart(spartVM, spart);

                    spart.ModifiedDate = DateTime.Now;
                    spart.ModifiedBy = User.Identity.Name;
                    spart.DataStatus = "Updated";

                    spartTasks.Update(spart);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SParts_Destroy([DataSourceRequest] DataSourceRequest request, SPartViewModel spartVM)
        {
            if (spartVM != null)
            {
                var spart = spartTasks.One(spartVM.SPartID);
                if (spart != null)
                {
                    //spart.ModifiedDate = DateTime.Now;
                    //spart.ModifiedBy = User.Identity.Name;
                    //spart.DataStatus = "Deleted";
                    spartTasks.Delete(spart);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<SPartViewModel> GetSParts()
        {
            var sparts = this.spartTasks.GetListNotDeleted();

            return from spart in sparts
                   select new SPartViewModel
        {
            SPartID = spart.Id,
            SPartName = spart.SPartName,
            SPartPurchasePrice = spart.SPartPurchasePrice,
            SPartServicePrice1 = spart.SPartServicePrice1,
            MerkId = spart.MerkId != null ? spart.MerkId.Id : string.Empty,
            SPartDesc = spart.SPartDesc
        };

        }

        public JsonResult PopulateSParts()
        {
            return Json(GetSParts(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSPart(string random, string sPartId)
        {
            return Json(GetSPart(sPartId), JsonRequestBehavior.AllowGet);
        }

        private SPartViewModel GetSPart(string sPartId)
        {
            var spart = this.spartTasks.One(sPartId);

            return new SPartViewModel
                   {
                       SPartID = spart.Id,
                       SPartName = spart.SPartName,
                       SPartPurchasePrice = spart.SPartPurchasePrice,
                       SPartServicePrice1 = spart.SPartServicePrice1,
                       MerkId = spart.MerkId != null ? spart.MerkId.Id : string.Empty,
                       SPartDesc = spart.SPartDesc
                   };
        }

    }
}
