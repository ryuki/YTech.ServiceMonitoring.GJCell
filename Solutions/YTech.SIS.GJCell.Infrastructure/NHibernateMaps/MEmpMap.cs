using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.SIS.GJCell.Domain;

namespace YTech.SIS.GJCell.Infrastructure.NHibernateMaps
{
    public class MEmpMap : IAutoMappingOverride<MEmp>
    {
        public void Override(FluentNHibernate.Automapping.AutoMapping<MEmp> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_EMP");
            mapping.Id(x => x.Id, "EMP_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.EmpName, "EMP_NAME");
            mapping.Map(x => x.EmpDesc, "EMP_DESC");

            mapping.Map(x => x.EmpPhone, "EMP_PHONE");
            mapping.Map(x => x.EmpAddress, "EMP_ADDRESS");
            mapping.Map(x => x.EmpWorkSince, "EMP_WORK_SINCE");
            mapping.Map(x => x.EmpGender, "EMP_GENDER");
            mapping.Map(x => x.EmpStatus, "EMP_STATUS");
            mapping.Map(x => x.EmpCommission, "EMP_COMISSION");
            mapping.Map(x => x.EmpReligion, "EMP_RELIGION");
            mapping.Map(x => x.EmpDepartment, "EMP_DEPARTMENT");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }
    }
}
