using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Enums;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public class WOSPartController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly ITWOTasks _woTasks;
        private readonly ITReferenceTasks _refTasks;
        private readonly ITWOLogTasks _woLogTasks;
        private readonly ITWOStatusTasks _woStatusTasks;
        private readonly ITWOTrackTasks _woTrackTasks;
        private readonly IMMerkTasks _merkTasks;
        private readonly IMTypeTasks _typeTasks;
        private readonly ITWOSPartTasks _woSPartTasks;
        private readonly IMSPartTasks _spartTasks;
        private readonly IMEmpTasks _empTasks;
        public WOSPartController(IMCustomerTasks customerTasks, ITWOTasks woTasks, ITReferenceTasks refTasks, ITWOLogTasks woLogTasks, ITWOStatusTasks woStatusTasks, ITWOTrackTasks woTrackTasks, IMMerkTasks merkTasks, IMTypeTasks typeTasks, ITWOSPartTasks woSPartTasks, IMSPartTasks spartTasks, IMEmpTasks empTasks)
        {
            this._customerTasks = customerTasks;
            this._woTasks = woTasks;
            this._refTasks = refTasks;
            this._woLogTasks = woLogTasks;
            this._woStatusTasks = woStatusTasks;
            this._woTrackTasks = woTrackTasks;
            this._merkTasks = merkTasks;
            this._typeTasks = typeTasks;
            this._woSPartTasks = woSPartTasks;
            this._spartTasks = spartTasks;
            this._empTasks = empTasks;
        }

        //[Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult Index()
        {
            return View();
        }

        private void PopulateWOSPartStatus()
        {
            var wostatus = from EnumWOSPartStatus e in Enum.GetValues(typeof(EnumWOSPartStatus))
                           where e != EnumWOSPartStatus.Request
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["wo_spart_status"] = wostatus;
        }

        public ActionResult WOSPart_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<RequestWOSPartViewModel> orderedWOS = GetWOSPart();

            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        private IEnumerable<RequestWOSPartViewModel> GetWOSPart()
        {
            //use temporary date, todo : create new function
            DateTime fromDate = new DateTime(2000, 1, 1);
            var wos = this._woSPartTasks.GetListBySPartDate(fromDate, DateTime.Today);

            return from wo in wos
                   where wo.WOSPartStatus == EnumWOSPartStatus.Request.ToString()
                   select new RequestWOSPartViewModel
                   {
                       WOSPartId = wo.Id,
                       WONo = wo.WOId.WONo,
                       SPartId = wo.SPartId.Id,
                       SPartName = wo.SPartId.SPartName,
                       WOSPartQty = wo.WOSPartQty,
                       WOSPartPrice = wo.WOSPartPrice,
                       WOSPartDisc = wo.WOSPartDisc,
                       WOSPartTotal = wo.WOSPartTotal,
                       WOSPartRequestBy = wo.WOSPartRequestBy == null ? string.Empty : wo.WOSPartRequestBy.EmpName,
                       WOSPartDateRequest = wo.WOSPartDateRequest
                   };
        }


        public ActionResult ChangeStatus()
        {
            PopulateWOSPartStatus();
            RequestWOSPartViewModel view = new RequestWOSPartViewModel();
            return View(view);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeStatus([DataSourceRequest] DataSourceRequest request, RequestWOSPartViewModel WOVM, FormCollection formCol)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                TWOSPart woSPart = _woSPartTasks.One(WOVM.WOSPartId);
                if (woSPart != null)
                {
                    woSPart.WOSPartStatus = WOVM.WOSPartStatus;
                    if (WOVM.WOSPartStatus == EnumWOSPartStatus.Serah_Terima_dan_Gunakan.ToString())
                    {
                        woSPart.WOSPartDateReceived = WOVM.WOSPartDateReceived;
                        woSPart.WOSPartReceivedBy = string.IsNullOrEmpty(WOVM.WOSPartReceivedBy) ? null : _empTasks.One(WOVM.WOSPartReceivedBy); 
                    }
                    else if (WOVM.WOSPartStatus == EnumWOSPartStatus.Retur.ToString())
                    {
                        woSPart.WOSPartDateReturn = WOVM.WOSPartDateReceived;
                        woSPart.WOSPartReturnBy = string.IsNullOrEmpty(WOVM.WOSPartReceivedBy) ? null : _empTasks.One(WOVM.WOSPartReceivedBy); 
                    }

                    woSPart.ModifiedDate = DateTime.Now;
                    woSPart.ModifiedBy = User.Identity.Name;
                    woSPart.DataStatus = "Updated";

                    _woSPartTasks.Update(woSPart);

                    //update wo spare part total
                    TWO wo = woSPart.WOId;
                    if (wo != null)
                    {
                        wo.WOSPartTotal = wo.WOSPartTotal + woSPart.WOSPartTotal;
                        wo.WOTotal = wo.WOServiceFee + wo.WOSPartTotal;

                        wo.ModifiedDate = DateTime.Now;
                        wo.ModifiedBy = User.Identity.Name;
                        wo.DataStatus = "Updated";
                        _woTasks.Update(wo);
                    }
                }
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }
    }
}