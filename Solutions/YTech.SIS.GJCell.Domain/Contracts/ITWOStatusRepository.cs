using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public interface ITWOStatusRepository : INHibernateRepositoryWithTypedId<TWOStatus, string>
    {
        IEnumerable<Domain.TWOStatus> GetWOStatus(string woId);
    }
}
