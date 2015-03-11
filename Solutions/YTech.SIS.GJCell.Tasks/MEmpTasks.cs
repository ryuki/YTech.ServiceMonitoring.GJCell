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
    public class MEmpTasks : IMEmpTasks
    {
        private readonly IMEmpRepository _empRepository;

        public MEmpTasks(IMEmpRepository empRepository)
        {
            this._empRepository = empRepository;
        }

        public IEnumerable<Domain.MEmp> GetAllEmps()
        {
            var emps = this._empRepository.GetAll(); ;
            return emps;
        }
        
        public MEmp Insert(Domain.MEmp emp)
        {
            _empRepository.DbContext.BeginTransaction();
            _empRepository.Save(emp);
            _empRepository.DbContext.CommitTransaction();
            return emp;
        }

        public MEmp Update(Domain.MEmp emp)
        {
            _empRepository.DbContext.BeginTransaction();
            _empRepository.Update(emp);
            _empRepository.DbContext.CommitTransaction();
            return emp;
        }

        public MEmp Delete(Domain.MEmp emp)
        {
            _empRepository.DbContext.BeginTransaction();
            _empRepository.Delete(emp);
            _empRepository.DbContext.CommitTransaction();
            return emp;
        }

        public MEmp One(string empId)
        {
            var emps = this._empRepository.Get(empId); ;
            return emps;
        }

        public IEnumerable<MEmp> GetListNotDeleted()
        {
            var emps = this._empRepository.GetListNotDeleted(); ;
            return emps;
        }
    }
}
