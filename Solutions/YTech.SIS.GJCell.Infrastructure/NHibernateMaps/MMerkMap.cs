using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class MMerkMap : IAutoMappingOverride<MMerk>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MMerk> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_MERK");
            mapping.Id(x => x.Id, "MERK_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.MerkName, "MERK_NAME");
            mapping.Map(x => x.MerkStatus, "MERK_STATUS");
            mapping.Map(x => x.MerkDesc, "MERK_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
