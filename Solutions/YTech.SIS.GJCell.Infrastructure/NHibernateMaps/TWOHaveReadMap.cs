using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class TWOHaveReadMap : IAutoMappingOverride<TWOHaveRead>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOHaveRead> mapping)
        {
            //use virtual table (not present in database) to display in wo list
            mapping.Table("dbo.T_WO_HAVE_READ");
            mapping.ReadOnly();
            mapping.Id(x => x.Id, "WO_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.WONo, "WO_NO");
            //mapping.References(x => x.CustomerId, "CUSTOMER_ID").Fetch.Join();

            mapping.Map(x => x.CustomerId, "CUSTOMER_ID");
            mapping.Map(x => x.CustomerName, "CUSTOMER_NAME");
            mapping.Map(x => x.CustomerPhone, "CUSTOMER_PHONE");
            mapping.Map(x => x.CustomerAddress, "CUSTOMER_ADDRESS");

            mapping.Map(x => x.MerkId, "MERK_ID");
            mapping.Map(x => x.MerkName, "MERK_NAME");
            mapping.Map(x => x.TypeId, "TYPE_ID");
            mapping.Map(x => x.TypeName, "TYPE_NAME");

            mapping.Map(x => x.WODate, "WO_DATE");
            mapping.Map(x => x.WOUnitIsGuarantee, "WO_UNIT_IS_GUARANTEE");
            mapping.Map(x => x.WOEquipments, "WO_EQUIPMENTS");
            mapping.Map(x => x.WOPriority, "WO_PRIORITY");
            mapping.Map(x => x.WOStartDate, "WO_START_DATE");
            mapping.Map(x => x.WOLastStatus, "WO_LAST_STATUS");
            mapping.Map(x => x.WOEstFinishDate, "WO_EST_FINISH_DATE");
            mapping.Map(x => x.WOTotal, "WO_TOTAL");
            mapping.Map(x => x.WODp, "WO_DP");
            mapping.Map(x => x.WOInvoiceNo, "WO_INVOICE_NO");
            mapping.Map(x => x.WOTakenDate, "WO_TAKEN_DATE");
            mapping.Map(x => x.WOBrokenDesc, "WO_BROKEN_DESC");
            mapping.Map(x => x.WODesc, "WO_DESC");
            mapping.Map(x => x.WOComplain, "WO_COMPLAIN");

            //mapping.References<MMerk>(x => x.MerkId, "MERK_ID").ForeignKey();
            //mapping.References<MType>(x => x.TypeId, "TYPE_ID").ForeignKey();
            mapping.Map(x => x.WOReferenceNo, "WO_REFERENCE_NO");
            mapping.Map(x => x.WOUnitName, "WO_UNIT_NAME");
            mapping.Map(x => x.WOUnitSn, "WO_UNIT_SN");
            mapping.Map(x => x.WOUnitImei, "WO_UNIT_IMEI");
            mapping.Map(x => x.WOUnitColor, "WO_UNIT_COLOR");
            mapping.Map(x => x.WOUnitPcbNo, "WO_UNIT_PCB_NO");
            mapping.Map(x => x.WOUnitPurchaseDate, "WO_UNIT_PURCHASE_DATE");
            mapping.Map(x => x.WOUnitLastTrack, "WO_UNIT_LAST_TRACK");
            mapping.Map(x => x.WOServiceFee, "WO_SERVICE_FEE");
            mapping.Map(x => x.WOSPartTotal, "WO_SPART_TOTAL");
            mapping.Map(x => x.WODisc, "WO_DISC");
            mapping.Map(x => x.WOUpdateDesc, "WO_UPDATE_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();

            mapping.Map(x => x.HaveBeenRead, "HaveBeenRead").ReadOnly();

            mapping.Map(x => x.WODateSentToSC, "WO_DATE_SEND_TO_SC");
            mapping.Map(x => x.WODateReceivedFromSC, "WO_DATE_RECEIVED_FROM_SC");

            mapping.Map(x => x.WOTrackId, "WO_TRACK_ID");
            mapping.Map(x => x.WOTrackFrom, "WO_TRACK_FROM");
            mapping.Map(x => x.WOTrackTo, "WO_TRACK_TO");
            mapping.Map(x => x.WOTrackDate, "WO_TRACK_DATE");
            mapping.Map(x => x.WOTrackIsConfirmed, "WO_TRACK_IS_CONFIRMED");
            mapping.Map(x => x.WOTrackConfirmedDate, "WO_TRACK_CONFIRMED_DATE");
        }
    }
}
