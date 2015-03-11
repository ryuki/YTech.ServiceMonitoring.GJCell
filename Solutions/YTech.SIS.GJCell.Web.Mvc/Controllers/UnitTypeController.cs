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
    public partial class UnitTypeController : Controller
    {
        private readonly IMTypeTasks _typeTasks;
        private readonly IMMerkTasks _merkTasks;
        public UnitTypeController(IMTypeTasks typeTasks, IMMerkTasks merkTasks)
        {
            this._typeTasks = typeTasks;
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

        public ActionResult Types_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetTypes().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Types_Create([DataSourceRequest] DataSourceRequest request, UnitTypeViewModel typeVM)
        {
            if (typeVM != null && ModelState.IsValid)
            {
                MType type = new MType();
                type.SetAssignedIdTo(typeVM.TypeID);

                ConvertToType(typeVM, type);

                type.CreatedDate = DateTime.Now;
                type.CreatedBy = User.Identity.Name;
                type.DataStatus = "New";

                _typeTasks.Insert(type);
            }

            return Json(new[] { typeVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToType(UnitTypeViewModel typeVM, MType type)
        {
            type.TypeName = typeVM.TypeName;
            type.TypeStatus = typeVM.TypeStatus;
            type.TypeDesc = typeVM.TypeDesc;
            type.MerkId = string.IsNullOrEmpty(typeVM.MerkId) ? null : _merkTasks.One(typeVM.MerkId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Types_Update([DataSourceRequest] DataSourceRequest request, UnitTypeViewModel typeVM)
        {
            if (typeVM != null && ModelState.IsValid)
            {
                var type = _typeTasks.One(typeVM.TypeID);
                if (type != null)
                {
                    ConvertToType(typeVM, type);

                    type.ModifiedDate = DateTime.Now;
                    type.ModifiedBy = User.Identity.Name;
                    type.DataStatus = "Updated";

                    _typeTasks.Update(type);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Types_Destroy([DataSourceRequest] DataSourceRequest request, UnitTypeViewModel typeVM)
        {
            if (typeVM != null)
            {
                var type = _typeTasks.One(typeVM.TypeID);
                if (type != null)
                {
                    //type.ModifiedDate = DateTime.Now;
                    //type.ModifiedBy = User.Identity.Name;
                    //type.DataStatus = "Deleted";
                    _typeTasks.Delete(type);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<UnitTypeViewModel> GetTypes()
        {
            var typeomers = this._typeTasks.GetListNotDeleted();

            return from type in typeomers
                   select new UnitTypeViewModel
        {
            TypeID = type.Id,
            TypeName = type.TypeName,
            TypeDesc = type.TypeDesc,
            TypeStatus = type.TypeStatus,
            MerkName = type.MerkId != null ? type.MerkId.MerkName : string.Empty,
            MerkId = type.MerkId != null ? type.MerkId.Id : string.Empty
        };

        }

        public JsonResult PopulateTypes()
        {
            return Json(GetTypes(), JsonRequestBehavior.AllowGet);
        }

    }
}
