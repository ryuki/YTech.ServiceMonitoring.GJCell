using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class MTypeMap : IAutoMappingOverride<MType>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MType> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_TYPE");
            mapping.Id(x => x.Id, "TYPE_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.TypeName, "TYPE_NAME");
            mapping.Map(x => x.TypeStatus, "TYPE_STATUS");
            mapping.Map(x => x.TypeDesc, "TYPE_DESC");
            mapping.References<MMerk>(x => x.MerkId, "MERK_ID").ForeignKey();

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
