using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Domain.Contracts;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public class MEmpRepository : NHibernateRepositoryWithTypedId<MEmp, string>, IMEmpRepository
{
    public IEnumerable<MEmp> GetListNotDeleted()
    {
        ICriteria criteria = Session.CreateCriteria(typeof(MEmp));
        criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
        criteria.AddOrder(new Order("EmpName", true));
        return criteria.List<MEmp>();
    }
}
}
