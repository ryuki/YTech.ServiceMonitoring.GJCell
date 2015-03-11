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
    public class TWOTrackRepository : NHibernateRepositoryWithTypedId<TWOTrack, string>, ITWOTrackRepository
    {
        public IEnumerable<TWOTrack> GetListByWOId(string woId)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TWOTrack));
            criteria.Add(Expression.Eq("WOId.Id", woId));
            criteria.AddOrder(new Order("WOTrackDate", true));
            return criteria.List<TWOTrack>();
        }
    }
}
