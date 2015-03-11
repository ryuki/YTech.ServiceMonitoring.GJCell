using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using YTech.SIS.GJCell.Enums;
using YTech.SIS.GJCell.Web.Mvc.Controllers.ViewModels;
using Microsoft.CSharp;

namespace YTech.SIS.GJCell.Web.Mvc.Controllers
{
    [HandleError]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ITWOTasks _woTasks;
        private readonly IMCustomerTasks _customerTasks;
        private readonly IMEmpTasks _empTasks;
        private readonly IMMerkTasks _merkTasks;
        private readonly IMTypeTasks _typeTasks;
        private readonly IMSPartTasks _spartTasks;
        private readonly IMEquipTasks _equipTasks;
        private readonly ITWOSPartTasks _woSPartTasks;
        public ReportsController(IMCustomerTasks customerTasks, ITWOTasks woTasks, IMEmpTasks empTasks, IMMerkTasks merkTasks, IMTypeTasks typeTasks, IMSPartTasks spartTasks, IMEquipTasks equipTasks, ITWOSPartTasks woSPartTasks)
        {
            this._woTasks = woTasks;
            this._customerTasks = customerTasks;
            this._empTasks = empTasks;
            this._merkTasks = merkTasks;
            this._typeTasks = typeTasks;
            this._spartTasks = spartTasks;
            this._equipTasks = equipTasks;
            this._woSPartTasks = woSPartTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, KASIR, TEKNISI")]
        public ActionResult Index(EnumReports rpt)
        {
            string title = string.Empty;
            ReportsViewModel rptVM = new ReportsViewModel();
            switch (rpt)
            {
                case EnumReports.RptWODailyRecap:
                    rptVM.Title = "Laporan Harian Servis";
                    rptVM.ShowDateFrom = true;
                    rptVM.ShowDateTo = true;
                    break;
                case EnumReports.RptMasterCustomer:
                    rptVM.Title = "Daftar Konsumen";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterEmp:
                    rptVM.Title = "Daftar Karyawan";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterUnitType:
                    rptVM.Title = "Daftar Tipe Unit";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterUnitMerk:
                    rptVM.Title = "Daftar Merk Unit";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterSPart:
                    rptVM.Title = "Daftar Spare Part";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptMasterEquip:
                    rptVM.Title = "Daftar Perlengkapan";
                    rptVM.ShowDateFrom = false;
                    rptVM.ShowDateTo = false;
                    break;
                case EnumReports.RptWOTrack:
                    rptVM.Title = "Laporan Posisi Unit";
                    rptVM.ShowDateFrom = true;
                    rptVM.ShowDateTo = true;
                    break;
                case EnumReports.RptWOSpartUsed:
                    rptVM.Title = "Laporan Pemakaian Spare Part";
                    rptVM.ShowDateFrom = true;
                    rptVM.ShowDateTo = true;
                    break;
            }
            return View(rptVM);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(EnumReports rpt, ReportsViewModel rptVM)
        {
            ReportDataSource[] repCol = new ReportDataSource[1];
            ReportParameterCollection paramCol = null;
            switch (rpt)
            {
                case EnumReports.RptWODailyRecap:
                    repCol[0] = GetWOs(rptVM.RptDateFrom, rptVM.RptDateTo);

                    //set params 
                    paramCol = new ReportParameterCollection();
                    paramCol.Add(new ReportParameter("V_START_DATE", rptVM.RptDateFrom.Value.ToString()));
                    paramCol.Add(new ReportParameter("V_END_DATE", rptVM.RptDateTo.Value.ToString()));
                    break;
                case EnumReports.RptMasterCustomer:
                    repCol[0] = GetCustomers();
                    break;
                case EnumReports.RptMasterEmp:
                    repCol[0] = GetEmps();
                    break;
                case EnumReports.RptMasterUnitType:
                    repCol[0] = GetTypes();
                    break;
                case EnumReports.RptMasterUnitMerk:
                    repCol[0] = GetMerks();
                    break;
                case EnumReports.RptMasterSPart:
                    repCol[0] = GetSParts();
                    break;
                case EnumReports.RptMasterEquip:
                    repCol[0] = GetEquips();
                    break;
                case EnumReports.RptWOTrack:
                    repCol[0] = GetWOs(rptVM.RptDateFrom, rptVM.RptDateTo);

                    //set params 
                    paramCol = new ReportParameterCollection();
                    paramCol.Add(new ReportParameter("V_START_DATE", rptVM.RptDateFrom.Value.ToString()));
                    paramCol.Add(new ReportParameter("V_END_DATE", rptVM.RptDateTo.Value.ToString()));
                    break;
                case EnumReports.RptWOSpartUsed:
                    repCol[0] = GetWOSpart(rptVM.RptDateFrom, rptVM.RptDateTo);
                    break;
            }

            //send report data source and report params to session data
            Session["ReportData"] = repCol;
            Session["ReportParams"] = paramCol;

            var e = new
            {
                Success = true,
                Message = "redirect",
                UrlReport = string.Format("{0}", rpt.ToString())
            };
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        private ReportDataSource GetWOSpart(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            var sparts = from wo in this._woSPartTasks.GetListBySPartDate(rptDateFrom, rptDateTo)
                         select new RequestWOSPartViewModel
                                         {
                                             WOSPartId = wo.Id,
                                             WOSPartStatus = wo.WOSPartStatus,
                                             WONo = wo.WOId.WONo,
                                             SPartId = wo.SPartId.Id,
                                             SPartName = wo.SPartId.SPartName,
                                             WOSPartQty = wo.WOSPartQty,
                                             WOSPartPrice = wo.WOSPartPrice,
                                             WOSPartDisc = wo.WOSPartDisc,
                                             WOSPartTotal = wo.WOSPartTotal,
                                             WOSPartRequestBy = wo.WOSPartRequestBy == null ? string.Empty : wo.WOSPartRequestBy.EmpName,
                                             WOSPartDateRequest = wo.WOSPartDateRequest,
                                             WOSPartReceivedBy = wo.WOSPartReceivedBy == null ? string.Empty : wo.WOSPartReceivedBy.EmpName,
                                             WOSPartDateReceived = wo.WOSPartDateReceived,
                                             WOSPartReturnBy = wo.WOSPartReturnBy == null ? string.Empty : wo.WOSPartReturnBy.EmpName,
                                             WOSPartDateReturn = wo.WOSPartDateReturn
                                         };

            //    var sparts = from spart in this._woSPartTasks.GetListBySPartDate(rptDateFrom, rptDateTo) 
            //                     select new {
            //                         WOId = spart.WOId.Id,
            //                         SPartId = spart.SPartId.SPartName,
            //                         spart.WOSPartDate,
            //                         spart.WOSPartDisc,
            //                         spart.WOSPartPrice,
            //                         spart.WOSPartQty,
            //                         spart.WOSPartTotal
            //}
            //                     ;
            ReportDataSource reportDataSource = new ReportDataSource("RequestWOSPartViewModel", sparts);
            return reportDataSource;
        }

        private ReportDataSource GetEquips()
        {
            var equips = this._equipTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("MEquip", equips);
            return reportDataSource;
        }

        private ReportDataSource GetSParts()
        {
            var sparts = this._spartTasks.GetAllSParts();
            ReportDataSource reportDataSource = new ReportDataSource("MSPart", sparts);
            return reportDataSource;
        }

        private ReportDataSource GetMerks()
        {
            var merks = this._merkTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("MMerk", merks);
            return reportDataSource;
        }

        private ReportDataSource GetTypes()
        {
            var types = this._typeTasks.GetListNotDeleted();
            ReportDataSource reportDataSource = new ReportDataSource("MType", types);
            return reportDataSource;
        }

        private ReportDataSource GetEmps()
        {
            var emps = this._empTasks.GetAllEmps();
            ReportDataSource reportDataSource = new ReportDataSource("MEmp", emps);
            return reportDataSource;
        }

        private ReportDataSource GetCustomers()
        {
            var customers = this._customerTasks.GetAllCustomers();
            ReportDataSource reportDataSource = new ReportDataSource("MCustomer", customers);
            return reportDataSource;
        }

        private ReportDataSource GetWOs(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            var wos = this._woTasks.GetWOByDate(rptDateFrom, rptDateTo);

            var vm = from wo in wos
                     select new WOViewModel
                     {
                         WOID = wo.Id,
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
                     };

            ReportDataSource reportDataSource = new ReportDataSource("WOViewModel", vm);
            return reportDataSource;
        }

    }
}
