using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public interface ITWOSPartRepository : INHibernateRepositoryWithTypedId<TWOSPart, string>
    {
        IEnumerable<TWOSPart> GetListBySPartDate(DateTime? rptDateFrom, DateTime? rptDateTo);
    }
}
