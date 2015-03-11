using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.SIS.GJCell.Domain.Contracts
{
    public interface ITWORepository : INHibernateRepositoryWithTypedId<TWO, string>
    {
        IEnumerable<TWOHaveRead> GetWOByDate(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<TWOHaveRead> GetListNotDeleted(string userName);

        IEnumerable<VWoByEmp> GetWoByEmp(DateTime? dateFrom, DateTime? dateTo);
    }
}
