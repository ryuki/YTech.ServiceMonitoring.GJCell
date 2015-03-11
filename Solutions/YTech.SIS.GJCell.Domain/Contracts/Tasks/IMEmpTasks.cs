using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMEmpTasks
    {
        IEnumerable<MEmp> GetAllEmps();
        MEmp Insert(MEmp emp);
        MEmp Update(MEmp emp);
        MEmp Delete(MEmp emp);
        MEmp One(string empId);
        IEnumerable<MEmp> GetListNotDeleted();
    }
}
