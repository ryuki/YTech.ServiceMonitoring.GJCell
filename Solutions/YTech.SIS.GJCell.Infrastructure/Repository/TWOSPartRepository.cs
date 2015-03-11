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
    public class TWOSPartRepository : NHibernateRepositoryWithTypedId<TWOSPart, string>, ITWOSPartRepository
    {
        public IEnumerable<TWOSPart> GetListBySPartDate(DateTime? rptDateFrom, DateTime? rptDateTo)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWOSPart));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.Add(Expression.Between("WOSPartDate", rptDateFrom.Value, rptDateTo.Value));
            criteria.AddOrder(new Order("WOSPartDate", true));
            criteria.SetFetchMode("SPartId", FetchMode.Eager);
            criteria.SetFetchMode("WOId", FetchMode.Eager);
            criteria.SetFetchMode("WOSPartRequestBy", FetchMode.Eager);
            criteria.SetFetchMode("WOSPartReceivedBy", FetchMode.Eager);
            criteria.SetFetchMode("WOSPartReturnBy", FetchMode.Eager);
            IList<TWOSPart> result = criteria.List<TWOSPart>();

            //if (result.Count > 0)
            //    NHibernateUtil.Initialize(result[0].MerkId);
            return result;
        }
    }
}
