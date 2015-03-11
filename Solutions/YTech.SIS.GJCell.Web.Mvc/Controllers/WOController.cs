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
    public class WOController : Controller
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
        public WOController(IMCustomerTasks customerTasks, ITWOTasks woTasks, ITReferenceTasks refTasks, ITWOLogTasks woLogTasks, ITWOStatusTasks woStatusTasks, ITWOTrackTasks woTrackTasks, IMMerkTasks merkTasks, IMTypeTasks typeTasks, ITWOSPartTasks woSPartTasks, IMSPartTasks spartTasks, IMEmpTasks empTasks)
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
            PopulateCustomers();
            PopulateWOStatus();
            PopulateSCToko();
            PopulatePriority();
            return View();
        }

        public ActionResult ReloadCustomers(string random)
        {
            PopulateCustomers();
            return View();
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, TEKNISI")]
        public ActionResult UpdateStatus()
        {
            PopulateCustomers();
            PopulateWOStatus();
            PopulateSCToko();
            PopulatePriority();
            return View();
        }

        private void PopulateWOStatus()
        {
            var wostatus = from EnumWOStatus e in Enum.GetValues(typeof(EnumWOStatus))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["wo_status"] = wostatus;

        }

        private void PopulateSCToko()
        {
            var wostatus = from EnumSCToko e in Enum.GetValues(typeof(EnumSCToko))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["sc_toko"] = wostatus;
        }

        private void PopulatePriority()
        {
            var wostatus = from EnumPriority e in Enum.GetValues(typeof(EnumPriority))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["priority"] = wostatus;

        }

        public JsonResult PopulateCustomers()
        {
            IEnumerable<CustomerViewModel> customers = from cust in _customerTasks.GetListNotDeleted()
                                                       select new CustomerViewModel
                                                       {
                                                           CustomerID = cust.Id,
                                                           CustomerName = cust.CustomerName + " - " + cust.CustomerPhone
                                                       };
            ViewData["customers"] = customers;
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintWOFactur(string random, string woId)
        {
            string msg = string.Empty;
            bool success = false;
            bool allowPrint = true;
            EnumReports rptToPrint = EnumReports.RptPrintWOFactur;
            try
            {
                //get wo by wo id
                TWO wo = this._woTasks.One(woId);
                //check if user have print WO, if not, allow print for role CS
                if (User.IsInRole("CS"))
                {
                    allowPrint = !_woLogTasks.HaveBeenPrint(wo, User.Identity.Name);
                }

                if (allowPrint)
                {
                    ReportParameterCollection paramCol = null;
                    ReportDataSource[] repCol = new ReportDataSource[1];
                    //get data source
                    IList<WOViewModel> listWO = GetWOById(wo);
                    ReportDataSource reportDataSource = new ReportDataSource("WOViewModel", listWO);
                    repCol[0] = reportDataSource;
                    //set rpt to print
                    switch (listWO[0].MerkName)
                    {
                        case "CROSS": rptToPrint = EnumReports.RptPrintWOFacturEvercoss;
                            break;
                        case "ADVAN": rptToPrint = EnumReports.RptPrintWOFacturAdvan;
                            break;
                        default: rptToPrint = EnumReports.RptPrintWOFacturGJCell;
                            break;
                    }

                    //save log
                    SaveLog(wo, EnumWOLog.Print);

                    Session["ReportData"] = repCol;
                    Session["ReportParams"] = paramCol;

                    success = true;
                    msg = "Print WO success";
                }
                else
                {
                    success = false;
                    msg = "Anda sudah pernah mencetak WO";
                }
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg,
                UrlReport = string.Format("{0}&rs%3aFormat=HTML4.0", rptToPrint.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogWO_Open(string random, string woId)
        {
            string msg = string.Empty;
            bool success = false;
            try
            {
                //get wo by wo id
                TWO wo = this._woTasks.One(woId);
                //save log
                SaveLog(wo, EnumWOLog.Read);

                success = true;
                msg = "Log WO success";
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private IList<WOViewModel> GetWOById(TWO wo)
        {
            WOViewModel vm = new WOViewModel
                     {
                         //WOID = wo.Id,
                         //CustomerName = wo.CustomerId.CustomerName,
                         //CustomerPhone = wo.CustomerId.CustomerPhone,
                         //CustomerAddress = wo.CustomerId.CustomerAddress,
                         //WODate = wo.WODate,
                         //WONo = wo.WONo,
                         //WOUnitIsGuarantee = wo.WOUnitIsGuarantee,
                         //WOEquipments = wo.WOEquipments,
                         //WOPriority = wo.WOPriority,
                         //WOBrokenDesc = wo.WOBrokenDesc,
                         //WOLastStatus = wo.WOLastStatus,
                         //WOStartDate = wo.WOStartDate,
                         //WOTotal = wo.WOTotal,
                         //WODp = wo.WODp,
                         //WOTakenDate = wo.WOTakenDate,
                         //WOInvoiceNo = wo.WOInvoiceNo,
                         //WOComplain = wo.WOComplain,
                         //WOUnitImei = wo.WOUnitImei,
                         //WOUnitColor = wo.WOUnitColor

                         WOID = wo.Id,
                         //Customer = ConvertToCustomerVM(wo.CustomerId.Id, wo.CustomerId.CustomerName),
                         CustomerName = wo.CustomerId == null ? string.Empty : wo.CustomerId.CustomerName,
                         CustomerPhone = wo.CustomerId == null ? string.Empty : wo.CustomerId.CustomerPhone,
                         CustomerAddress = wo.CustomerId == null ? string.Empty : wo.CustomerId.CustomerAddress,
                         // HiddenCustomerId = wo.CustomerId.Id,
                         WODate = wo.WODate,
                         WONo = wo.WONo,
                         WOUnitName = wo.WOUnitName,
                         WOUnitSn = wo.WOUnitSn,
                         WOUnitIsGuarantee = wo.WOUnitIsGuarantee,
                         WOEquipments = wo.WOEquipments,
                         WOPriority = wo.WOPriority,
                         WOBrokenDesc = wo.WOBrokenDesc,
                         WOLastStatus = wo.WOLastStatus,
                         WOStartDate = wo.WOStartDate,
                         WOEstFinishDate = wo.WOEstFinishDate,
                         WOTotal = wo.WOTotal,
                         WODp = wo.WODp,
                         WOTakenDate = wo.WOTakenDate,
                         WOInvoiceNo = wo.WOInvoiceNo,
                         WOComplain = wo.WOComplain,
                         //HaveBeenRead = wo.HaveBeenRead,
                         WOUnitLastTrack = wo.WOUnitLastTrack,
                         MerkId = wo.MerkId,
                         MerkName = wo.MerkId == null ? string.Empty : wo.MerkId.MerkName,
                         TypeId = wo.TypeId,
                         TypeName = wo.TypeId == null ? string.Empty : wo.TypeId.TypeName,
                         //WOTrackId = wo.WOTrackId,
                         //WOTrackTo = wo.WOTrackTo,
                         //WOTrackIsConfirmed = wo.WOTrackIsConfirmed,
                         WOUnitImei = wo.WOUnitImei,
                         WOUnitColor = wo.WOUnitColor
                     };

            IList<WOViewModel> listWO = new List<WOViewModel>();
            listWO.Add(vm);
            return listWO;

        }

        private void SaveLog(TWO wo, EnumWOLog enumWOLog)
        {
            TWOLog woLog = new TWOLog();
            woLog.SetAssignedIdTo(Guid.NewGuid().ToString());
            woLog.WOId = wo;
            woLog.LogUser = User.Identity.Name;
            woLog.LogDate = DateTime.Now;
            woLog.LogStatus = enumWOLog.ToString();

            woLog.CreatedDate = DateTime.Now;
            woLog.CreatedBy = User.Identity.Name;
            woLog.DataStatus = "New";
            _woLogTasks.Insert(woLog);
        }

        private string GetNewWONo()
        {
            TReference refer = _refTasks.GetByReferenceType(EnumReferenceType.WONo);
            bool automatedIncrease = true;
            decimal no = Convert.ToDecimal(refer.ReferenceValue) + 1;

            //reset wo no to 1 every month
            if (DateTime.Today.Day == 1 && refer.ModifiedDate < DateTime.Today)
                no = 1;

            refer.ReferenceValue = no.ToString();
            if (automatedIncrease)
            {
                refer.ModifiedDate = DateTime.Now;
                refer.ModifiedBy = User.Identity.Name;
                refer.DataStatus = "Updated";
                _refTasks.Update(refer);
            }

            StringBuilder result = new StringBuilder();
            result.Append("WO[YEAR][MONTH][XXXX]");
            result.Replace("[XXXX]", GetNo(4, no));
            result.Replace("[DAY]", DateTime.Today.Day.ToString());
            result.Replace("[MONTH]", DateTime.Today.ToString("MM").ToUpper());
            result.Replace("[YEAR]", DateTime.Today.ToString("yy"));
            return result.ToString();
        }

        private static string GetNo(int maxLength, decimal no)
        {
            int len = maxLength - no.ToString().Length;
            string factur = no.ToString();
            for (int i = 0; i < len; i++)
            {
                factur = "0" + factur;
            }
            return factur;
        }

        public ActionResult WO_Read_By_Imei(string imei, [DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<WOViewModel> orderedWOS;
            orderedWOS = from wo in GetWOs()
                         where wo.WOUnitImei == imei
                         orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                         select wo;

            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult WO_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<WOViewModel> orderedWOS;
            //get wo list that  last track is to  all user 
            if (User.IsInRole("ADMINISTRATOR") || User.IsInRole("SUPERVISOR"))
            {
                orderedWOS = from wo in GetWOs()
                             orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                             select wo;
            }
            //get wo list that last track is to current user
            else
            {
                orderedWOS = from wo in GetWOs()
                             where wo.WOUnitLastTrack == User.Identity.Name
                             orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                             select wo;
            }

            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult WOStatus_Read(string woID, [DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetWOStatus(woID).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WO_Create([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM, FormCollection formCol)
        {
            var errors = ModelState
    .Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();

            if (WOVM != null && ModelState.IsValid)
            {
                TWO wo = new TWO();
                wo.SetAssignedIdTo(Guid.NewGuid().ToString());

                wo.WONo = GetNewWONo();

                ConvertToWO(WOVM, wo, formCol);

                wo.WOUnitLastTrack = User.Identity.Name;
                wo.CreatedDate = DateTime.Now;
                wo.CreatedBy = User.Identity.Name;
                wo.DataStatus = "New";

                _woTasks.Insert(wo);
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToWO(WOViewModel WOVM, TWO wo, FormCollection formCol)
        {
            //get customer id from form collection, customerVM not catch the customer id :(
            string custId = formCol["HiddenCustomerId"];
            string typeId = string.IsNullOrEmpty(formCol["TypeId.TypeID"]) ? formCol["TypeId"] : formCol["TypeId.TypeID"];
            string merkId = string.IsNullOrEmpty(formCol["MerkId.MerkID"]) ? formCol["MerkId"] : formCol["MerkId.MerkID"];

            //wo.WONo = WOVM.WONo;
            wo.CustomerId = GetCustomer(custId);
            wo.WODate = WOVM.WODate;
            wo.WOUnitName = WOVM.WOUnitName;
            wo.WOUnitSn = WOVM.WOUnitSn;
            wo.WOUnitIsGuarantee = WOVM.WOUnitIsGuarantee;
            wo.WOEquipments = WOVM.WOEquipments;
            wo.WOPriority = WOVM.WOPriority;
            wo.WOStartDate = WOVM.WOStartDate;
            wo.WOLastStatus = WOVM.WOLastStatus;
            wo.WOEstFinishDate = WOVM.WOEstFinishDate;
            wo.WOTotal = WOVM.WOTotal;
            wo.WODp = WOVM.WODp;
            wo.WOInvoiceNo = WOVM.WOInvoiceNo;
            wo.WOTakenDate = WOVM.WOTakenDate;
            wo.WOBrokenDesc = WOVM.WOBrokenDesc;
            wo.WODesc = WOVM.WODesc;
            wo.WOComplain = WOVM.WOComplain;
            wo.MerkId = string.IsNullOrEmpty(merkId) ? null : _merkTasks.One(merkId);
            wo.TypeId = string.IsNullOrEmpty(typeId) ? null : _typeTasks.One(typeId);
            wo.WOUnitImei = WOVM.WOUnitImei;
            wo.WOUnitColor = WOVM.WOUnitColor;
            wo.WODateSentToSC = WOVM.WODateSentToSC;
            wo.WODateReceivedFromSC = WOVM.WODateReceivedFromSC;
            wo.WOServiceFee = WOVM.WOServiceFee;
            wo.WOSPartTotal = WOVM.WOSPartTotal;
            wo.WOReferenceNo = WOVM.WOReferenceNo;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WO_Update([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM, FormCollection formCol)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    ConvertToWO(WOVM, wo, formCol);

                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Updated";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, TEKNISI")]
        public ActionResult WO_UpdateStatus([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    wo.WONo = WOVM.WONo;
                    //wo.CustomerId = GetCustomer(WOVM.Customer.CustomerID);
                    //wo.WODate = WOVM.WODate;
                    //wo.WOItemType = WOVM.WOItemType;
                    //wo.WOItemSn = WOVM.WOItemSN;
                    //wo.WOIsGuarantee = WOVM.WOIsGuarantee;
                    //wo.WOEquipments = WOVM.WOEquipments;
                    //wo.WOScStore = WOVM.WOScStore;
                    //wo.WOPriority = WOVM.WOPriority;
                    wo.WOStartDate = WOVM.WOStartDate;
                    wo.WOLastStatus = WOVM.WOLastStatus;
                    wo.WOEstFinishDate = WOVM.WOEstFinishDate;
                    //wo.WOTotal = WOVM.WOTotal;
                    //wo.WODp = WOVM.WODp;
                    //wo.WOInvoiceNo = WOVM.WOInvoiceNo;
                    //wo.WOTakenDate = WOVM.WOTakenDate;
                    wo.WOBrokenDesc = WOVM.WOBrokenDesc;
                    //wo.WODesc = WOVM.WODesc;

                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Updated";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR")]
        public ActionResult WO_Destroy([DataSourceRequest] DataSourceRequest request, WOViewModel WOVM)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                var wo = _woTasks.One(WOVM.WOID);
                if (wo != null)
                {
                    wo.ModifiedDate = DateTime.Now;
                    wo.ModifiedBy = User.Identity.Name;
                    wo.DataStatus = "Deleted";

                    _woTasks.Update(wo);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult ServerDate(string random)
        {
            return Content(DateTime.Now.ToString());
        }

        private MCustomer GetCustomer(string custId)
        {
            return _customerTasks.One(custId);
        }

        private IEnumerable<WOViewModel> GetWOs()
        {
            var wos = this._woTasks.GetListNotDeleted(User.Identity.Name);

            return from wo in wos
                   select new WOViewModel
                   {
                       WOID = wo.Id,
                       Customer = ConvertToCustomerVM(wo.CustomerId, wo.CustomerName),
                       CustomerName = wo.CustomerName + " - " + wo.CustomerPhone,
                       CustomerPhone = wo.CustomerPhone,
                       CustomerAddress = wo.CustomerAddress,
                       HiddenCustomerId = wo.CustomerId,
                       WODate = wo.WODate,
                       WONo = wo.WONo,
                       WOUnitName = wo.WOUnitName,
                       WOUnitSn = wo.WOUnitSn,
                       WOUnitIsGuarantee = wo.WOUnitIsGuarantee,
                       WOEquipments = wo.WOEquipments,
                       WOPriority = wo.WOPriority,
                       WOBrokenDesc = wo.WOBrokenDesc,
                       WOLastStatus = wo.WOLastStatus,
                       WOStartDate = wo.WOStartDate,
                       WOEstFinishDate = wo.WOEstFinishDate,
                       WOTotal = wo.WOTotal,
                       WODp = wo.WODp,
                       WOTakenDate = wo.WOTakenDate,
                       WOInvoiceNo = wo.WOInvoiceNo,
                       WOComplain = wo.WOComplain,
                       HaveBeenRead = wo.HaveBeenRead,
                       WOUnitLastTrack = wo.WOUnitLastTrack,
                       MerkId = wo.MerkId,
                       MerkName = wo.MerkName,
                       TypeId = wo.TypeId,
                       TypeName = wo.TypeName,
                       WOTrackId = wo.WOTrackId,
                       WOTrackTo = wo.WOTrackTo,
                       WOTrackIsConfirmed = wo.WOTrackIsConfirmed,
                       WOUnitImei = wo.WOUnitImei,
                       WOUnitColor = wo.WOUnitColor,
                       WOSPartTotal = wo.WOSPartTotal,
                       WOReferenceNo = wo.WOReferenceNo,
                       WODateReceivedFromSC = wo.WODateReceivedFromSC,
                       WODateSentToSC = wo.WODateSentToSC,
                       WODesc = wo.WODesc,
                       WOServiceFee = wo.WOServiceFee

                       //use stored procedure, looping to get read flag make wo list slow response
                       //HaveBeenRead = _woLogTasks.HaveBeenRead(wo, User.Identity.Name)
                   };
        }

        private IEnumerable<WOStatusViewModel> GetWOStatus(string woId)
        {
            var wos = this._woStatusTasks.GetWOStatus(woId);

            return from wo in wos
                   select new WOStatusViewModel
                   {
                       WOStatusId = wo.Id,
                       WOStatusUser = wo.WOStatusUser,
                       WOStatus = wo.WOStatus,
                       WOStatusDate = wo.WOStatusDate,
                       WOStatusBrokenDesc = wo.WOStatusBrokenDesc,
                       WOStatusStartDate = wo.WOStatusStartDate,
                       WOStatusFinishDate = wo.WOStatusFinishDate
                   };
        }

        private CustomerViewModel ConvertToCustomerVM(string customerId, string customerName)
        {
            CustomerViewModel custVM = new CustomerViewModel();
            if (string.IsNullOrEmpty(customerId))
            {
                custVM.CustomerID = string.Empty;
                custVM.CustomerName = "Select a value";
            }
            else
            {
                custVM.CustomerID = customerId;
                custVM.CustomerName = customerName;
            }
            return custVM;
        }

        //[Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WOMutation()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        //[Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")]
        public ActionResult WOMutation([DataSourceRequest] DataSourceRequest request, WOMutationViewModel WOVM, FormCollection formCol)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                TWOTrack woTrack = new TWOTrack();
                woTrack.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToWOTrack(WOVM, woTrack);

                woTrack.CreatedDate = DateTime.Now;
                woTrack.CreatedBy = User.Identity.Name;
                woTrack.DataStatus = "New";

                _woTrackTasks.Insert(woTrack);
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToWOTrack(WOMutationViewModel WOVM, TWOTrack woTrack)
        {
            woTrack.WOId = _woTasks.One(WOVM.MutationWOId);
            woTrack.WOTrackFrom = User.Identity.Name;
            woTrack.WOTrackTo = WOVM.UserName;
            woTrack.WOTrackDate = DateTime.Now;
            woTrack.WOTrackIsConfirmed = false;
        }

        public ActionResult ApproveWOMutation()
        {
            return View();
        }

        public ActionResult ApproveWOMutation_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<WOViewModel> orderedWOS;
            //get wo list that mutation to all user that not confirmed yet
            if (User.IsInRole("ADMINISTRATOR") || User.IsInRole("SUPERVISOR"))
            {
                orderedWOS = from wo in GetWOs()
                             where wo.WOTrackIsConfirmed == false
                             orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                             select wo;
            }
            //get wo list that mutation to current user that not confirmed yet
            else
            {
                orderedWOS = from wo in GetWOs()
                             where wo.WOTrackTo == User.Identity.Name && wo.WOTrackIsConfirmed == false
                             orderby wo.HaveBeenRead, wo.WOPriority descending, wo.WONo descending
                             select wo;
            }

            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ApproveWOMutation(string random, string woTrackId)
        {
            string msg = string.Empty;
            bool success = false;
            try
            {
                //get woTrack by woTrackId
                TWOTrack woTrack = this._woTrackTasks.One(woTrackId);
                //save woTrack
                if (woTrack != null)
                {
                    woTrack.WOTrackIsConfirmed = true;
                    woTrack.WOTrackConfirmedDate = DateTime.Now;
                    woTrack.WOTrackDesc = "";

                    woTrack.ModifiedDate = DateTime.Now;
                    woTrack.ModifiedBy = User.Identity.Name;
                    woTrack.DataStatus = "Update";
                    _woTrackTasks.ConfirmTrack(woTrack);

                    SaveLog(woTrack.WOId, EnumWOLog.Mutation);
                }

                success = true;
                msg = "Mutasi Unit berhasil";
            }
            catch (Exception ex)
            {
                success = false;
                msg = ex.GetBaseException().Message;
            }
            var e = new
            {
                Success = success,
                Message = msg
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWoByEmp([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<VWoByEmp> orderedWOS = this._woTasks.GetWoByEmp(DateTime.Today.AddYears(-1), DateTime.Today);

            DataSourceResult result = orderedWOS.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult RequestWOSPart()
        {
            RequestWOSPartViewModel view = new RequestWOSPartViewModel();
            view.WOSPartQty = 1;
            return View(view);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RequestWOSPart([DataSourceRequest] DataSourceRequest request, RequestWOSPartViewModel WOVM, FormCollection formCol)
        {
            if (WOVM != null && ModelState.IsValid)
            {
                TWOSPart woSPart = new TWOSPart();
                woSPart.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToWOSPart(WOVM, woSPart);

                woSPart.CreatedDate = DateTime.Now;
                woSPart.CreatedBy = User.Identity.Name;
                woSPart.DataStatus = "New";

                _woSPartTasks.Insert(woSPart);
            }

            return Json(new[] { WOVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToWOSPart(RequestWOSPartViewModel WOVM, TWOSPart woSPart)
        {
            woSPart.WOId = _woTasks.One(WOVM.WOId);
            woSPart.SPartId = string.IsNullOrEmpty(WOVM.SPartId) ? null : _spartTasks.One(WOVM.SPartId);
            woSPart.WOSPartPrice = WOVM.WOSPartPrice;
            woSPart.WOSPartTotal = WOVM.WOSPartTotal;
            woSPart.WOSPartQty = WOVM.WOSPartQty;
            woSPart.WOSPartDisc = WOVM.WOSPartDisc;
            woSPart.WOSPartDate = WOVM.WOSPartDate;
            woSPart.WOSPartStatus = EnumWOSPartStatus.Request.ToString();
            woSPart.WOSPartRequestBy = string.IsNullOrEmpty(WOVM.WOSPartRequestBy) ? null : _empTasks.One(WOVM.WOSPartRequestBy);
            woSPart.WOSPartDateRequest = WOVM.WOSPartDate;
        }

        public ActionResult Histories()
        {
            return View();
        }
    }
}
