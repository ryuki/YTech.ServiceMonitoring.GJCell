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
    public class MSPartRepository : NHibernateRepositoryWithTypedId<MSPart, string>, IMSPartRepository
    {
        public IEnumerable<MSPart> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MSPart));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(new Order("SPartName", true));
            criteria.SetFetchMode("MerkId", FetchMode.Eager);
            IList<MSPart> result = criteria.List<MSPart>();

            //if (result.Count > 0)
            //    NHibernateUtil.Initialize(result[0].MerkId);
            return result;
        }
    }
}
