using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMCityTasks
    {
        void Insert(MCity city);
        void Update(MCity city);
        void Delete(MCity city);
        MCity One(string cityId);
        IEnumerable<MCity> GetListNotDeleted();
    }
}
