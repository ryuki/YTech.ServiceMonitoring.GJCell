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
    public partial class EquipController : Controller
    {
        private readonly IMEquipTasks _equipTasks;
        public EquipController(IMEquipTasks equipTasks)
        {
            this._equipTasks = equipTasks;
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

        public ActionResult Equips_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEquips().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Create([DataSourceRequest] DataSourceRequest request, EquipViewModel equipVM)
        {
            if (equipVM != null && ModelState.IsValid)
            {
                MEquip equip = new MEquip();
                equip.SetAssignedIdTo(equipVM.EquipID);

                ConvertToEquip(equipVM, equip);

                equip.CreatedDate = DateTime.Now;
                equip.CreatedBy = User.Identity.Name;
                equip.DataStatus = "New";

                _equipTasks.Insert(equip);
            }

            return Json(new[] { equipVM }.ToDataSourceResult(request, ModelState));
        }

        private static void ConvertToEquip(EquipViewModel equipVM, MEquip equip)
        {
            equip.EquipName = equipVM.EquipName;
            equip.EquipStatus = equipVM.EquipStatus;
            equip.EquipDesc = equipVM.EquipDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Update([DataSourceRequest] DataSourceRequest request, EquipViewModel equipVM)
        {
            if (equipVM != null && ModelState.IsValid)
            {
                var equip = _equipTasks.One(equipVM.EquipID);
                if (equip != null)
                {
                    ConvertToEquip(equipVM, equip);

                    equip.ModifiedDate = DateTime.Now;
                    equip.ModifiedBy = User.Identity.Name;
                    equip.DataStatus = "Updated";

                    _equipTasks.Update(equip);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Equips_Destroy([DataSourceRequest] DataSourceRequest request, EquipViewModel equipVM)
        {
            if (equipVM != null)
            {
                var equip = _equipTasks.One(equipVM.EquipID);
                if (equip != null)
                {
                    //equip.ModifiedDate = DateTime.Now;
                    //equip.ModifiedBy = User.Identity.Name;
                    //equip.DataStatus = "Deleted";
                    _equipTasks.Delete(equip);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<EquipViewModel> GetEquips()
        {
            var equipomers = this._equipTasks.GetListNotDeleted();

            return from equip in equipomers
                   select new EquipViewModel
        {
            EquipID = equip.Id,
            EquipName = equip.EquipName,
            EquipDesc = equip.EquipDesc,
            EquipStatus = equip.EquipStatus
        };

        }

        public JsonResult PopulateEquips()
        {
            IEnumerable<EquipViewModel> equips = from equip in _equipTasks.GetListNotDeleted()
                                                       select new EquipViewModel
                                                       {
                                                           EquipID = equip.Id,
                                                           EquipName = equip.EquipName
                                                       };
            ViewData["equips"] = equips;
            return Json(equips, JsonRequestBehavior.AllowGet);
        }
    }
}
