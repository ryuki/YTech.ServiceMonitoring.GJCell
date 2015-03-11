using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class MSPartMap : IAutoMappingOverride<MSPart>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MSPart> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_SPART");
            mapping.Id(x => x.Id, "SPART_ID")
                 .GeneratedBy.Assigned();

            mapping.References<MMerk>(x => x.MerkId, "MERK_ID").ForeignKey();//.LazyLoad(FluentNHibernate.Mapping.Laziness.False);
            //mapping.References<MType>(x => x.TypeId, "TYPE_ID").ForeignKey();

            mapping.Map(x => x.SPartName, "SPART_NAME");

            mapping.Map(x => x.SPartPurchasePrice, "SPART_PURCHASE_PRICE");
            mapping.Map(x => x.SPartServicePrice1, "SPART_SERVICE_PRICE_1");
            mapping.Map(x => x.SPartServicePrice2, "SPART_SERVICE_PRICE_2");
            mapping.Map(x => x.SPartServicePrice3, "SPART_SERVICE_PRICE_3");
            mapping.Map(x => x.SPartStatus, "SPART_STATUS");
            mapping.Map(x => x.SPartDesc, "SPART_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
