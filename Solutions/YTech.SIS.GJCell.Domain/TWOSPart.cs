using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.SIS.GJCell.Domain
{
    public class TWOSPart : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual TWO WOId { get; set; }
        public virtual MSPart SPartId { get; set; }
        //public virtual MEmp EmpId { get; set; }
        public virtual DateTime? WOSPartDate { get; set; }
        public virtual decimal? WOSPartQty { get; set; }
        public virtual decimal? WOSPartPrice { get; set; }
        public virtual decimal? WOSPartDisc { get; set; }
        public virtual decimal? WOSPartTotal { get; set; }
        public virtual string WOSPartDesc { get; set; }

        public virtual string WOSPartStatus { get; set; }
        public virtual DateTime? WOSPartDateRequest { get; set; }
        public virtual DateTime? WOSPartDateReceived { get; set; }
        public virtual DateTime? WOSPartDateReturn { get; set; }
        public virtual MEmp WOSPartRequestBy { get; set; }
        public virtual MEmp WOSPartReceivedBy { get; set; }
        public virtual MEmp WOSPartReturnBy { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
