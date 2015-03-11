using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class TWOSPartMap : IAutoMappingOverride<TWOSPart>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOSPart> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_WO_SPART");
            mapping.Id(x => x.Id, "WO_SPART_ID")
                 .GeneratedBy.Assigned();

            mapping.References<TWO>(x => x.WOId, "WO_ID").ForeignKey();
            mapping.References<MSPart>(x => x.SPartId, "SPART_ID").ForeignKey();

            //mapping.Map(x => x.em, "EMP_ID");
            mapping.Map(x => x.WOSPartDate, "WO_SPART_DATE");
            mapping.Map(x => x.WOSPartQty, "WO_SPART_QTY");
            mapping.Map(x => x.WOSPartPrice, "WO_SPART_PRICE");
            mapping.Map(x => x.WOSPartDisc, "WO_SPART_DISC");
            mapping.Map(x => x.WOSPartTotal, "WO_SPART_TOTAL");
            mapping.Map(x => x.WOSPartDesc, "WO_SPART_DESC");

            mapping.Map(x => x.WOSPartStatus, "WO_SPART_STATUS");
            mapping.Map(x => x.WOSPartDateRequest, "WO_SPART_DATE_REQUEST");
            mapping.Map(x => x.WOSPartDateReceived, "WO_SPART_DATE_RECEIVED");
            mapping.Map(x => x.WOSPartDateReturn, "WO_SPART_DATE_RETURN");
            mapping.References<MEmp>(x => x.WOSPartRequestBy, "WO_SPART_REQUEST_BY").ForeignKey();
            mapping.References<MEmp>(x => x.WOSPartReceivedBy, "WO_SPART_RECEIVED_BY").ForeignKey();
            mapping.References<MEmp>(x => x.WOSPartReturnBy, "WO_SPART_RETURN_BY").ForeignKey();

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
