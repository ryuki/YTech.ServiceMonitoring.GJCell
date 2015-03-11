using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.NHibernate;
using SharpArch.NHibernate.Contracts.Repositories;

namespace YTech.SIS.GJCell.Domain.Contracts.Tasks
{
    public interface IMSPartTasks
    {
        IEnumerable<MSPart> GetAllSParts();
        MSPart Insert(MSPart spart);
        MSPart Update(MSPart spart);
        MSPart Delete(MSPart spart);
        MSPart One(string spartId);
        IEnumerable<MSPart> GetListNotDeleted();
    }
}
