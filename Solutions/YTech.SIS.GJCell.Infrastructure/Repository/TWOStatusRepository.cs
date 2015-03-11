using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Domain.Contracts;

namespace YTech.SIS.GJCell.Infrastructure.Repository
{
    public class TWOStatusRepository : NHibernateRepositoryWithTypedId<TWOStatus, string>, ITWOStatusRepository
    {
        public IEnumerable<TWOStatus> GetWOStatus(string woId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWOStatus));
            criteria.Add(Expression.Eq("WOId.Id", woId));
            criteria.AddOrder(new Order("WOStatusDate", false));
            return criteria.List<TWOStatus>();
        }
    }
}
