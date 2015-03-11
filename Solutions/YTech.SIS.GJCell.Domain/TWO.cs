using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Domain.DomainModel;
using System;
using SharpArch.Domain;

namespace YTech.SIS.GJCell.Domain
{
    public class TWO : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string WONo { get; set; }
        public virtual MCustomer CustomerId { get; set; }
        public virtual DateTime? WODate { get; set; }
        public virtual bool WOUnitIsGuarantee { get; set; }
        public virtual string WOEquipments { get; set; }
        public virtual string WOPriority { get; set; }
        public virtual DateTime? WOStartDate { get; set; }
        public virtual string WOLastStatus { get; set; }
        public virtual DateTime? WOEstFinishDate { get; set; }
        public virtual decimal? WOTotal { get; set; }
        public virtual decimal? WODp { get; set; }
        public virtual string WOInvoiceNo { get; set; }
        public virtual DateTime? WOTakenDate { get; set; }
        public virtual string WOBrokenDesc { get; set; }
        public virtual string WODesc { get; set; }
        public virtual string WOComplain { get; set; }

        public virtual string WOReferenceNo { get; set; }
        public virtual MMerk MerkId { get; set; }
        public virtual MType TypeId { get; set; }
        public virtual string WOUnitName { get; set; }
        public virtual string WOUnitSn { get; set; }
        public virtual string WOUnitImei { get; set; }
        public virtual string WOUnitColor { get; set; }
        public virtual string WOUnitPcbNo { get; set; }
        public virtual DateTime? WOUnitPurchaseDate { get; set; }
        public virtual string WOUnitLastTrack { get; set; }
        public virtual decimal? WOServiceFee { get; set; }
        public virtual decimal? WOSPartTotal { get; set; }
        public virtual decimal? WODisc { get; set; }
        public virtual string WOUpdateDesc { get; set; }

        public virtual DateTime? WODateSentToSC { get; set; }
        public virtual DateTime? WODateReceivedFromSC { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual IList<TWOLog> WOLogs { get; protected set; }

        public virtual IList<TWOEmp> WOEmps { get; protected set; }
        //public virtual TWOLog WOLog { get; protected set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
