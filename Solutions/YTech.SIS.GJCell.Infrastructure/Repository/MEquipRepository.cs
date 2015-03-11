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
    public class MEquipRepository : NHibernateRepositoryWithTypedId<MEquip, string>, IMEquipRepository
    {
        public IEnumerable<MEquip> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MEquip));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(new Order("EquipName", true));
            return criteria.List<MEquip>();
        }
    }
}
