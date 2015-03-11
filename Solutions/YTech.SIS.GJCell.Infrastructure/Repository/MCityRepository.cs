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
    public class MCityRepository : NHibernateRepositoryWithTypedId<MCity, string>, IMCityRepository
    {
        public IEnumerable<MCity> GetListNotDeleted()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MCity));
            criteria.Add(Expression.Not(Expression.Eq("DataStatus", "Deleted")));
            criteria.AddOrder(new Order("CityName", true));
            return criteria.List<MCity>();
        }
    }
}
