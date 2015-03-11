using NHibernate;
using NHibernate.Criterion;
using SharpArch.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;
using YTech.SIS.GJCell.Domain.Contracts;
using YTech.SIS.GJCell.Enums;

namespace YTech.SIS.GJCell.Infrastructure.Repository
{
    public class TReferenceRepository : NHibernateRepositoryWithTypedId<TReference, string>, ITReferenceRepository
    {
        public TReference GetByReferenceType(EnumReferenceType referenceType)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TReference));
            criteria.Add(Expression.Eq("ReferenceType", referenceType.ToString()));
            criteria.SetCacheable(true);
            return criteria.UniqueResult() as TReference;
        }
    }
}
