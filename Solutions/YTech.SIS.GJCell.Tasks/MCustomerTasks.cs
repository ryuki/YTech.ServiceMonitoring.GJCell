using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Domain.Contracts.Tasks;
using SharpArch.Domain;
using YTech.SIS.GJCell.Infrastructure.Repository;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Tasks
{
    public class MCustomerTasks : IMCustomerTasks
    {
        private readonly IMCustomerRepository _customerRepository;

        public MCustomerTasks(IMCustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public IEnumerable<Domain.MCustomer> GetAllCustomers()
        {
            var customers = this._customerRepository.GetAll(); ;
            return customers;
        }
        
        public MCustomer Insert(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Save(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer Update(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Update(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer Delete(Domain.MCustomer cust)
        {
            _customerRepository.DbContext.BeginTransaction();
            _customerRepository.Delete(cust);
            _customerRepository.DbContext.CommitTransaction();
            return cust;
        }

        public MCustomer One(string custId)
        {
            var customers = this._customerRepository.Get(custId); ;
            return customers;
        }

        public MCustomer GetLastCreatedCustomer()
        {
            MCustomer cust = this._customerRepository.GetLastCreatedCustomer();
            return cust;
        }


        public IEnumerable<MCustomer> GetListNotDeleted()
        {
            var customers = this._customerRepository.GetListNotDeleted(); ;
            return customers;
        }
    }
}
