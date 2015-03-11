using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMEquipTasks
    {
        void Insert(MEquip equip);
        void Update(MEquip equip);
        void Delete(MEquip equip);
        MEquip One(string equipId);
        IEnumerable<MEquip> GetListNotDeleted();
    }
}
