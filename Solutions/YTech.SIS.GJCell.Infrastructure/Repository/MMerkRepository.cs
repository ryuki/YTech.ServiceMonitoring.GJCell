﻿using NHibernate;
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
    public class MMerkRepository : NHibernateRepositoryWithTypedId<MMerk, string>, IMMerkRepository
    {
        public IEnumerable<MMerk> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MMerk));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(new Order("MerkName", true));
            return criteria.List<MMerk>();
        }
    }
}
