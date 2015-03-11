using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMTypeTasks
    {
        void Insert(MType type);
        void Update(MType type);
        void Delete(MType type);
        MType One(string typeId);
        IEnumerable<MType> GetListNotDeleted();
    }
}
