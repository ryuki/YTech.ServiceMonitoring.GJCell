using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMMerkTasks
    {
        void Insert(MMerk merk);
        void Update(MMerk merk);
        void Delete(MMerk merk);
        MMerk One(string typeId);
        IEnumerable<MMerk> GetListNotDeleted();
    }
}
