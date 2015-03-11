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
    public class MTypeRepository : NHibernateRepositoryWithTypedId<MType, string>, IMTypeRepository
    {
        public IEnumerable<MType> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MType));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(new Order("TypeName", true));
            criteria.SetFetchMode("MerkId", FetchMode.Eager);
            return criteria.List<MType>();
        }
    }
}
