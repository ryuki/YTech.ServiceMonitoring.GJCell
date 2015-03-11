﻿using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class MEquipMap : IAutoMappingOverride<MEquip>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MEquip> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_EQUIP");
            mapping.Id(x => x.Id, "EQUIP_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.EquipName, "EQUIP_NAME");
            mapping.Map(x => x.EquipStatus, "EQUIP_STATUS");
            mapping.Map(x => x.EquipDesc, "EQUIP_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
