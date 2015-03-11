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
    public partial class CustomerController : Controller
    {
        private readonly IMCustomerTasks _customerTasks;
        private readonly IMCityTasks _cityTasks;
        public CustomerController(IMCustomerTasks customerTasks, IMCityTasks cityTasks)
        {
            this._customerTasks = customerTasks;
            this._cityTasks = cityTasks;
        }

        [Authorize(Roles = "ADMINISTRATOR, SUPERVISOR, CS")] 
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index(bool? isModal)
        {
            PopulateCustomerType();

            if (isModal.HasValue)
                if (isModal.HasValue)
                    return View("Index", "~/Views/Shared/_NoMenuLayout.cshtml");

            return View();
        }

        private void PopulateCustomerType()
        {
            var custType = from CustomerType e in Enum.GetValues(typeof(CustomerType))
                           select new { Value = e.ToString(), Text = e.ToString() };
            ViewData["customerType"] = custType;

        }

        public ActionResult Customers_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetCustomers().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Create([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                MCustomer cust = new MCustomer();
                cust.SetAssignedIdTo(Guid.NewGuid().ToString());

                ConvertToCustomer(custVM, cust);

                cust.CreatedDate = DateTime.Now;
                cust.CreatedBy = User.Identity.Name;
                cust.DataStatus = "New";

                _customerTasks.Insert(cust);
            }

            return Json(new[] { custVM }.ToDataSourceResult(request, ModelState));
        }

        private void ConvertToCustomer(CustomerViewModel custVM, MCustomer cust)
        {
            cust.CustomerName = custVM.CustomerName;
            cust.CustomerPhone = custVM.CustomerPhone;
            cust.CustomerAddress = custVM.CustomerAddress;
            cust.CustomerType = custVM.CustomerType;
            //cust.CustomerCity = custVM.CustomerCity;
            cust.CityId = string.IsNullOrEmpty(custVM.CityId) ? null : _cityTasks.One(custVM.CityId);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Update([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null && ModelState.IsValid)
            {
                var cust = _customerTasks.One(custVM.CustomerID);
                if (cust != null)
                {
                    ConvertToCustomer(custVM, cust);

                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Updated";

                    _customerTasks.Update(cust);
                }
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Customers_Destroy([DataSourceRequest] DataSourceRequest request, CustomerViewModel custVM)
        {
            if (custVM != null)
            {
                var cust = _customerTasks.One(custVM.CustomerID);
                if (cust != null)
                {
                    cust.ModifiedDate = DateTime.Now;
                    cust.ModifiedBy = User.Identity.Name;
                    cust.DataStatus = "Deleted";
                    _customerTasks.Update(cust);
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        private IEnumerable<CustomerViewModel> GetCustomers()
        {
            var customers = this._customerTasks.GetListNotDeleted();

            return from cust in customers
                   select new CustomerViewModel
        {
            CustomerID = cust.Id,
            CustomerName = cust.CustomerName,
            CustomerPhone = cust.CustomerPhone,
            CustomerAddress = cust.CustomerAddress,
            CustomerType = cust.CustomerType,
            //CustomerCity = cust.CustomerCity,
            CityName = cust.CityId != null ? cust.CityId.CityName : string.Empty,
            CityId = cust.CityId != null ? cust.CityId.Id : string.Empty
        };

        }

        public ActionResult GetLastCreatedCustomer(string random)
        {
            MCustomer cust = _customerTasks.GetLastCreatedCustomer();
            return Content(cust.Id);
        }

    }
}
