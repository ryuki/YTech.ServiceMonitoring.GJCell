using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class VWoByEmpMap : IAutoMappingOverride<VWoByEmp>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<VWoByEmp> mapping)
        {
            mapping.Table("dbo.V_WO_BY_EMP");
            mapping.ReadOnly();
            mapping.Id(x => x.Id, "ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.WoCount, "WO_COUNT");
            mapping.Map(x => x.EmpName, "EMP_NAME");
            mapping.Map(x => x.WoLastStatus, "WO_LAST_STATUS");
        }
    }
}
