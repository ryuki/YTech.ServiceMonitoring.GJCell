using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class TWOTrackMap : IAutoMappingOverride<TWOTrack>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOTrack> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_WO_TRACK");
            mapping.Id(x => x.Id, "WO_TRACK_ID")
                 .GeneratedBy.Assigned();

            mapping.References<TWO>(x => x.WOId, "WO_ID").ForeignKey();
            mapping.Map(x => x.WOTrackFrom, "WO_TRACK_FROM");
            mapping.Map(x => x.WOTrackTo, "WO_TRACK_TO");
            mapping.Map(x => x.WOTrackDate, "WO_TRACK_DATE");
            mapping.Map(x => x.WOTrackIsConfirmed, "WO_TRACK_IS_CONFIRMED");
            mapping.Map(x => x.WOTrackConfirmedDate, "WO_TRACK_CONFIRMED_DATE");
            mapping.Map(x => x.WOTrackDesc, "WO_TRACK_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
