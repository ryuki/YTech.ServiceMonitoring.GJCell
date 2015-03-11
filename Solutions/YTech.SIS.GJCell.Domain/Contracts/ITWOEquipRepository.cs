using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public interface ITWOEquipRepository : INHibernateRepositoryWithTypedId<TWOEquip, string>
    {
    }
}
