using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.SIS.GJCell.Domain
{
    public class MSPart : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string SPartName { get; set; }
        public virtual decimal? SPartPurchasePrice { get; set; }
        public virtual decimal? SPartServicePrice1 { get; set; }
        public virtual decimal? SPartServicePrice2 { get; set; }
        public virtual decimal? SPartServicePrice3 { get; set; }
        public virtual string SPartStatus { get; set; }
        public virtual string SPartDesc { get; set; }
        public virtual MMerk MerkId { get; set; }
        //public virtual MType TypeId { get; set; }

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
