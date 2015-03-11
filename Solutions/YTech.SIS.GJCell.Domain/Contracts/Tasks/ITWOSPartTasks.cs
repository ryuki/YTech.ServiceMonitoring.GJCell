using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface ITWOSPartTasks
    {
        void Insert(TWOSPart woSpart);
        void Update(TWOSPart woSpart);
        void Delete(TWOSPart woSpart);
        TWOSPart One(string woSpartId);

        IEnumerable<TWOSPart> GetAll();

        IEnumerable<TWOSPart> GetListBySPartDate(DateTime? rptDateFrom, DateTime? rptDateTo);
    }
}
