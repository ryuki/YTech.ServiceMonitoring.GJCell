using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.SIS.GJCell.Domain
{
    public class VWoByEmp : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual decimal? WoCount { get; set; }        
        public virtual string EmpName { get; set; }
        public virtual string WoLastStatus { get; set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
