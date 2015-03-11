using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class TWOEmpMap : IAutoMappingOverride<TWOEmp>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<TWOEmp> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_WO_EMP");
            mapping.Id(x => x.Id, "WO_EMP_ID")
                 .GeneratedBy.Assigned();

            mapping.References<TWO>(x => x.WOId, "WO_ID").ForeignKey();
            mapping.References<MEmp>(x => x.EmpId, "EMP_ID").ForeignKey();

            mapping.Map(x => x.WOEmpStatus, "WO_EMP_STATUS");
            mapping.Map(x => x.WOEmpDesc, "WO_EMP_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
