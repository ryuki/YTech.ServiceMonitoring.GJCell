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
    public partial class EmpController : Controller
    {
        private readonly IMEmpTasks empTasks;
        public EmpController(IMEmpTasks empTasks)
        {
            this.empTasks = empTasks;
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

        public ActionResult Emps_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetEmps().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Emps_Create([DataSourceRequest] DataSourceRequest request, EmpViewModel empVM)
        {
            if (empVM != null && ModelState.IsValid)
            {
                MEmp emp = new MEmp();
                emp.SetAssignedIdTo(empVM.EmpID);

                ConvertToEmp(empVM, emp);

                emp.CreatedDate = DateTime.Now;
                emp.CreatedBy = User.Identity.Name;
                emp.DataStatus = "New";

                empTasks.Insert(emp);
            }

            return Json(new[] { empVM }.ToDataSourceResult(request, ModelState));
        }

        private static void ConvertToEmp(EmpViewModel empVM, MEmp emp)
        {
            emp.EmpName = empVM.EmpName;
            emp.EmpPhone = empVM.EmpPhone;
            emp.EmpAddress = empVM.EmpAddress;
            emp.EmpWorkSince = empVM.EmpWorkSince;
            emp.EmpGender = empVM.EmpGender;
            emp.EmpStatus = empVM.EmpStatus;
            emp.EmpCommission = empVM.EmpCommission;
            emp.EmpReligion = empVM.EmpReligion;
            emp.EmpDepartment = empVM.EmpDepartment;
            emp.EmpDesc = empVM.EmpDesc;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Emps_Update([DataSourceRequest] DataSourceRequest request, EmpViewModel empVM)
        {
            if (empVM != null && ModelState.IsValid)
            {
                var emp = empTasks.One(empVM.EmpID);
                if (emp != null)
                {
                    ConvertToEmp(empVM, emp);

                    emp.ModifiedDate = DateTime.Now;
                    emp.ModifiedBy = User.Identity.Name;
                    emp.DataStatus = "Updated";

                    empTasks.Update(emp);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Emps_Destroy([DataSourceRequest] DataSourceRequest request, EmpViewModel empVM)
        {
            if (empVM != null)
            {
                var emp = empTasks.One(empVM.EmpID);
                if (emp != null)
                {
                    //emp.ModifiedDate = DateTime.Now;
                    //emp.ModifiedBy = User.Identity.Name;
                    //emp.DataStatus = "Deleted";
                    empTasks.Delete(emp);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<EmpViewModel> GetEmps()
        {
            var emps = this.empTasks.GetListNotDeleted();

            return from emp in emps
                   select new EmpViewModel
        {
            EmpID = emp.Id,
            EmpName = emp.EmpName,
            EmpPhone = emp.EmpPhone,
            EmpAddress = emp.EmpAddress,
            EmpWorkSince = emp.EmpWorkSince,
            EmpGender = emp.EmpGender,
            EmpStatus = emp.EmpStatus,
            EmpCommission = emp.EmpCommission,
            EmpReligion = emp.EmpReligion,
            EmpDepartment = emp.EmpDepartment,
            EmpDesc = emp.EmpDesc
        };

        }

        public JsonResult PopulateEmps()
        {
            return Json(GetEmps(), JsonRequestBehavior.AllowGet);
        }

    }
}
