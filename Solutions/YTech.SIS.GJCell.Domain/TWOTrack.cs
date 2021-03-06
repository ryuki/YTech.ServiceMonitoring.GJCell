﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.SIS.GJCell.Domain
{
    public class TWOTrack : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual TWO WOId { get; set; }
        public virtual string WOTrackFrom { get; set; }
        public virtual string WOTrackTo { get; set; }
        public virtual DateTime? WOTrackDate { get; set; }
        public virtual bool? WOTrackIsConfirmed { get; set; }
        public virtual DateTime? WOTrackConfirmedDate { get; set; }
        public virtual string WOTrackDesc { get; set; }
        
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
